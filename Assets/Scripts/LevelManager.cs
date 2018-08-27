using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Include namespace required to use Unity SceneManager.
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    
    public Text pauseText;

    private bool pause;

    // Initialisation.
    void Start ()
    {
        pause = false;
        pauseText.text = "";
    }

	// Game logic update.
	void Update ()
    {
        // Restart game if R key is pressed.
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Pause game if P key is pressed.
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = pause ^ true;
        }

        // Check if game is paused.
        if (pause == true)
        {
            Time.timeScale = 0;
            pauseText.text = "Paused";
        }
        else
        {
            Time.timeScale = 1;
            pauseText.text = "";
        }

        // Move to next level if enter key is pressed.
        if (PlayerController.levelClear == true && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // Return to start screen if escape key is pressed.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
