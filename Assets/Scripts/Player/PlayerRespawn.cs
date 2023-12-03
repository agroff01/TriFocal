using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    public AudioSource fallingSound;
    public Animator fading;

    public int maxLives = 3;
    private int currentLives;
    private GameObject lastRespawnPlatform;

    public TextMeshProUGUI livesText;

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesText();
    }

    public void Respawn()
    {
        if (lastRespawnPlatform != null)
        {
            // Respawn above platform
            transform.position = lastRespawnPlatform.transform.position + Vector3.up;
            transform.position += Vector3.up * 2f;
            currentLives--;

            // Update the text
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
        // Update the UI text with the current number of lives
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }

    public void FallDetected()
    {
        fading.Play("FadeIn");
        fallingSound.Play();
        Respawn(); 
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            lastRespawnPlatform = other.gameObject;
        }
    }
}