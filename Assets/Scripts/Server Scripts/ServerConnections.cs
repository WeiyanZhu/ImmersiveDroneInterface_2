﻿namespace ISAACS
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using ROSBridgeLib;
    using ROSBridgeLib.std_msgs;
    using ROSBridgeLib.interface_msgs;
    using UnityEditor;
    using System.IO;
    using ISAACS;
    using SimpleJSON;

    public class ServerConnections : MonoBehaviour
    {

        [Header("Server Connection information")]
        private static string serverIP;
        private static int serverPort;

        [Header("ros server connection")]
        private static ROSBridgeWebSocketConnection rosServerConnection = null;


        void Start()
        {
            rosServerConnection = new ROSBridgeWebSocketConnection("ws://" + serverIP, serverPort);
        }

        void GetAllDrones()
        {
            string service_name = "/isaacs_server/getalldrones";
            //Debug.LogFormat();
            //rosServerConnection.CallService(GetAllDronesCallback, service_name, params);
        }

        public static void GetAllDronesCallback(JSONNode response)
        {
            //TODO : @Newman
        }

        public static void uploadMission(Drone drone, string ID, string[] waypoints)
        {
            string service_name = "/isaacs_server/ TODO ";
            //Debug.LogFormat();
            //rosServerConnection.CallService( TODO , service_name, params);
        }

        //TODO: Implement this (What does this do?)
        public static void uploadMissionCallback(JSONNode response)
        {

        }

        //TODO
        public static void startMission(Drone drone, string ID)
        {
            string service_name = "/isaacs_server/ TODO ";
            //Debug.LogFormat();
            //rosServerConnection.CallService( TODO , service_name, params);
        }

        //TODO
        public static void startMissionCallback(JSONNode response)
        {

        }

        //TODO
        public static void pauseMission(Drone drone, string ID)
        {
            string service_name = "/isaacs_server/ TODO ";
            //Debug.LogFormat();
            //rosServerConnection.CallService( TODO , service_name, params);
        }

        //TODO
        public static void pauseMissionCallback(JSONNode response)
        {

        }

        //TODO
        public static void resumeMission(Drone drone, string ID)
        {
            string service_name = "/isaacs_server/ TODO ";
            //Debug.LogFormat();
            //rosServerConnection.CallService( TODO , service_name, params);
        }

        //TODO
        public static void resumeMissionCallback(JSONNode response)
        {

        }

        //TODO
        public static void landDrone(Drone drone, string ID)
        {
            string service_name = "/isaacs_server/ TODO ";
            //Debug.LogFormat();
            //rosServerConnection.CallService( TODO , service_name, params);
        }

        //TODO
        public static void landDroneCallback(JSONNode response)
        {

        }

        //TODO
        public static void flyHome(Drone drone, string ID)
        {
            string service_name = "/isaacs_server/ TODO ";
            //Debug.LogFormat();
            //rosServerConnection.CallService( TODO , service_name, params);
        }

        //TODO
        public static void flyHomeCallback(JSONNode response)
        {

        }
    }
}