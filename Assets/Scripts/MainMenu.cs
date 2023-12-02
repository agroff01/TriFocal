using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Transition into main menu scene
    public void ReturnMainMenu()
    {
        // Reset the time scale before loading the main menu
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }

    public void Options()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
}
