using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [System.Serializable]
    public struct FlowMusic
    {
        public AudioClip introClip;  // The intro track for the flow state
        public AudioClip loopClip;   // The looping track for the flow state
    }

    [Header("Flow Music Settings")]
    public FlowMusic[] flowMusics;    // Array of music tracks for each flow state
    public AudioSource loopSource;    // AudioSource for looping tracks
    public AudioSource otherLoopSource;    
    public float fadeDuration = 0.70f;   // Duration of crossfade in seconds

    private int currentFlowState = -1; // Tracks the current flow state (-1 = no state)
    private Coroutine crossfadeCoroutine; // Tracks the current crossfade coroutine

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist this manager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLevelMusic(int flowState)
    {
        if (flowState < 0 || flowState >= flowMusics.Length)
            return; // Invalid state

        currentFlowState = flowState;

        // Play the intro music
        FlowMusic initialMusic = flowMusics[flowState];
        loopSource.clip = initialMusic.loopClip;
        PlayLoopMusic();
    }

    private void PlayLoopMusic()
    {
        loopSource.Play();
        loopSource.loop = true;
    }

    public void ChangeMusicForFlowState(int flowState)
    {
        if (flowState == currentFlowState || flowState < 0 || flowState >= flowMusics.Length)
            return; // Ignore if already in this state or invalid state

        // Get the new flow state's music
        FlowMusic newMusic = flowMusics[flowState];

        // Note: Two should never happen at the same time.
        // Start the new crossfade
        crossfadeCoroutine = StartCoroutine(CrossfadeToNewLoop(newMusic));
    }

    private System.Collections.IEnumerator CrossfadeToNewLoop(FlowMusic newMusic)
    {
        // loopSource is where the new track will play.
        (loopSource, otherLoopSource) = (otherLoopSource, loopSource);
        float elapsedTime = 0f;

        // Start new loop
        loopSource.clip = newMusic.loopClip;
        loopSource.Play();
        loopSource.time = otherLoopSource.time; // Sync AFTER to make up for latency
        loopSource.loop = true;
        loopSource.volume = 0f;

        while (elapsedTime < fadeDuration)
        {
            // fade out previous
            otherLoopSource.volume = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            // fade in new
            loopSource.volume = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        loopSource.volume = 1f;
        // Stop old loop
        otherLoopSource.volume = 0f;
        otherLoopSource.Stop();
    }
}

