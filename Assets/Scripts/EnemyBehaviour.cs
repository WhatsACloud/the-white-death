using UnityEngine;

public class Enemy : MonoBehaviour
{
    // public GameObject projectilePrefab;   // Assign the projectile prefab in the Inspector
    // public Transform firePoint;           // Point where projectiles spawn
    // public float fireInterval = 2f;       // Time between shots

    private Transform playerPos;          // Reference to the player (the location)
    private GameObject player;            // Reference to player (object)
    // private float fireTimer;              // Timer to track when to shoot
    private float moveCountdown = 0;          // Timer to track when to move
    private Rigidbody2D rb;
    private bool awoken = false;               // Seen player yet

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (gameObject.GetComponent<Rigidbody2D>() == null){
            gameObject.AddComponent<Rigidbody2D>();
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerPos = player.transform; // Find the player
        if (PlayerInSight()){
            moveCountdown = 0f + Random.Range(0f, 0.2f);
        } else {
            moveCountdown = 2f;
        }
    }

    void Update()
    {
        // fireTimer += Time.deltaTime;
        moveCountdown -= Time.deltaTime;

        // if (fireTimer >= fireInterval)
        // {
        //     FireProjectile();
        //     fireTimer = 0;
        // }

        if (moveCountdown <= 0){
            moveCountdown = 0.2f + Random.Range(0f, 0.5f);
            Move();
        }

        if (rb.linearVelocity.magnitude > 0.1f){
            rb.linearVelocity *= 0.98f;
        } else {
            rb.linearVelocity = Vector2.zero;
        }
        if (!awoken && PlayerInSight()){
            awoken = true;
        }
    }
    bool LineIntersectsWall(Vector2 start, Vector2 end)
    {
        Vector2 direction = end - start;
        float distance = direction.magnitude;

        // Cast a ray from the start to the end position and get all hits along the way
        RaycastHit2D[] hits = Physics2D.RaycastAll(start, direction.normalized, distance);

        // Check all the hits to see if any are tagged with "Environment"
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Environment"))
            {
                return true; 
            }
        }

        return false; 
    }

    public bool PlayerInSight(){
        return !LineIntersectsWall(player.transform.position,gameObject.transform.position);
    }

    void Move(){
        // if (PlayerInSight()){
            Vector2 direction = (gameObject.transform.position - playerPos.position).normalized;
            rb.linearVelocity = (direction * (4f + Random.Range(0f, 3f)));
        // }
    }

    void FireProjectile()
    {
        // if (player != null && awoken)
        // {
        //     // if (gameObject.GetComponent<Collider>() == null){
        //     //     gameObject.AddComponent<BoxCollider2D>();
        //     // }
        //     // Instantiate projectile
        //     GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        //     // projectile.AddComponent<BoxCollider2D>();
        //     // Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(),true);
        //     // projectile.GetComponent<Collider2D>().enabled = false;
        //     Vector2 direction = playerPos.position - firePoint.position;

        //     // Set projectile direction
        //     Projectile projectileScript = projectile.GetComponent<Projectile>();
        //     if (projectileScript != null)
        //     {
        //         projectileScript.SetDirection(direction);
        //     }
        // }
    }
}

