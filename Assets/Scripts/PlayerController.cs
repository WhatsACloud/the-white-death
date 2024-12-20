using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of the player
    public float baseMoveSpeed = 5f;  // Default movement speed
    public float baseSlashRange = 2f; // Default slash range
    public float slashRange;         // Current slash range

    private Rigidbody2D rb;       // Rigidbody for physics movement
    private Vector2 movement;     // Direction of movement
    public bool isDashing = false; // Shared flag with PlayerAttack
    public bool isMoving = false;  // Flag for WASD movement


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = baseMoveSpeed;
        slashRange = baseSlashRange;
    }

    public void SetStats(float speedMultiplier, float rangeMultiplier)
    {
        moveSpeed = baseMoveSpeed * speedMultiplier;
        slashRange = baseSlashRange * rangeMultiplier;
    }

    void FixedUpdate()
    {
        // Check input for WASD movement only if not dashing
        if (!isDashing)
        {
            //float moveX = Input.GetAxisRaw("Horizontal");
            //float moveY = Input.GetAxisRaw("Vertical");

            //movement = new Vector2(moveX, moveY).normalized;
            //isMoving = movement.magnitude > 0; // Check if there's movement
        }
        if (!isDashing)
        {
            //if (isMoving)
            //    rb.linearVelocity = movement * moveSpeed;
            //else
                //rb.linearVelocity = Vector2.zero; // Stop movement when no input
        }
    }
}

