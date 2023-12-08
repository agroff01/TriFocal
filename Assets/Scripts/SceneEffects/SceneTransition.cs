using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    public float delayBeforeTransition = 35.0f; // Set the delay time in seconds
    public string SceneName = "";

    private void Start()
    {
        // Start the fade-in effect when the scene loads
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        // Check if the delayBeforeTransition time has passed, and start the transition if it has
        if (Time.timeSinceLevelLoad >= delayBeforeTransition)
        {
            TransitionToScene(SceneName);
        }
    }

    IEnumerator FadeIn()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

        // Load the target scene after the fade-out
        SceneManager.LoadScene(SceneName);
    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(FadeOut());
    }
}
