using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private void Update()
    {
        // Check if the current scene is the main menu
        if (IsMainMenuScene())
        {
            // Enable the cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    private bool IsMainMenuScene()
    {
        // Check the name of the current active scene
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu";
    }

}
