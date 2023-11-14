using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Parameters
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f; // New sprint speed
    public float rotationSpeed = 100f;
    public float jumpForce = 5f;
    private bool isGrounded;

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
            HandleInput();
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

        // Rotation with q and e keys
        if (Input.GetButton("RotateLeft"))
        {
            RotatePlayer(-1);
        }
        else if (Input.GetButton("RotateRight"))
        {
            RotatePlayer(1);
        }

        // Jumping with space key
        CheckGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void RotatePlayer(float direction)
    {
        // Calculate the rotation amount based on direction and rotation speed
        float rotationAmount = direction * rotationSpeed * Time.deltaTime;

        // Apply rotation to the player
        transform.Rotate(Vector3.up, rotationAmount);
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
}
