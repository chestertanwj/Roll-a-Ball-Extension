using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Target is the world space position the object is to be moved to.

public class Translator : MonoBehaviour {

    // Public variables are accessible through Unity Editor's Inspector.
    public float targetX;
    public float targetY;
    public float targetZ;
    public float speed;

    // Private variables are accessible only within this script.
    private Rigidbody rb;

    private Vector3 origin;
    private Vector3 target;
    private Vector3 offset;

    private bool reverse;

    // Initialisation.
    void Start ()
    {
        rb = GetComponent<Rigidbody>();

        origin = transform.position;
        target = new Vector3(targetX, targetY, targetZ);
        offset = target - origin;

        reverse = false;

        InvokeRepeating("Reverse", offset.magnitude/speed, offset.magnitude/speed);
    }

    // Physics update.
    void FixedUpdate ()
    {
        if (reverse == false)
        {
            rb.MovePosition(rb.position + offset.normalized * speed * Time.deltaTime);
        }
        else
        {
            rb.MovePosition(rb.position - offset.normalized * speed * Time.deltaTime);
        }
    }

    void Reverse ()
    {
        reverse = reverse ^ true;
    }
}
