using UnityEngine;
using Unity.Cinemachine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 50; // Enemy's health

    public ParticleSystem explosionEffect;
    private Unity.Cinemachine.CinemachineImpulseSource impulseSource;

    public void Start()
    {
        impulseSource = GetComponent<Unity.Cinemachine.CinemachineImpulseSource>();

    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            FindFirstObjectByType<FlowManager>().GainFlow(25);
            explosionEffect.transform.parent = null; // Detach to play independently
            explosionEffect.Play();

            impulseSource.GenerateImpulse();

            SFXManager.Instance.PlaySFX("enemyDeath");

            Destroy(gameObject); // Remove enemy when health drops to 0
        }
    }
}

