using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 50; // Enemy's health

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject); // Remove enemy when health drops to 0
        }
    }
}

