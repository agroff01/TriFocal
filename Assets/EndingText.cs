using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EndingText : MonoBehaviour
{
    public float delayBetweenTexts = 1f; // Delay between each text display

    public TMP_Text[] text;
    private int currentIndex = 0;

    public ColorManager ColorManager;

    void Start()
    {
        text[0].enabled = !text[0].enabled;
        text[1].enabled = !text[1].enabled;
        text[2].enabled = !text[2].enabled;
        text[3].enabled = !text[3].enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if(ColorManager.allLensesCollected)
        {
            StartCoroutine(PrintTexts());
        }
    }

    private IEnumerator PrintTexts()
    {
        while(currentIndex < 4)
        {
            text[currentIndex].enabled = !text[currentIndex].enabled;
            yield return new WaitForSeconds(delayBetweenTexts);
            currentIndex++;
        }
    }
}
