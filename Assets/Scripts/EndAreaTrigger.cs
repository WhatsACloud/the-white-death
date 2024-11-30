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
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

