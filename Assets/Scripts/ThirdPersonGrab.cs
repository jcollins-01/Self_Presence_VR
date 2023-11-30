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

    public AudioSource playerAudio;

    public ExperimentLogger logger;
    // Start is called before the first frame update
    // void Start()
    // {
    // }

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

        if (rightXRController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
        {
            //Log button press
            logger.RecordKey("RightSecondary", "Attempt to drop Grabbable with body one");

            Debug.Log("Attempting drop with third person body one");
            DropObject();

            //Log button press
            logger.RecordKey("RightSecondary", "Successfully drop Grabbable with body one");
        }

        if (leftXRController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue2) && secondaryButtonValue2)
        {
            //Log button press
            logger.RecordKey("LeftSecondary", "Attempt to drop Grabbable with body two");

            Debug.Log("Attempting drop with third person body two");
            DropObject();

            //Log button press
            logger.RecordKey("LeftSecondary", "Successfully drop Grabbable with body two");
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // If the grabbable collided with the first third-person body, uses right hand controller
        // Layer 6 is Interactables

        if (hit.transform.gameObject.layer == 6 && gameObject == thirdPersonBodyOne)
        {
            grabbable = hit.gameObject;
            grabbableScale = grabbable.transform.localScale;
            // switch from grip to primary button?
            if (rightXRController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            {
                Debug.Log("Attempting grab with third person body one");
                
                //Log button press
                logger.RecordKey("RightPrimary", "Attempt to grab Grabbable with body one");

                GrabObject(bodyOneRightHand);

                //Log button press
                logger.RecordKey("RightPrimary", "Successfully grab Grabbable with body one");
            }
        }

        // If the grabbable collided with the second third-person body, uses left hand controller
        if (hit.transform.gameObject.layer == 6 && gameObject == thirdPersonBodyTwo)
        {
            grabbable = hit.gameObject;
            grabbableScale = grabbable.transform.localScale;

            if (leftXRController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            {
                Debug.Log("Attempting grab with third person body two");

                //Log button press
                logger.RecordKey("LeftPrimary", "Attempt to grab Grabbable with body two");

                GrabObject(bodyTwoRightHand);

                //Log button press
                logger.RecordKey("LeftPrimary", "Successfully grab Grabbable with body two");
            }
        }
    }

    public void GrabObject(GameObject grabbingHand)
    {
        Debug.Log("Trying to grab with " + grabbingHand);
        grabbable.transform.SetParent(grabbingHand.transform);
        grabbable.transform.localPosition = new Vector3(0f, 0f, 0f);
        grabbable.transform.localScale = grabbableScale;
        grabbable.GetComponent<Rigidbody>().useGravity = false;
        grabbable.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);

        // Turns it invisible
        grabbable.transform.localScale = new Vector3(0, 0, 0);
        playerAudio.Play();
    }

    public void DropObject()
    {
        Debug.Log("Trying to drop grabbable");
        grabbable.transform.parent = null;
        grabbable.transform.localScale = grabbableScale;
        grabbable.GetComponent<Rigidbody>().useGravity = true;
    }
}
