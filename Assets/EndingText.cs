using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EndingText : MonoBehaviour
{
    public TMP_Text textDisplay;
    public float delayBetweenTexts = 1f; // Delay between each text display

    public string[] texts;
    private int currentIndex = 0;

    public ColorManager ColorManager;
    // public TMP_Text first;
    // public TMP_Text second;
    // public TMP_Text third;
    // public TMP_Text fourth;
    // Start is called before the first frame update
    void Start()
    {
        // first.enabled = !first.enabled;
        // second.enabled = !second.enabled;
        // third.enabled = !third.enabled;
        // fourth.enabled = !fourth.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if(ColorManager.allLensesCollected)
        {
            
        }
    }



    // private void Start()
    // {
    //     StartCoroutine(PrintTexts());
    // }

    private IEnumerator PrintTexts()
    {
        foreach (string textToDisplay in texts)
        {
            textDisplay.text = textToDisplay;
            yield return new WaitForSeconds(delayBetweenTexts);
        }

        // Optional: Do something after all texts are displayed
        Debug.Log("All texts displayed");
    }
}
