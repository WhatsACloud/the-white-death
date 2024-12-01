using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class Playbutton: MonoBehaviour
{
    public void EnterGame()
    {
        SceneManager.LoadScene("MainGame");
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
