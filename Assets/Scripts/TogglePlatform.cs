using UnityEngine;

public class TogglePlatform : MonoBehaviour
{
    private bool togglePlatforms;

    [SerializeField]
    private GameObject blackPlatform, redPlatform;
    [SerializeField]
    private Camera mainCamera;

    private Color blackPlatformBackgroundColor = Color.black;
    private Color redPlatformBackgroundColor = Color.red;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Switch the boolean from true to false each button press;
            togglePlatforms = !togglePlatforms;

            //Enable the black platform when the boolean is false, disable when true
            blackPlatform.SetActive(!togglePlatforms);
            //Enable the red platform when the boolean is true, disable when false
            redPlatform.SetActive(togglePlatforms);

            // Set the camera background color based on the active platforms
            mainCamera.backgroundColor = togglePlatforms ? redPlatformBackgroundColor : blackPlatformBackgroundColor;
        }
    }
}
