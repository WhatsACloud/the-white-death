using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float dashSpeed = 10f;         // Speed of the dash
    public float dashDuration = 0.2f;    // Duration of the dash
    public float minSwipeDistance = 5f;  // Minimum swipe distance in pixels

    private Vector2 swipeStart;          // Start position of swipe
    private bool isDashing = false;      // Shared flag with PlayerController
    private bool canDash = true;         // Prevents continuous dashing
    private Vector2 dashDirection;       // Direction of the dash
    private float dashTimer;             // Timer to track dash duration
    private Rigidbody2D rb;              // Reference to Rigidbody2D

    private PlayerController playerController; // Reference to PlayerController

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>(); // Access PlayerController
    }

    void Update()
    {
        // Detect swipe and initiate dash
        if (Input.GetMouseButtonDown(0) && canDash && !isDashing)
        {
            swipeStart = Input.mousePosition; // Record start position of swipe
        }

        if (Input.GetMouseButton(0) && canDash && !isDashing)
        {
            DetectSwipe();
        }

        // Reset canDash when the mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            canDash = true; // Allow new dashes after release
        }
    }

    void DetectSwipe()
    {
        Vector2 swipeEnd = Input.mousePosition; // Use current mouse position
        float swipeDistance = Vector2.Distance(swipeStart, swipeEnd); // Calculate distance

        if (swipeDistance >= minSwipeDistance)
        {
            // Calculate swipe direction and initiate dash
            Vector2 swipeVector = (swipeEnd - swipeStart).normalized;
            dashDirection = swipeVector; // Assign direction
            StartDash();
        }
    }

    void StartDash()
    {
        isDashing = true;
        canDash = false; // Disable dashing until mouse is released
        playerController.isDashing = true; // Notify PlayerController
        dashTimer = dashDuration;

        // Set linearVelocity in the dash direction
        rb.linearVelocity = dashDirection * dashSpeed;
    }

    void FixedUpdate()
    {
        // Handle dash timer and stop dashing after duration
        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;

            if (dashTimer <= 0)
            {
                isDashing = false;
                playerController.isDashing = false; // Notify PlayerController
                rb.linearVelocity = Vector2.zero; // Reset linearVelocity after dash
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with an enemy during a dash
        if (isDashing && collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destroy the enemy
        }
    }
}

