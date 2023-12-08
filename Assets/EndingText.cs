using System.Collections;
using TMPro;
using UnityEngine;

public class EndingText : MonoBehaviour
{
    public float delayBetweenTexts = 3.0f;
    public TMP_Text[] text;
    private int currentIndex = 0;
    private bool flag = false;

    public ColorManager ColorManager;

    void Start()
    {
        InitializeText();
    }

    void InitializeText()
    {
        foreach (var txt in text)
        {
            Color textColor = txt.color;
            textColor.a = 0f; // Set alpha to 0 for initial fade-in
            txt.color = textColor;
        }
    }

    void Update()
    {
        if (ColorManager.allLensesCollected && !flag)
        {
            StartCoroutine(PrintTexts());
            flag = true;
        }
    }

    private IEnumerator PrintTexts()
    {
        while (currentIndex < 4)
        {
            yield return StartCoroutine(FadeText(text[currentIndex], 1f, delayBetweenTexts / 2f)); // Fade in
            yield return new WaitForSeconds(delayBetweenTexts / 2f); // Wait for half of the delay
            yield return StartCoroutine(FadeText(text[currentIndex], 0f, delayBetweenTexts / 2f)); // Fade out
            currentIndex++;
        }
    }

    IEnumerator FadeText(TMP_Text txt, float targetAlpha, float duration)
    {
        Color textColor = txt.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Lerp(textColor.a, targetAlpha, elapsedTime / duration);
            txt.color = textColor;
            yield return null;
        }

        textColor.a = targetAlpha; // Ensure alpha is set to the target value
        txt.color = textColor;
    }
}