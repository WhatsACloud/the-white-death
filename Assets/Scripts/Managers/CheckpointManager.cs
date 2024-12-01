using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Vector3 respawnPosition;

    void Start()
    {
        // Set initial respawn position to the player's starting position
        respawnPosition = FindFirstObjectByType<PlayerAttack>().transform.position;
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        respawnPosition = checkpointPosition;
    }

    public void RespawnPlayer()
    {
        PlayerAttack player = FindFirstObjectByType<PlayerAttack>();
        player.transform.position = respawnPosition;
        player.ResetHealth(); // Implement this in PlayerAttack to reset health
    }
}

