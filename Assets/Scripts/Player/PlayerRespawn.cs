using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    public TextMeshProUGUI livesText; 

    void Start()
    {
        currentLives = maxLives;

        UpdateLivesText();
    }

    public void Respawn()
    {
        GameObject[] respawnPoints = GameObject.FindGameObjectsWithTag("Respawn");

        if (respawnPoints.Length > 0)
        {
            System.Array.Sort(respawnPoints, (x, y) => Vector3.Distance(transform.position, x.transform.position)
                                                        .CompareTo(Vector3.Distance(transform.position, y.transform.position)));

            transform.position = respawnPoints[0].transform.position;
            currentLives--;

            // Update the UI text element
            UpdateLivesText();

            if (currentLives <= 0)
            {
                GameOver();
            }
        }
        else
        {
            Debug.LogWarning("No respawn points found with the 'Respawn' tag.");
        }
    }

    public void GameOver()
    {
        // Enable the cursor before loading the GameOver scene
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("GameOver");
    }

    void UpdateLivesText()
    {
        // Update the UI text element with the current number of lives
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }

    void Update()
    {
        if (GetComponent<PlayerMovement>().isFalling)
        {
            Respawn();
        }
    }
}
