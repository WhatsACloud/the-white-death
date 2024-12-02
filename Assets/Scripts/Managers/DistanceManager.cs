using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DistanceManager : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    public TMP_Text distanceText; // Assign a UI Text element in the Inspector
    public int maxDistance = 20; // Maximum distance for display

    void Start(){
    }
    void Update()
    { 
        float distance = Mathf.Abs(player.position.y); 
        if (player != null && distanceText != null)
        {
            // Update the UI Text with the format "[Y-pos]/200m"
            distanceText.text = $"{Mathf.Round(distance)}/{maxDistance}m";
        }
        if (distance > maxDistance){
            string history=PlayerPrefs.GetString("History");
            history+=FindFirstObjectByType<TimerController>().Now()+",";
            PlayerPrefs.SetString("History",history);
            Debug.Log(PlayerPrefs.GetString("History"));

            //NOTE: PlayerPrefs.SetString("History","") to clear leaderboard etc.

            SceneManager.LoadScene("StartScreen");
            GameObject manager = GameObject.Find("Manager");
            Destroy(manager);
        }
    }
}

