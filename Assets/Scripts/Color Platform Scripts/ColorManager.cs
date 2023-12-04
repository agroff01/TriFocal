using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class ColorManager : MonoBehaviour
{

    public static ColorManager Instance;

    private bool hasRedLens = false;
    private bool hasBlueLens = false;
    private bool hasGreenLens = false;

    public enum FilterState { Normal, Red, Blue, Green }
    [HideInInspector]
    public FilterState currentFilterState = FilterState.Normal;

    [SerializeField]
    private PostProcessProfile normalProfile, redProfile, blueProfile, greenProfile;

    [HideInInspector]
    public bool runColorUpdate = false;
    [HideInInspector]
    public List<ColorObject> inactiveObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (ColorManager.Instance == null){
            ColorManager.Instance = this;
        } else {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (runColorUpdate)
            runColorUpdate = false;

        // Temp Toggle with keys
        if (Input.GetKeyDown(KeyCode.R) && hasRedLens)
        {
            setColorToRed();
            runColorUpdate = true;
        }
        if (Input.GetKeyDown(KeyCode.B) && hasBlueLens)
        {
            setColorToBlue();
            runColorUpdate = true;
        }
        if (Input.GetKeyDown(KeyCode.G) && hasGreenLens)
        {
            setColorToGreen();
            runColorUpdate = true;
        }
        if (Input.GetKeyDown(KeyCode.N)){
            setColorToNormal();
            runColorUpdate = true;
        }

        // if color update needs to be run, run it on inactive objects
        if (runColorUpdate) {
            for (int i = 0;  i < inactiveObjects.Count; i++){
                inactiveObjects[i].localColorUpdate();
            }
        }
    }

    void applyColorFilter(FilterState targetFilter) {
        // Apply post-processing profile based on the target filter
        PostProcessVolume postProcessVolume = Camera.main.GetComponent<PostProcessVolume>();

        if (postProcessVolume != null)
        {
            switch (targetFilter)
            {
                case FilterState.Normal:
                    postProcessVolume.profile = normalProfile;
                    break;
                case FilterState.Red:
                    postProcessVolume.profile = redProfile;
                    break;
                case FilterState.Blue:
                    postProcessVolume.profile = blueProfile;
                    break;
                case FilterState.Green:
                    postProcessVolume.profile = greenProfile;
                    break;                    
            }
            for (int i = 0; i < inactiveObjects.Count; i++)
            {
                inactiveObjects[i].localColorUpdate();
            }
        }
    }

    public bool setColorToRed(){
       if (hasRedLens){
            currentFilterState = FilterState.Red;
            applyColorFilter(currentFilterState);
            return true;
       }
       return false;
    }

    public bool setColorToBlue()
    {
        if (hasBlueLens)
        {
            currentFilterState = FilterState.Blue;
            applyColorFilter(currentFilterState);
            return true;
        }
        return false;
    }

    public bool setColorToGreen()
    {
        if (hasGreenLens)
        {
            currentFilterState = FilterState.Green;
            applyColorFilter(currentFilterState);
            return true;
        }
        return false;
    }

    public bool setColorToNormal()
    {
        currentFilterState = FilterState.Normal;
        applyColorFilter(currentFilterState);
        return true;
    }

    public bool collectedRedLens(){
        hasRedLens = true;
        Debug.Log("RedLens collected! You can now toggle the red filter.");
        return true;
    }

    public bool collectedGreenLens(){
        hasGreenLens = true;
        Debug.Log("GreenLens collected! You can now toggle the Green filter.");
        return true;
    }

    public bool collectedBlueLens(){
        hasBlueLens = true;
        Debug.Log("BlueLens collected! You can now toggle the blue filter.");
        return true;
    }
}
