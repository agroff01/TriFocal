using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EndingText : MonoBehaviour
{
    public ColorManager ColorManager;
    public TMP_Text first;
    public TMP_Text second;
    public TMP_Text third;
    public TMP_Text fourth;
    // Start is called before the first frame update
    void Start()
    {
        first.enabled = !first.enabled;
        second.enabled = !second.enabled;
        third.enabled = !third.enabled;
        fourth.enabled = !fourth.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if(ColorManager.allLensesCollected)
        {

        }
    }
}
