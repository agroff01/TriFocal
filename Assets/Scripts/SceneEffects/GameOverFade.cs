using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverFade : MonoBehaviour
{
    // Start is called before the first frame update
    public FadeEffect screenFade;

    void Start()
    {
        screenFade = FindObjectOfType<FadeEffect>();

        screenFade.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake()
    {
        
    }
}
