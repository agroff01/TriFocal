using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TogglePlatform : MonoBehaviour
{
    private bool hasRedLens = false;
    private bool hasBlueLens = false;

    private enum FilterState { Normal, Red, Blue }
    private FilterState currentFilterState = FilterState.Normal;

    [SerializeField]
    private GameObject normalPlatform, redPlatform, bluePlatform;

    [SerializeField]
    private PostProcessProfile normalProfile, redProfile, blueProfile;

    private void Update()
    {
        // Temp Toggle with keys
        if (Input.GetKeyDown(KeyCode.R) && hasRedLens)
        {
            ToggleFilter(FilterState.Red);
        }
        if (Input.GetKeyDown(KeyCode.B) && hasBlueLens)
        {
            ToggleFilter(FilterState.Blue);
        }
    }

    private void ToggleFilter(FilterState targetFilter)
    {
        // Check if the target filter is the same as the current filter
        if (targetFilter == currentFilterState)
        {
            // If so, toggle back to NormalFilter
            targetFilter = FilterState.Normal;
        }

        // Disable all platforms
        if (normalPlatform != null)
        {
            normalPlatform.SetActive(false);
        }
        if (redPlatform != null)
        {
            redPlatform.SetActive(false);
        }
        if (bluePlatform != null)
        {
            bluePlatform.SetActive(false);
        }

        // Apply post-processing profile based on the target filter
        PostProcessVolume postProcessVolume = Camera.main.GetComponent<PostProcessVolume>();

        if (postProcessVolume != null)
        {
            switch (targetFilter)
            {
                case FilterState.Normal:
                    postProcessVolume.profile = normalProfile;
                    normalPlatform.SetActive(true);
                    break;
                case FilterState.Red:
                    postProcessVolume.profile = redProfile;
                    redPlatform.SetActive(true);
                    break;
                case FilterState.Blue:
                    postProcessVolume.profile = blueProfile;
                    bluePlatform.SetActive(true);
                    break;
            }
        }

        currentFilterState = targetFilter;
    }

    // This method is called when the player collects the "RedLens"
    public void CollectRedLens()
    {
        hasRedLens = true;
        Debug.Log("RedLens collected! You can now toggle the red filter.");
    }

    // This method is called when the player collects the "BlueLens"
    public void CollectBlueLens()
    {
        hasBlueLens = true;
        Debug.Log("BlueLens collected! You can now toggle the blue filter.");
    }
}
