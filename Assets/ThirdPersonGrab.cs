using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ThirdPersonGrab : MonoBehaviour
{
    public GameObject thirdPersonBodyOne;
    public GameObject thirdPersonBodyTwo;
    public GameObject bodyOneRightHand;
    public GameObject bodyTwoRightHand;
    private GameObject grabbable;
    private Vector3 grabbableScale;

    private InputDevice rightXRController;
    private InputDevice leftXRController;
    private bool rightControllerGrabbed = false;
    private bool leftControllerGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {

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

            if (!rightControllerGrabbed)
                rightXRController = devices[2]; //attached to right controller
            if (!leftControllerGrabbed)
                leftXRController = devices[1]; // attached to left controller

            if (devices[2] != null) // rightXRController
                rightControllerGrabbed = true;

            if (devices[1] != null) // leftXRController
                leftControllerGrabbed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        getControllers();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("Something collided with the grabbable object");

        // If the grabbable collided with the first third-person body, uses right hand controller
        // Layer 6 is Interactables
        if (hit.transform.gameObject.layer == 6)
        {
            grabbable = hit.gameObject;
            grabbableScale = grabbable.transform.localScale;
            if (rightXRController.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                if (gripValue > 0.1f)
                {
                    Debug.Log("Attempting grab with third person body one");
                    GrabObject(bodyOneRightHand);
                }
                else
                {
                    // Third person body collided with grabbable, but isn't grabbing
                    Debug.Log("Attempting drop with third person body one");
                    DropObject();
                }
            }
        }

        // If the grabbable collided with the second third-person body, uses left hand controller
        if (hit.transform.gameObject.layer == 6)
        {
            grabbable = hit.gameObject;
            grabbableScale = grabbable.transform.localScale;
            if (leftXRController.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                if (gripValue > 0.1f)
                {
                    Debug.Log("Attempting grab with third person body two");
                    GrabObject(bodyTwoRightHand);
                }
                else
                {
                    // Third person body collided with grabbable, but isn't grabbing
                    Debug.Log("Attempting drop with third person body two");
                    DropObject();
                }
            }
        }
    }

    public void GrabObject(GameObject grabbingHand)
    {
        Debug.Log("Trying to grab with " + grabbingHand);
        grabbable.transform.SetParent(grabbingHand.transform);
        grabbable.transform.localPosition = new Vector3(0f, 0f, 0f);
        grabbable.transform.localScale = grabbableScale;
    }

    public void DropObject()
    {
        Debug.Log("Trying to drop grabbable");
        grabbable.transform.parent = null;
        grabbable.transform.localScale = grabbableScale;
        grabbable.GetComponent<Rigidbody>().useGravity = true;
    }
}
