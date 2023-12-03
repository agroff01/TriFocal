using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotate = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is paused
        if (Time.timeScale == 0) {
            return;
        }
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraVerticalRotate -= inputY;
        cameraVerticalRotate = Mathf.Clamp(cameraVerticalRotate, -70f, 70f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotate;

        Player.Rotate(Vector3.up * inputX);
    }
    // credit to https://youtu.be/5Rq8A4H6Nzw?si=Dv_SeG5XQdhqJjbU
}
