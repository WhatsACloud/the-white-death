using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DistanceManager : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    public TMP_Text distanceText; // Assign a UI Text element in the Inspector
    public int maxDistance = 20; // Maximum distance for display
    public GameObject textPrefab;
    private TimerController timer;
    private bool gameFinished = false;
    private int dashCounter = 0;
    private TMP_Text gameOverText;
    public GameObject textContainer;
    private long time;

    void Start(){
        timer = FindFirstObjectByType<TimerController>();
    }
    void Update()
    { 
        float distance = player.position.y; 
        gameOverText = distanceText;
        if (player != null && distanceText != null && !gameFinished)
        {
            // Update the UI Text with the format "[Y-pos]/200m"
            distanceText.text = $"{Mathf.Round(distance)}/{maxDistance}m";
            if (distance < -10){
                distanceText.text += "\nThere is literally nothing on this side.";
            } else if (distance < -1.5){
                distanceText.text += "\nGo the other way :)";
            } 
        }
        if (distance > maxDistance && !gameFinished){ // text & dash thrice
            gameFinished = true;
            string history=PlayerPrefs.GetString("History");
            time = timer.Now();
            timer.StopTimer();
            history += time + ",";
            PlayerPrefs.SetString("History",history);
            Debug.Log(PlayerPrefs.GetString("History"));
            UpdateGameOverText();
        }
    }

    public void UpdateGameOverText(){
        // Calculate seconds and milliseconds

        gameOverText.text = $"You made it! \nIn just ${time/1000}.{time%1000}s too!\nDash {3-dashCounter} more time";
        if (dashCounter < 2){
            gameOverText.text += "s to end game.";
        } else {
            gameOverText.text += " to end game.";
        }
        gameOverText.fontSize = 20f;
    }
    public void DashCallback(){
        if (gameFinished){
            dashCounter++;
            UpdateGameOverText();
        }
        if (gameFinished && dashCounter == 3){
            EndGame();
        }
    }

    void EndGame(){
        //NOTE: PlayerPrefs.SetString("History","") to clear leaderboard etc.
        SceneManager.LoadScene("StartScreen");
        GameObject manager = GameObject.Find("Manager");
        Destroy(manager);
    }
}

