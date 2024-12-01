using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class EndAreaTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if Player entered the area
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        // Load the next scene in the build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //NOTE: The following is the code to add the time to the leaderboard.
        //Copy this over to whatever trigger you have at the end of the game.
        string history=PlayerPrefs.GetString("History");
        history+=FindFirstObjectByType<TimerController>().Now()+",";
        PlayerPrefs.SetString("History",history);
        Debug.Log(PlayerPrefs.GetString("History"));

        //NOTE: PlayerPrefs.SetString("History","") to clear leaderboard etc.

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

