using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject scoreMenu;
    public Text levelSubtitle;
    public Text levelText;
    public Text scoreText;
    public GameObject helpMenu;
    public InputField levelInputField;

    private AudioSource audioSrc;

    void Start ()
    {
        levelMenu.SetActive(false);
        scoreMenu.SetActive(false);
        helpMenu.SetActive(false);

        levelSubtitle.text = "(1-" + (SceneManager.sceneCountInBuildSettings - 1) + ")";
        
        PlayerPrefs.SetInt("Level", 0);

        audioSrc = GetComponent<AudioSource>();
        audioSrc.Play();
    }

    public void PlayGame ()
    {
        // Next scene after Start in build settings should be Level 1.
        // SceneManager.LoadScene("Level Name");
        SceneManager.LoadScene(1);
    }

    public void SelectLevel ()
    {
        PlayerPrefs.SetInt("Level", int.Parse(levelInputField.text));
    }

    public void LoadLevel ()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }

    public void DisplayScore()
    {
        levelText.text = "";
        scoreText.text = "";

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            levelText.text += "Level " + i.ToString() + "\n";
            scoreText.text += " Pickups: " + PlayerPrefs.GetInt(("Level" + i.ToString() + "pickup")).ToString() + "\t" +
                "Time: " + PlayerPrefs.GetFloat("Level" + i.ToString() + "time").ToString() + "\n";
        }
    }

    public void ClearScore ()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            PlayerPrefs.SetInt("Level" + i.ToString() + "pickup", 0);
            PlayerPrefs.SetFloat("Level" + i.ToString() + "time", 0);
        }

        DisplayScore();
    }

    public void QuitGame()
    {
        Application.Quit();
    }    
}
