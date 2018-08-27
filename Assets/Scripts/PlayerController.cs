using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Public variables are accessible through Unity Editor's Inspector.
    public Text pickupText;
    public Text timeText;
    public Text promptText;

    public GameObject mainCamera;

    // Private variables are accessible only within this script.
    private Rigidbody rb;
    private float moveSpeed;
    private float jumpPower;

    private int currPickups;
    private float time;

    public static bool levelClear;

    private int currJumps;
    private int maxJumps;

    private Vector3 offset;
    
    // Initialisation.
    void Start ()
    {
        // Assign Rigidbody Component to private Rigidbody variable.
        rb = GetComponent<Rigidbody>();

        moveSpeed = 10;
        jumpPower = 5;

        currPickups = 0;
        pickupText.text = "Pickups Collected: " + currPickups.ToString();

        levelClear = false;

        currJumps = 2;
        maxJumps = 2;

        promptText.text = "";
    }

    // Physics update.
    void FixedUpdate ()
    {
        // Camera-relative movement for forward, backward, left and right.
        // Start.
        offset = transform.position - mainCamera.transform.position;
        offset.Set(offset.x, 0.0f, offset.z);
        offset = offset.normalized;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(offset * moveSpeed);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(-offset * moveSpeed);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Cross product of 2 vectors produces orthogonal vector.
            rb.AddForce(Vector3.Cross(offset, Vector3.up) * moveSpeed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Cross product of 2 vectors produces orthogonal vector.
            rb.AddForce(Vector3.Cross(-offset, Vector3.up) * moveSpeed);
        }
        // End.

        // Environment-relative movement for forward, backward, left and right.
        // vector3 movement = new vector3(input.getaxis("horizontal"), 0.0f, input.getaxis("vertical"));
        // rb.addforce(movement * speed);

        // Jump if at least 1 jump available.
        if (Input.GetKeyDown(KeyCode.Space) && currJumps >= 1)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            // Remove 1 jump from jumps counter when jumping.
            // Cannot use OnCollisionExit() as we want to jump in mid-air.
            currJumps--;
        }

        // Complete brake.
        if (Input.GetKeyDown(KeyCode.X))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    // Game logic update.
    void Update ()
    {
        // Display time.
        if (levelClear == false)
        {
            time = (float)Math.Round(Time.timeSinceLevelLoad, 3);
        }
        timeText.text = "Time: " + time.ToString();
    }

    // Called when this collider/rigidbody has begun touching another rigidbody/collider.
    void OnCollisionEnter (Collision collision)
    {
        // Reset maximum number of jumps when player returns to ground.
        if (collision.gameObject.CompareTag("Ground"))
        {
            currJumps = maxJumps;
        }

        // Collision with lava.
        if (collision.gameObject.CompareTag("Lava"))
        {
            // Deactivate player.
            gameObject.SetActive(false);
            promptText.text = "You Died!\nR to Restart!";
            AudioManager.deathAudioSrc.Play();
        }

        // Collision with rubber.
        if (collision.gameObject.CompareTag("Rubber"))
        {
            rb.AddForce(collision.gameObject.transform.forward * -10, ForceMode.Impulse);
        }

        // Collision with rubber block.
        if (collision.gameObject.CompareTag("Rubber Block"))
        {
            rb.AddForce(collision.gameObject.transform.up.normalized * 10, ForceMode.Impulse);
        }
    }

    // Called when the Collider other enters the trigger.
    void OnTriggerEnter (Collider other)
    {
        // Collision with pickups.
        if (other.gameObject.CompareTag("Pickup") && levelClear == false)
        {
            // Deactivate and collect pickups.
            other.gameObject.SetActive(false);
            currPickups++;
            pickupText.text = "Pickups Collected: " + currPickups.ToString();
            AudioManager.pickupAudioSrc.Play();
        }

        // Collision with powerups.
        if (other.gameObject.CompareTag("Powerup"))
        {
            other.gameObject.SetActive(false);
            AudioManager.powerAudioSrc.Play();
            StartCoroutine("Powerup");
        }

        // Collision with goal.
        if (other.gameObject.CompareTag("Goal"))
        {
            // Save score. Format: Pickup:"Level1pickup". Time:"Level1time".
            if ((currPickups > PlayerPrefs.GetInt("Level" + SceneManager.GetActiveScene().buildIndex.ToString() + "pickup")) ||
                (currPickups == PlayerPrefs.GetInt("Level" + SceneManager.GetActiveScene().buildIndex.ToString() + "pickup") && time < PlayerPrefs.GetFloat("Level" + SceneManager.GetActiveScene().buildIndex.ToString() + "time"))
                )
            {
                PlayerPrefs.SetInt("Level" + SceneManager.GetActiveScene().buildIndex.ToString() + "pickup", currPickups);
                PlayerPrefs.SetFloat("Level" + SceneManager.GetActiveScene().buildIndex.ToString() + "time", time);
            }

            // Deactivate player.
            gameObject.SetActive(false);
            levelClear = true;
            promptText.text = "Level Cleared!\nEnter for Next Level!";
            AudioManager.clearAudioSrc.Play();
        }
    }

    IEnumerator Powerup ()
    {
        promptText.text = "Powerup!";
        moveSpeed = 20;
        currJumps = 3;
        maxJumps = 3;
        yield return new WaitForSeconds(10f);
        promptText.text = "";
        moveSpeed = 10;
        maxJumps = 2;
    }
}