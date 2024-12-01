using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;          // Speed of the projectile
    public float lifespan = 5f;      // Time before the projectile auto-destroys
    public int dmgAmt = 1;

    private Vector2 direction;       // Direction the projectile travels

    void Start()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.isKinematic = true; 
        }
        Destroy(gameObject, lifespan); // Auto-destroy after lifespan
        // gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Collider2D>().isTrigger = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerAttack player = collision.GetComponent<PlayerAttack>();
            if (player != null)
            {
                player.TakeDamage(dmgAmt);
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Environment"))
        {
            Destroy(gameObject); // Destroy on hitting walls or other obstacles
        }
    }
}

