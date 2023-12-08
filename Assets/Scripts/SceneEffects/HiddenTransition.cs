using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HiddenTransition : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    public string hiddenSceneName = "HiddenScene";

    private void Start()
    {
        // Start the fade-in effect when the scene loads
        StartCoroutine(FadeIn());
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the tag "Computer"
        if (other.CompareTag("Computer"))
        {
            StartCoroutine(FadeOut());

        }
    }

    public void StartHiddenCutScene()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.StopAudio();
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene("HiddenScene");
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartHiddenCutScene();
    }
}
