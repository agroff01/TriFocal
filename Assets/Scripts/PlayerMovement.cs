using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Player Parameters
    private bool canControl = true;
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float rotationSpeed = 100f;
    public float jumpForce = 5f;
    private bool isGrounded;

    public bool isFalling = false;
    private float fallTime = 0f;
    public float maxFallTime = 3f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Only handle input if the object is active
        if (gameObject.activeSelf)
        {
            if(canControl)
            {
                HandleInput();
                CheckFalling();
            }
        }
    }

    void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Check if the shift key is held down to sprint and if the player is grounded
        bool isSprinting = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && isGrounded;

        // Choose the speed based on whether the player is sprinting
        float currentMoveSpeed = isSprinting ? sprintSpeed : moveSpeed;

        // Get the player's local forward and right directions
        // Prevents keys from flipping when player's orientation changes.
        Vector3 forward = transform.forward * vertical;
        Vector3 right = transform.right * horizontal;

        // Calculate the movement direction based on the player's local space
        Vector3 movement = (forward + right).normalized;

        // Apply movement to the player with the current speed
        rb.velocity = new Vector3(movement.x * currentMoveSpeed, rb.velocity.y, movement.z * currentMoveSpeed);

        // Jumping with space key
        CheckGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void CheckGrounded()
    {
        RaycastHit hit;
        float rayLength = 2f;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void CheckFalling()
    {
        if (!isGrounded)
        {
            isFalling = true;
            fallTime += Time.deltaTime;

            // Check if the player has been falling for too long
            if (fallTime >= maxFallTime)
            {
                // Call a function to handle the game over logic
                GameOver();
            }
        }
        else
        {
            // Reset falling variables if the player is grounded
            isFalling = false;
            fallTime = 0f;
        }
    }

    void GameOver()
    {
        // Enable the cursor before loading the GameOver scene
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("GameOver");
    }
    private void OnTriggerEnter(Collider other)
    {
        TogglePlatform togglePlatformScript = FindObjectOfType<TogglePlatform>();
        // Check if the collided object has the "RedLens" tag
        if (other.CompareTag("RedLens"))
        {
            if (togglePlatformScript != null)
            {
                togglePlatformScript.CollectRedLens();
            }
            // Destroy the RedLens 
            Destroy(other.gameObject); 
        }

        // Check if the collided object has the "BlueLens" tag
        if (other.CompareTag("BlueLens"))
        {
            if (togglePlatformScript != null)
            {
                togglePlatformScript.CollectBlueLens();
            }
            // Destroy the BlueLens 
            Destroy(other.gameObject);
        }
    }

    public void SetPlayerControl(bool enableControl)
    {
        canControl = enableControl;
    }
}
