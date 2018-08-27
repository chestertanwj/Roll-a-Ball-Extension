using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Target is the rotation in degrees per second.

public class Rotator : MonoBehaviour
{

    // Public variables are accessible through Unity Editor's Inspector.
    public float targetX;
    public float targetY;
    public float targetZ;
    public float speed;

    // Private variables are accessible only within this script.
    private Rigidbody rb;

    private Vector3 eulerAngle;
    private Quaternion quatRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        eulerAngle = new Vector3(targetX, targetY, targetZ);
    }

    void FixedUpdate()
    {
        // quatRotation = Quaternion.Euler(eulerAngle.normalized * speed * Time.deltaTime);
        quatRotation = Quaternion.Euler(eulerAngle * Time.deltaTime);
        rb.MoveRotation(rb.rotation * quatRotation);
    }
}