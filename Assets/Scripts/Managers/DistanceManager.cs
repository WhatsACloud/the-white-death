using UnityEngine;
using TMPro;

public class DistanceManager : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    public TMP_Text distanceText; // Assign a UI Text element in the Inspector
    public int maxDistance = 200; // Maximum distance for display

    void Update()
    {
        if (player != null && distanceText != null)
        {
            // Get and round the player's Y position
            float playerY = Mathf.Round(player.position.y);

            // Update the UI Text with the format "[Y-pos]/200m"
            distanceText.text = $"{playerY}/{maxDistance}m";
        }
        else if (distanceText == null)
        {
            Debug.LogWarning("Distance Text UI is not assigned!");
        }
        else
        {
            Debug.LogWarning("Player is not assigned in DistanceManager!");
        }
    }
}

