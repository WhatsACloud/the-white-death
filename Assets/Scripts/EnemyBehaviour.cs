using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectilePrefab;   // Assign the projectile prefab in the Inspector
    public Transform firePoint;           // Point where projectiles spawn
    public float fireInterval = 2f;       // Time between shots

    private Transform player;             // Reference to the player
    private float fireTimer;              // Timer to track when to shoot

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
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
            // Instantiate projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // Calculate direction to the player
            Vector2 direction = player.position - firePoint.position;

            // Set projectile direction
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.SetDirection(direction);
            }
        }
    }
}

