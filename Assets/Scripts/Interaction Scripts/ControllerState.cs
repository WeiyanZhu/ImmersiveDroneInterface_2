﻿namespace ISAACS {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
		using VRTK;


	/* This script is a wrapper around VRTK, to be used as a library for
	higher-level scripts needing access to user input coming from a controller.
	For further clarifications, ask Apollo.
	*/

	public class ControllerState : MonoBehaviour {

		// Remember to set these controllers to actual GameObjects through the Unity GUI.
		public GameObject LeftController;
		public GameObject RightController;


		// These variables store information on the state of the left controller.
		private bool LeftIsGrabbing; /// True if left hand is grabbing another object.
		private bool LeftIndex; /// True if the left index trigger is being pressed.
		private bool LeftMiddle; /// True if the left middle trigger is being pressed.
		private bool LeftX; /// True if the left X button is being pressed.
		private bool LeftY; /// True if the left Y button is being pressed.
		private bool LeftThumb; /// True if the left thumbstick is being pressed.
		private Vector2 LeftThumbDelta; /// The distance by which the left thumbstick has been moved in the X-axis and Y-axis.
		private float LeftThumbAngle; /// The angle of rotation of the left thumbstick.
		private Transform LeftOrigin; /// The origin (position, rotation, scale) of the left controller.
		private Vector3 LeftPosition; /// The position of the left controller.
		private Vector3 LeftLocalPosition; /// The relative position of the left controller.
		private Quaternion LeftRotation; /// The rotation of the left controller. Can be queried for its normalized value, eulerAngles, and w, x, y, and z components.
		private Quaternion LeftLocalRotation; /// The relative rotation of the left controller. Can be queried for its normalized value, eulerAngles, and w, x, y, and z components.
		private Vector3 LeftVelocity; /// How fast the left controller is moving in space.
		private Vector3 LeftAngularVelocity; /// How fast the left controller is rotating in space.


		// These variables store information on the state of the right controller.
		private bool RightIsGrabbing; /// True if the right hand is grabbing another object.
		private bool RightIndex; /// True if the right index trigger is being pressed.
		private bool RightMiddle; /// True if the right middle trigger is being pressed.
		private bool RightA; /// True if the right A button is being pressed.
		private bool RightB; /// True if the right B button is being pressed.
		private bool RightThumb; /// True if the right thumbstick is being pressed.
		private Vector2 RightThumbDelta; /// The distance by which the right thumbstick has been moved in the X-axis and Y-axis.
		private float RightThumbAngle; /// The angle of rotation of the right thumbstick.
		private Transform RightOrigin; /// The origin (position, rotation, scale) of the right controller.
		private Vector3 RightPosition; /// The position of the right controller.
		private Vector3 RightLocalPosition; /// The relative position of the right controller.
		private Quaternion RightRotation; /// The rotation of the right controller. Can be queried for its normalized value, eulerAngles, and w, x, y, and z components.
		private Quaternion RightLocalRotation; /// The relative rotation of the right controller. Can be queried for its normalized value, eulerAngles, and w, x, y, and z components.
		private Vector3 RightVelocity; /// How fast the right controller is moving in space.
		private Vector3 RightAngularVelocity; /// How fast the right controller is rotating in space.


		// These variables store information on the state of both controllers.
		private bool BothMiddle; /// True if both the right and the left middle triggers are being pressed.
		private Vector3 Distance; /// The absolute distance between the two controllers.
		private Vector3 LocalDistance; /// The relative distance between the two controller. Unless the local scale is different from the absolute scale, LocalDistance == Distance.
		private Vector3 VelocityDelta; /// The difference in velocity of the two controllers.
		private Vector3 AngularVelocityDelta; /// The difference in angular velocity of the two controllers.
		private float ScalingFactor; /// The dot product of VelocityDelta with Distance, indicating the absolute cartesian scaling factor.
		private float LocalScalingFactor; /// The dot product of elocityDelta with LocalDistance, indicating the relative cartesian scaling factor.


		// Unlike Update, which gets called once per frame, FixedUpdate is called accordingly to the physics engine.
		void FixedUpdate() {

			// These variables store the controller state, which can be queried for input information.
            var left = LeftController.GetComponent<VRTK_ControllerEvents>();
			var leftInteractGrab = LeftController.GetComponent<VRTK_InteractGrab>();
			var right = RightController.GetComponent<VRTK_ControllerEvents>();
			var rightInteractGrab = RightController.GetComponent<VRTK_InteractGrab>();

			// Receive input information from the left controller.
			LeftIsGrabbing = (leftInteractGrab.GetGrabbedObject()) ? true : false;
			LeftIndex = left.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress);
			LeftMiddle = left.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress);
			LeftX = left.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress);
			LeftY = left.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress);
			LeftThumb = left.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TouchpadPress);
			LeftThumbDelta = left.GetAxis(VRTK_ControllerEvents.Vector2AxisAlias.Touchpad);
			LeftThumbAngle = left.GetAxisAngle(VRTK_ControllerEvents.Vector2AxisAlias.Touchpad);
			LeftOrigin = VRTK_DeviceFinder.GetControllerOrigin(VRTK_DeviceFinder.GetControllerReferenceLeftHand());
			LeftPosition = LeftOrigin.position;
			LeftLocalPosition = LeftOrigin.localPosition;
			LeftRotation = LeftOrigin.rotation;
			LeftLocalRotation = LeftOrigin.localRotation;
			LeftVelocity = VRTK_DeviceFinder.GetControllerVelocity(VRTK_DeviceFinder.GetControllerReferenceLeftHand());
			LeftAngularVelocity = VRTK_DeviceFinder.GetControllerAngularVelocity(VRTK_DeviceFinder.GetControllerReferenceLeftHand());

            // Receive input information from the right controller.
			RightIsGrabbing = (rightInteractGrab.GetGrabbedObject()) ? true : false;
			RightIndex = right.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress);
			RightMiddle = right.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress);
			RightA = right.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress);
			RightB = right.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress);
			RightThumb = right.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TouchpadPress);
			RightThumbDelta = right.GetAxis(VRTK_ControllerEvents.Vector2AxisAlias.Touchpad);
			RightThumbAngle = right.GetAxisAngle(VRTK_ControllerEvents.Vector2AxisAlias.Touchpad);
			RightOrigin = VRTK_DeviceFinder.GetControllerOrigin(VRTK_DeviceFinder.GetControllerReferenceRightHand());
			RightPosition = RightOrigin.position;
			RightLocalPosition = RightOrigin.localPosition;
			RightRotation = RightOrigin.rotation;
			RightLocalRotation = RightOrigin.localRotation;
			RightVelocity = VRTK_DeviceFinder.GetControllerVelocity(VRTK_DeviceFinder.GetControllerReferenceRightHand());
			RightAngularVelocity = VRTK_DeviceFinder.GetControllerAngularVelocity(VRTK_DeviceFinder.GetControllerReferenceRightHand());

            // Compute information about the state of both controllers.
			BothMiddle = LeftMiddle && RightMiddle;
			Distance = LeftPosition - RightPosition;
			LocalDistance = LeftLocalPosition - RightLocalPosition;
			VelocityDelta = LeftVelocity - RightVelocity;
			AngularVelocityDelta = LeftAngularVelocity - RightAngularVelocity;
			ScalingFactor = Vector3.Dot(VelocityDelta, Distance);
			LocalScalingFactor = Vector3.Dot(VelocityDelta, LocalDistance);
		}


		// Getter methods, left controller.
		public bool GetLeftIsGrabbing() /// True if left hand is grabbing an object.
		{
			return LeftIsGrabbing;
		}
		
		public bool GetLeftIndex() /// True if the left index trigger is being pressed.
		{
			return LeftIndex;
		}

		public bool GetLeftMiddle() /// True if the left middle trigger is being pressed.
		{
			return LeftMiddle;
		}
		
		public bool GetLeftX() /// True if the left X button is being pressed.
		{
			return LeftX;
		}

		public bool GetLeftY() /// True if the left Y button is being pressed.
		{
			return LeftY;
		}

		public bool GetLeftThumb() /// True if the left thumbstick is being pressed.
		{
			return LeftThumb;
		}

		public Vector2 GetLeftThumbDelta() /// The distance by which the left thumbstick has been moved in the X-axis and Y-axis.
		{
			return LeftThumbDelta;
		}

		public float GetLeftThumbAngle() /// The angle of rotation of the left thumbstick.
		{
			return LeftThumbAngle;
		}

		public Transform GetLeftOrigin() /// The origin ("position") of the left controller.
		{
			return LeftOrigin;
		}

		public Vector3 GetLeftPosition() /// The position of the left controller.
		{
			return LeftPosition;
		}

		public Vector3 GetLeftLocalPosition() /// The relative position of the left controller.
		{
			return LeftLocalPosition;
		}

		public Quaternion GetLeftRotation() /// The rotation of the left controller. Can be queried for its normalized value, eulerAngles, and w, x, y, and z components.
		{
			return LeftRotation;
		}

		public Quaternion GetLeftLocalRotation() /// The relative rotation of the left controller. Can be queried for its normalized value, eulerAngles, and w, x, y, and z components.
		{
			return LeftLocalRotation;
		}

		public Vector3 GetLeftVelocity() /// How fast the left controller is moving in space.
		{
			return LeftVelocity;
		}

		public Vector3 GetLeftAngularVelocity() /// How fast the left controller is rotating in space.
		{
			return LeftAngularVelocity;
		}




		// Getter methods, right controller.
		public bool GetRightIsGrabbing() /// True if the right hand is grabbing an object.
		{
			return RightIsGrabbing;
		}
		
		public bool GetRightIndex() /// True if the right index trigger is being pressed.
		{
			return RightIndex;
		}

		public bool GetRightMiddle() /// True if the right middle trigger is being pressed.
		{
			return RightMiddle;
		}
		
		public bool GetRightA() /// True if the right A button is being pressed.
		{
			return RightA;
		}

		public bool GetRightB() /// True if the right B button is being pressed.
		{
			return RightB;
		}

		public bool GetRightThumb() /// True if the right thumbstick is being pressed.
		{
			return RightThumb;
		}

		public Vector2 GetRightThumbDelta() /// The distance by which the right thumbstick has been moved in the X-axis and Y-axis.
		{
			return RightThumbDelta;
		}

		public float GetRightThumbAngle() /// The angle of rotation of the right thumbstick.
		{
			return RightThumbAngle;
		}

		public Transform GetRightOrigin() /// The origin ("position") of the right controller.
		{
			return RightOrigin;
		}

		public Vector3 GetRightPosition() /// The position of the right controller.
		{
			return RightPosition;
		}

		public Vector3 GetRightLocalPosition() /// The relative position of the right controller.
		{
			return RightLocalPosition;
		}

		public Quaternion GetRightRotation() /// The rotation of the right controller. Can be queried for its normalized value, eulerAngles, and w, x, y, and z components.
		{
			return RightRotation;
		}

		public Quaternion GetRightLocalRotation() /// The relative rotation of the right controller. Can be queried for its normalized value, eulerAngles, and w, x, y, and z components.
		{
			return RightLocalRotation;
		}

		public Vector3 GetRightVelocity() /// How fast the right controller is moving in space.
		{
			return RightVelocity;
		}

		public Vector3 GetRightAngularVelocity() /// How fast the right controller is rotating in space.
		{
			return RightAngularVelocity;
		}




		// Getter methods, both controllers.
		public bool GetBothMiddle() /// True if both the right and the left middle triggers are being pressed.
		{
			return BothMiddle;
		}
		
		public Vector3 GetDistance() /// The absolute distance between the two controllers.
		{
			return Distance;
		}
		
		public Vector3 GetLocalDistance() /// The relative distance between the two controller. Unless the local scale is different from the absolute scale, LocalDistance == Distance.
		{
			return LocalDistance;
		}
		
		public Vector3 GetVelocityDelta() /// The difference in velocity of the two controllers.
		{
			return VelocityDelta;
		}
		
		public Vector3 GetAngularVelocityDelta() /// The difference in angular velocity of the two controllers.
		{
			return AngularVelocityDelta;
		}
		
		public float GetScalingFactor() /// The dot product of VelocityDelta with Distance, indicating the absolute cartesian scaling factor.
		{
			return ScalingFactor;
		}
		
		public float GetLocalScalingFactor() /// The dot product of elocityDelta with LocalDistance, indicating the relative cartesian scaling factor.
		{
			return LocalScalingFactor;
		}
		
	}
}