using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CollisionHandling : MonoBehaviour
{
    private XRGrabInteractable grabbable;
    public GameObject xrOrigin;
    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbable.isSelected)
        {
            // Ignore collisions between the game object being grabbed and the XRRig
            Physics.IgnoreCollision(grabbable.GetComponent<Collider>(), xrOrigin.GetComponent<Collider>(), true);
            // Ignore collisions between Interactables (6) and XRRig (7)
            //Physics.IgnoreLayerCollision(6, 7, true);
        }
        else
        {
            Physics.IgnoreCollision(grabbable.GetComponent<Collider>(), xrOrigin.GetComponent<Collider>(), false);
            //Physics.IgnoreLayerCollision(6, 7, false);
        }
    }
}
