using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SwitchBodies : MonoBehaviour
{
    public GameObject bodyOne;
    public GameObject bodyTwo;
    private bool bodyOneIsActive = true;
    private GameObject[] switchComponents;
    private GameObject[] bodyTwoSwitchComponents;

    private InputDevice rightXRController;
    private InputDevice leftXRController;
    private bool rightControllerGrabbed = false;
    private bool leftControllerGrabbed = false;
    // Start is called before the first frame update
    void Start()
    {
        // Find all switch components in scene
        switchComponents = GameObject.FindGameObjectsWithTag("SwitchComponents");
        bodyTwoSwitchComponents = GameObject.FindGameObjectsWithTag("BodyTwoSwitchComponents");

        // Deactivate bodyTwo components
        for (int i = 0; i < bodyTwoSwitchComponents.Length; i++)
        {
            bodyTwoSwitchComponents[i].SetActive(false);
        }
    }

    public void getControllers()
    {
        if (!rightControllerGrabbed || !leftControllerGrabbed)
        {
            // Makes a list for input devices + fills it with devices that match the characteristics we give in the Unity editor
            // Narrows devices list using characteristics to just the controller we want to use
            List<InputDevice> devices = new List<InputDevice>();

            InputDeviceCharacteristics rightController = InputDeviceCharacteristics.HeldInHand & InputDeviceCharacteristics.Right;
            InputDevices.GetDevicesWithCharacteristics(rightController, devices);

            InputDeviceCharacteristics leftController = InputDeviceCharacteristics.HeldInHand & InputDeviceCharacteristics.Left;
            InputDevices.GetDevicesWithCharacteristics(leftController, devices);

            Debug.Log("Found devices " + devices);

            if (!rightControllerGrabbed)
                rightXRController = devices[2]; //attached to right controller
            if (!leftControllerGrabbed)
                leftXRController = devices[1]; // attached to left controller

            if (devices[2] != null) // rightXRController
            {
                Debug.Log("Grabbed right controller successfully");
                rightControllerGrabbed = true;
            }

            if (devices[1] != null) // leftXRController
            {
                Debug.Log("Grabbed left controller successfully");
                leftControllerGrabbed = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        getControllers();

        //When primary button is pressed
        if ((rightXRController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue))
        {
            Debug.Log("Pressed primary button");
            if (bodyOneIsActive)
            {
                Debug.Log("Body one is active");
                // Activate all components in bodyTwo
                for (int i = 0; i < bodyTwoSwitchComponents.Length; i++)
                {
                    bodyTwoSwitchComponents[i].SetActive(true);
                }

                // Deactivate bodyOne components
                for (int i = 0; i < switchComponents.Length; i++)
                {
                    switchComponents[i].SetActive(false);
                }
                bodyOneIsActive = false;
                Debug.Log("Switched components from body one to body two");
            }
        }

        if((leftXRController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue2) && primaryButtonValue2))
        {
            if (!bodyOneIsActive)
            {
                Debug.Log("Body two is active");
                // Activate all components in bodyOne
                for (int i = 0; i < switchComponents.Length; i++)
                {
                    switchComponents[i].SetActive(true);
                }

                // Deactivate bodyTwo components
                for (int i = 0; i < bodyTwoSwitchComponents.Length; i++)
                {
                    bodyTwoSwitchComponents[i].SetActive(false);
                }
                bodyOneIsActive = true;
                Debug.Log("Switched components from body two to body one");
            }
        }
    }
}
