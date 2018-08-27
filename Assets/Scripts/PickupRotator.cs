using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRotator : MonoBehaviour {

    // Game logic update.
    void Update ()
    {
        // Rotate the GameObject that this script is attached to
        // by 15 in X-axis, 30 in Y-axis and 45 in Z-axis,
        // multiplied by Time.deltaTime to make rotation per second instead of per frame.
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}