using UnityEngine;
using UnityEngine.UI;  // For accessing Button component
using UnityEngine.SceneManagement; // Required for scene management

public class StartScreenManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Button playButton;
    private Button leaderboardButton;
    void Update()
    {
    }

    // Update is called once per frame
    void Start()
    {   
        SceneManager.sceneLoaded += OnSceneLoaded;
        Rebind();
    }

    void Rebind(){
        leaderboardButton = GameObject.Find("LeaderboardButton")?.GetComponent<Button>();
        playButton = GameObject.Find("PlayButton")?.GetComponent<Button>();
        leaderboardButton.onClick.AddListener(OnLeaderboardButtonClick);
        playButton.onClick.AddListener(OnPlayButtonClick);
    }

    // Example method to handle button click
    void OnLeaderboardButtonClick()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // You can put the code you want to run when the scene changes here
        Rebind();
    }
    void OnPlayButtonClick()
    {
        SceneManager.LoadScene("MainGame");
        GameObject sceneManager = GameObject.Find("SceneManager");
        Destroy(sceneManager);
    }
}
