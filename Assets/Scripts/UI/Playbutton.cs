using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class Playbutton: MonoBehaviour
{
    public void EnterGame()
    {
        SceneManager.LoadScene("MainGame");
        GameObject sceneManager = GameObject.Find("SceneManager");
        Destroy(sceneManager);
    }
    public void EnterLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
    public void EnterStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
