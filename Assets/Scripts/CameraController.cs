using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Public variables are accessible through Unity Editor's Inspector.
    public GameObject player;

    // Private variables are accessible only within this script.
    private Vector3 offset;

    // Initialisation.
    void Start ()
    {
        // Difference between camera position and player position.
        offset = transform.position - player.transform.position;
    }

    // Scene rendering update.
    void LateUpdate ()
    {
        // Camera is moved into new position aligned with player.
        // Update() is not the best place for this code though it runs every frame.
        // Use LateUpdate() for follow cameras, procedural animation and gathering last known states.
        // LastUpdate() runs every frame just like Update() but
        // guaranteed to run after all items have been processed in Update().
        // This sets camera position after knowing absolutely player has moved for that frame.
        // transform.position = player.transform.position + offsset;

        // Zooms camera in.
        if (Input.GetKey(KeyCode.W))
        {
            // Fixed-length zoom in.
            offset = offset - offset.normalized * 0.1f;

            // Exponentially decreasing zoom in.
            offset = offset * 0.99f;
        }

        // Zooms camera out.
        if (Input.GetKey(KeyCode.S))
        {
            // Fixed-length zoom out.
            offset = offset + offset.normalized * 0.1f;

            // Exponentially increasing zoom out.
            // offset = offset * 1.01f;
        }

        // Rotate camera around player to the left.
        if (Input.GetKey(KeyCode.A))
        {
            // Vector3 * Quaternion does not work as matrix multiplication is non-commutative.
            offset = Quaternion.AngleAxis(1.0f, Vector3.up) * offset;

            // This does not work for some reason.
            // transform.RotateAround(player.transform.position, Vector3.up, 10 * Time.deltaTime);
            // offset = transform.position - player.transform.position;
        }

        // Rotate camera around player to the right.
        if (Input.GetKey(KeyCode.D))
        {
            // Vector3 * Quaternion does not work as matrix multiplication is non-commutative.
            offset = Quaternion.AngleAxis(-1.0f, Vector3.up) * offset;

            // This does not work for some reason.
            // transform.RotateAround(player.transform.position, Vector3.up, -10 * Time.deltaTime);
            // offset = transform.position - player.transform.position;
        }

        // Update camera position and make camera point toward player.
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform);
    }
}
