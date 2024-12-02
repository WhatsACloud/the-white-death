using UnityEngine;

public class TitleMusicManager : MonoBehaviour
{
    MusicManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindFirstObjectByType<MusicManager>();
        manager.StartLevelMusic(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
