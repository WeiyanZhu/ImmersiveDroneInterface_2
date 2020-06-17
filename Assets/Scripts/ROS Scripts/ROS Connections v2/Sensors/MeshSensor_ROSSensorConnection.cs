using System;
using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using System.Threading;


using ROSBridgeLib;
using ROSBridgeLib.geometry_msgs;
using ROSBridgeLib.sensor_msgs;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.interface_msgs;
using ROSBridgeLib.voxblox_msgs;

using ISAACS;

public class MeshSensor_ROSSensorConnection : MonoBehaviour, ROSTopicSubscriber {
    
    // Visualizer variables
    public static string rendererObjectName = "PlacementPlane"; // pick a center point of the map, ideally as part of rotating map

    // Private connection variables
    private ROSBridgeWebSocketConnection ros = null;
    public string client_id;
    private Thread rosMsgThread;

    // Queue of jsonMsgs to be parsed on thread
    private Queue<JSONNode> jsonMsgs = new Queue<JSONNode>();

    // Queue of Mesh Dictionary parsed and generated by thread and to be visualized
    private Queue<Dictionary<long[], Mesh>> meshDicts = new Queue<Dictionary<long[], Mesh>>();

    private MeshVisualizer visualizer;

    private void CreateThread()
    {
        rosMsgThread = new Thread( new ThreadStart(ParseJSON));
        rosMsgThread.IsBackground = true;
        rosMsgThread.Start();
    }

    private void ParseJSON()
    {
        while (true)
        {
            // Check if any json msgs have been recieved
            if (jsonMsgs.Count > 0)
            {
                Debug.Log("JSON Message Count: " + jsonMsgs.Count);
                // Parse json msg to mesh msg
                DateTime startTime = DateTime.Now;
                JSONNode rawMsg = jsonMsgs.Dequeue();
                MeshMsg meshMsg = new MeshMsg(rawMsg);
//                meshMsgs.Enqueue(meshMsg);
                Debug.Log("Message Generation: " + DateTime.Now.Subtract(startTime).TotalMilliseconds.ToString() + "ms");
                startTime = DateTime.Now;
                meshDicts.Enqueue(visualizer.generateMesh(meshMsg));
                Debug.Log("Set Mesh: " + DateTime.Now.Subtract(startTime).TotalMilliseconds.ToString() + "ms");

            }
        }
    }

    public void OnApplicationQuit()
    {
        rosMsgThread.Abort();
    }

    // Initilize the sensor
    public void InitilizeSensor(int uniqueID, string sensorIP, int sensorPort, List<string> sensorSubscribers)
    {
        Debug.Log("Init Mesh Connection at IP " + sensorIP + " Port " + sensorPort.ToString());

        ros = new ROSBridgeWebSocketConnection("ws://" + sensorIP, sensorPort);
        client_id = uniqueID.ToString();

        foreach (string subscriber in sensorSubscribers)
        {
            string subscriberTopic = "";

            switch (subscriber)
            {
                case "mesh":
                    subscriberTopic = "/voxblox_node/" + subscriber;
                    break;
                default:
                    subscriberTopic = "/" + subscriber;
                    break;
            }
            Debug.Log(" Mesh Subscribing to : " + subscriberTopic);
            ros.AddSubscriber(subscriberTopic, this);
        }

        Debug.Log("Mesh Connection Established");
        ros.Connect();

        // Initilize visualizer
        visualizer = GameObject.Find(rendererObjectName).GetComponent<MeshVisualizer>();
        CreateThread();
    }

    // Update is called once per frame in Unity
    void Update()
    {
        if (ros != null)
        {
            ros.Render();
        }

        // Check if any mesh msgs are available to be visualized
        if (meshDicts.Count > 0)
        {
            Debug.Log("Mesh Dict Count: " + meshDicts.Count);
            DateTime startTime = DateTime.Now;
            Dictionary<long[], Mesh> mesh_dict = meshDicts.Dequeue();
            visualizer.SetMesh(mesh_dict);
            Debug.Log("Set Mesh: " + DateTime.Now.Subtract(startTime).TotalMilliseconds.ToString() + "ms");
        }
    }

    // ROS Topic Subscriber methods
    public ROSBridgeMsg OnReceiveMessage(string topic, JSONNode raw_msg, ROSBridgeMsg parsed = null)
    {
        ROSBridgeMsg result = null;
        // Writing all code in here for now. May need to move out to separate handler functions when it gets too unwieldy.
        switch (topic)
        {
            case "/voxblox_node/mesh":
                // Add raw_msg to the jsonMsgs to be parsed on the thread
                jsonMsgs.Enqueue(raw_msg);
                break;
            default:
                Debug.LogError("Topic not implemented: " + topic);
                break;
        }
        return result;
    }
    public string GetMessageType(string topic)
    {
        Debug.Log("Mesh message type is returned as voxblox_msgs/Mesh by default");
        return "voxblox_msgs/Mesh";
    }


}
