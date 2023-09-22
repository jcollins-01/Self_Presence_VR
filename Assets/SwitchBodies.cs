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

    private InputDevice rightXRController;
    private InputDevice leftXRController;
    // Start is called before the first frame update
    void Start()
    {
        //Get controllers
        List<InputDevice> devices = new List<InputDevice>();

        InputDeviceCharacteristics rightController = InputDeviceCharacteristics.HeldInHand & InputDeviceCharacteristics.Right;
        InputDevices.GetDevicesWithCharacteristics(rightController, devices);

        InputDeviceCharacteristics leftController = InputDeviceCharacteristics.HeldInHand & InputDeviceCharacteristics.Left;
        InputDevices.GetDevicesWithCharacteristics(leftController, devices);

        Debug.Log("Found devices " + devices);
        if (devices.Count > 0)
        {
            rightXRController = devices[2]; //attached to right controller
            Debug.Log("Assigned " + devices[2] + " as right controller");
            leftXRController = devices[1]; // attached to left controller
            Debug.Log("Assigned " + devices[1] + " as left controller");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //When primary button is pressed
        if ((rightXRController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue))
        {
            if (bodyOneIsActive)
            {
                //Find all objects with tag SwitchComponents
            }
            else
            {

            }
        }
        //if bodyOneIsActive, get bodyOne trackingspace, locomotion system, camera, and deactivate them
        // active bodyTwo trackingspace, locomotion system, and camera

        //if bodyOneIsactive!, do the reverse

    }
}
