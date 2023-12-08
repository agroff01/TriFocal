using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;
    private PlayerMovement playermovement;

    void Start()
    {
        playermovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;

            if (pauseMenu != null)
            {
                pauseMenu.SetActive(isPaused);

                if (isPaused)
                {
                    Time.timeScale = 0;
                    if (playermovement != null)
                    {
                        playermovement.SetPlayerControl(false);
                    }
                    // Enable the cursor when the game is paused
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Time.timeScale = 1;
                    if (playermovement != null)
                    {
                        playermovement.SetPlayerControl(true);
                    }
                    // Disable the cursor when the game is unpaused
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }
}
