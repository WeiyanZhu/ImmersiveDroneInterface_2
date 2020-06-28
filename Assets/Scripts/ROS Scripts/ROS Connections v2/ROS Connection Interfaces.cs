﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISAACS;
using ROSBridgeLib.geometry_msgs;
using ROSBridgeLib.sensor_msgs;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.interface_msgs;


public interface ROSDroneConnectionInterface
{
    // Initlization function
    void InitilizeDrone(int uniqueID, string droneIP, int dronePort, List<string> droneSubscribers, bool simFlight);

    // Query state variables for Informative UI and misc. info

    /// <summary>
    /// State of Unity interface authority over controlling the drone
    /// </summary>
    /// <returns></returns>
    bool HasAuthority();

    /// <summary>
    /// Drone attitude
    /// </summary>
    /// <returns></returns>
    Quaternion GetAttitude();

    /// <summary>
    /// Drone GPS position
    /// </summary>
    /// <returns></returns>
    NavSatFixMsg GetGPSPosition();

    /// <summary>
    /// Drone GPS Health
    /// </summary>
    /// <returns></returns>
    float GetGPSHealth();

    /// <summary>
    /// Drone velocity
    /// </summary>
    /// <returns></returns>
    Vector3 GetVelocity();

    /// <summary>
    /// Drone height above takeoff height
    /// </summary>
    /// <returns></returns>
    float GetHeightAboveTakeoff();

    /// <summary>
    /// Gimbal angles (if available)
    /// </summary>
    /// <returns></returns>
    Vector3 GetGimbalJointAngles();

    /// <summary>
    /// Drone home latitude
    /// </summary>
    /// <returns></returns>
    double GetHomeLat();

    /// <summary>
    /// Drone home longitude
    /// </summary>
    /// <returns></returns>
    double GetHomeLong();

    /// Drone control methods 

    /// <summary>
    /// Start the drone mission
    /// </summary>
    void StartMission();

    /// <summary>
    /// Pause drone mission
    /// </summary>
    void PauseMission();

    /// <summary>
    /// Resume drone mission
    /// </summary>
    void ResumeMission();

    /// <summary>
    /// Update drone mission
    /// </summary>
    void UpdateMission();

    /// <summary>
    /// Land drone
    /// </summary>
    void LandDrone();

    /// <summary>
    /// Sent drone to home position
    /// </summary>
    void FlyHome();

    /// <summary>
    /// Disconnect the ROS connection
    /// </summary>
    void DisconnectROSConnection();

    // Optional in the future
    // void DoTask()

}

public interface ROSSensorConnectionInterface
{
    // Initlization function
    void InitilizeSensor(int uniqueID, string sensorIP, int sensorPort, List<string> sensorSubscribers);

    /// <summary>
    /// Disconnect the ROS connection
    /// </summary>    
    void DisconnectROSConnection();



    /// <summary>
    /// Returns a dictionary of connected subscribers with unique identifiers for each.
    /// </summary>
    /// <returns></returns>
    Dictionary<int, string> GetSensorSubscribers();

    /// <summary>
    /// Function to disconnect a specific subscriber
    /// </summary>
    /// <param name="subscriberID"></param>
    void Unsubscriber(int subscriberID);

    /// <summary>
    /// Function to connect a specific subscriber
    /// </summary>
    /// <param name="subscriberID"></param>
    void Subscribe(int subscriberID);


    // Anything else common across sensors?

}