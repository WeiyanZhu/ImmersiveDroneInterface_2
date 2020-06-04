﻿namespace ISAACS
{
    using UnityEngine;
    using UnityEngine.UI; // <-- you need this to access UI (button in this case) functionalities
    using UnityEngine.SceneManagement;
    using System.Collections;
    using VRTK;

    public class TakeOffButton : MonoBehaviour
    {
        Button myButton;
        Drone drone;
        private GameObject controller; //needed to access pointer

        void Awake()
        {
            controller = GameObject.FindGameObjectWithTag("GameController");

            myButton = GetComponent<Button>(); // <-- you get access to the button component here
            myButton.onClick.AddListener(() => { OnClickEvent(); });  // <-- you assign a method to the button OnClick event here
        }

        void OnClickEvent()
        {
            Debug.Log("Take Off Button");
            WorldProperties.worldObject.GetComponent<ROSDroneConnection>().Takeoff();
        }
    }
}
