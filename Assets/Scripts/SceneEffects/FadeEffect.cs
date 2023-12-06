using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public float fadeDuration = 3f; 
    private Image fadeImage;

    void Start()
    {
        fadeImage = GetComponent<Image>();
        // Start with a fully black image (Can update just looks like nice transition)
        fadeImage.color = Color.black;  
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure it's fully transparent at the end
        fadeImage.color = new Color(0f, 0f, 0f, 0f);  
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0f, 0f, 0f, 1f);
    }
}
