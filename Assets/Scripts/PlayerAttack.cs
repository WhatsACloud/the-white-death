using UnityEngine;
using TMPro; // Required for TextMeshPro
using System.Collections;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float dashDeceleration = 5f; // Deceleration rate during the dash
    public float minSwipeDistance = 5f; // Minimum swipe distance in pixels
    public int maxHealth = 10;          // Maximum health of the player
    public int damage = 50;
    public float invincibilityDuration = 2f; // Duration of invincibility
    public float flashInterval = 0.1f;  // Interval for flashing effect

    private int currentHealth;           // Player's current health
    public TextMeshProUGUI healthTextTMP; // Reference to the TMP Text element

    private Vector2 swipeStart;          // Start position of swipe
    private bool isDashing = false;      // Tracks dashing state
    private bool canDash = true;         // Prevents continuous dashing
    private bool isInvincible = false;   // Tracks invincibility state
    private Vector2 dashDirection;       // Direction of the dash
    private Rigidbody2D rb;              // Reference to Rigidbody2D
    private SpriteRenderer spriteRenderer; // For flashing effect

    private PlayerController playerController; // Reference to PlayerController

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>(); // Access PlayerController
        spriteRenderer = GetComponent<SpriteRenderer>(); // For visual flashing
        currentHealth = maxHealth; // Initialize health
        UpdateHealthUI(); // Update health UI at the start
    }

    void Update()
    {
        // Detect swipe start position
        if (Input.GetMouseButtonDown(0) && canDash)
        {
            swipeStart = Input.mousePosition; // Record start position of swipe
        }

        // Execute dash on mouse release
        if (Input.GetMouseButtonUp(0) && canDash)
        {
            DetectSwipe();
            CenterMousePosition(); // Optional: Reset the cursor position
            canDash = true;       // Prevent immediate consecutive dashes
        }
    }

    private void SetMousePosition(Vector2 mousePos)
    {
        Mouse.current.WarpCursorPosition(mousePos);
    }

    void CenterMousePosition()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        // Lock and unlock the cursor to reposition it

        SetMousePosition(new Vector2((int)screenCenter.x, (int)screenCenter.y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with an enemy during a dash
        if (collision.collider.CompareTag("Enemy"))
        {
            // Get the EnemyHealth component
            EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Call TakeDamage with damage value
            }
        }

        if (collision.collider.CompareTag("Environment"))
        {
            // Move the player slightly out of the wall
            Vector2 collisionNormal = collision.contacts[0].normal;
            transform.position += (Vector3)(collisionNormal * 0.1f); // Adjust 0.1f as needed
            rb.linearVelocity = Vector2.zero; // Stop the dash
            isDashing = false;
        }

        // Handle collisions with projectiles or enemies outside of a dash
        if (!isInvincible && collision.collider.CompareTag("EnemyProjectile"))
        {
            TakeDamage(1); // Example: Take 1 damage from projectiles
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); // Update health display
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        FindFirstObjectByType<CheckpointManager>().RespawnPlayer();
    }

    void DetectSwipe()
    {
        Vector2 swipeEnd = Input.mousePosition; // Use current mouse position
        float swipeDistance = Vector2.Distance(swipeStart, swipeEnd); // Calculate distance

        if (swipeDistance >= minSwipeDistance && FlowManager.instance.CanDash())
        {
            // Calculate swipe direction and initiate dash
            Vector2 swipeVector = (swipeEnd - swipeStart).normalized;
            float dashLength = Mathf.Clamp(swipeDistance * 0.1f, 1f, playerController.slashRange); // Scale and clamp length
            dashDirection = swipeVector * dashLength; // Assign direction
            StartDash();
        }
    }

    void StartDash()
    {
        isDashing = true;
        //canDash = false; // Disable dashing until the current input is processed
        rb.linearVelocity = dashDirection; // Set initial velocity
    }

    void FixedUpdate()
    {
        // Handle deceleration during dashing
        if (isDashing)
        {
            if (rb.linearVelocity.magnitude > 0.1f)
            {
                // Gradually reduce velocity
                rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, dashDeceleration * Time.fixedDeltaTime);
            }
            else
            {
                isDashing = false; // End the dash when velocity is very low
                rb.linearVelocity = Vector2.zero; // Ensure velocity is completely stopped
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return; // Ignore damage if invincible

        currentHealth -= damage; // Reduce health
        UpdateHealthUI(); // Update health display
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // Handle player death
        }
        else
        {
            StartCoroutine(ActivateInvincibility(true, invincibilityDuration));
        }
    }

    private IEnumerator ActivateInvincibility(bool flash, float invincibilityTime)
    {
        isInvincible = true;
        float timer = 0f;

        if (flash)
        {
            while (timer < invincibilityTime)
            {
                // Toggle sprite visibility for flashing effect
                spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSeconds(flashInterval);
                timer += flashInterval;
            }
        }
        else
        {
            yield return new WaitForSeconds(invincibilityTime);
        }

        // Ensure the sprite is visible after flashing
        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    private void UpdateHealthUI()
    {
        healthTextTMP.text = "Health: " + currentHealth; // Update the TMP health text
    }

    public bool IsDashing()
    {
        return isDashing; // Return the current dashing state
    }
}

