using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectilePrefab;   // Assign the projectile prefab in the Inspector
    public Transform firePoint;           // Point where projectiles spawn
    public float fireInterval = 2f;       // Time between shots

    private Transform playerPos;          // Reference to the player (the location)
    private GameObject player;            // Reference to player (object)
    private float fireTimer;              // Timer to track when to shoot

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform; // Find the player
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            FireProjectile();
            fireTimer = 0;
        }
    }

    void FireProjectile()
    {
        if (player != null)
        {
            // if (gameObject.GetComponent<Collider>() == null){
            //     gameObject.AddComponent<BoxCollider2D>();
            // }
            // Instantiate projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // projectile.AddComponent<BoxCollider2D>();
            // Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(),true);
            // projectile.GetComponent<Collider2D>().enabled = false;
            Vector2 direction = playerPos.position - firePoint.position;

            // Set projectile direction
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.SetDirection(direction);
            }
        }
    }
}

