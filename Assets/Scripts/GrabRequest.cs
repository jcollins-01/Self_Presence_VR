using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabRequest : MonoBehaviour
{
    private XRGrabInteractable xrGrabInteractable;

    // Sound variables
    private bool grabbed = false;
    public AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        xrGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (xrGrabInteractable.isSelected)
        {
            grabbed = true;
            playerAudio.Play();
            if (grabbed == true)
                xrGrabInteractable.transform.localScale = new Vector3(0, 0, 0);
        }

        if (grabbed == true)
            xrGrabInteractable.transform.localScale = new Vector3(0, 0, 0);
    }
}
