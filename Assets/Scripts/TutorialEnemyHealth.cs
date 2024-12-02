using UnityEngine;

public class TutorialEnemyHealth : EnemyHealth
{
    public GameObject walls;

    public void DisableWalls()
    {
        walls.SetActive(false);
    }
    private void StartGame()
    {
        FindFirstObjectByType<TimerController>().StartTimer();
        FindFirstObjectByType<SpawningManager>().StartSpawning();
        DisableWalls();
    }

    public override void TakeDamage(int damage)
    {
        this.BaseTakeDamage(damage);
        StartGame();
    }
}
