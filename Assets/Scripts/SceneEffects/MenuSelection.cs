using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour
{
    // Transition into main menu scene
    public void MainMenu()
    {
        // Reset the time scale before loading the main menu
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}
