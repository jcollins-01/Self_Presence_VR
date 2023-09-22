using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Ignore Collisions between avatars and XRRig
        Physics.IgnoreLayerCollision(6, 7);
        CharacterController control = GetComponent<CharacterController>();
        control.detectCollisions = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string name = collision.gameObject.name;
        Debug.Log("Colliding with " + name);
    }
}
