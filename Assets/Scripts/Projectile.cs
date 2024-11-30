using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;          // Speed of the projectile
    public float lifespan = 5f;      // Time before the projectile auto-destroys
    public int dmgAmt = 1;

    private Vector2 direction;       // Direction the projectile travels

    void Start()
    {
        Destroy(gameObject, lifespan); // Auto-destroy after lifespan
    }

    void Update()
    {
        // Move the projectile in the given direction
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; // Ensure the direction is normalized
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerAttack player = collision.collider.GetComponent<PlayerAttack>();
            if (player != null && player.IsDashing())
            {
                Destroy(gameObject); // Destroy projectile if player is dashing
            }
            else
            {
                // Handle damage or other logic when player is hit and not dashing
                player.TakeDamage(dmgAmt);
                Destroy(gameObject);
            }
        }
        else if (collision.collider.CompareTag("Environment"))
        {
            Destroy(gameObject); // Destroy on hitting walls or other obstacles
        }
    }
}

