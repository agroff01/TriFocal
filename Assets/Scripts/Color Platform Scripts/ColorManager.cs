using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


public class ColorManager : MonoBehaviour
{

    public static ColorManager Instance;

    private bool hasRedLens = false;
    private bool hasBlueLens = false;
    private bool hasGreenLens = false;
    private bool defaultLens = true;

    public bool allLensesCollected = false;

    public enum FilterState { Normal, Red, Blue, Green }
    [HideInInspector]
    public FilterState currentFilterState = FilterState.Normal;

    [SerializeField]
    private PostProcessProfile normalProfile, redProfile, blueProfile, greenProfile;

    [SerializeField]
    private Image redButtonCover;
    [SerializeField]
    private Image blueButtonCover;
    [SerializeField]
    private Image greenButtonCover;

    [HideInInspector]
    public List<ColorObject> existingColorObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (ColorManager.Instance == null){
            ColorManager.Instance = this;
        } else {
            Destroy(this);
        }

        // Initialize lens images
        if (redButtonCover != null)
        {
            redButtonCover.enabled = !hasRedLens;
        }

        if (blueButtonCover != null)
        {
            blueButtonCover.enabled = !hasBlueLens;
        }

        if (greenButtonCover != null)
        {
            greenButtonCover.enabled = !hasGreenLens;
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
        }
        for (int i = 0; i < existingColorObjects.Count; i++)
        {
            existingColorObjects[i].localColorUpdate();
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
            //GetComponent<VineGrowth>().StartGrowVines();
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
        // Toggle off the red lens image
        if (redButtonCover != null)
        {
            redButtonCover.enabled = false;
        }
        return true;
    }

    public bool collectedGreenLens(){
        hasGreenLens = true;
        Debug.Log("GreenLens collected! You can now toggle the Green filter.");
        // Toggle off the green lens image
        if (greenButtonCover != null)
        {
            greenButtonCover.enabled = false;
        }
        return true;
    }

    public bool collectedBlueLens(){
        hasBlueLens = true;
        Debug.Log("BlueLens collected! You can now toggle the blue filter.");
        // Toggle off the blue lens image
        if (blueButtonCover != null)
        {
            blueButtonCover.enabled = false;
        }
        return true;
    }

    public bool AllLensesCollected()
    {
        if (!allLensesCollected && hasRedLens && hasBlueLens && hasGreenLens)
        {
            allLensesCollected = true;
            Debug.Log("All lenses collected!");
        }

        return allLensesCollected;
    }

    public bool HasLensColor(FilterState targetFilter)
    {
        switch (targetFilter)
        {
            case FilterState.Normal:
                return defaultLens;
            case FilterState.Red:
                return hasRedLens;
            case FilterState.Blue:
                return hasBlueLens;
            case FilterState.Green:
                return hasGreenLens;
            default:
                return false;
        }
    }
}
