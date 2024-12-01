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
    public AudioSource introSource;   // AudioSource for playing intro tracks
    public AudioSource loopSource;    // AudioSource for looping tracks
    public float fadeDuration = 3f;   // Duration of crossfade in seconds

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
        introSource.clip = initialMusic.introClip;
        loopSource.clip = initialMusic.loopClip;

        introSource.Play();
        introSource.loop = false;

        // Schedule the loop to play after the intro finishes
        Invoke(nameof(PlayLoopMusic), initialMusic.introClip.length);
    }

    private void PlayLoopMusic()
    {
        introSource.Stop(); // Stop the intro source
        loopSource.Play();
        loopSource.loop = true;
    }

    public void ChangeMusicForFlowState(int flowState)
    {
        if (flowState == currentFlowState || flowState < 0 || flowState >= flowMusics.Length)
            return; // Ignore if already in this state or invalid state

        // Get the new flow state's music
        FlowMusic newMusic = flowMusics[flowState];

        // Record the current position of the loop
        float currentPlaybackPosition = loopSource.time;

        // Stop the previous crossfade if one is running
        if (crossfadeCoroutine != null)
        {
            StopCoroutine(crossfadeCoroutine);
        }

        // Start the new crossfade
        crossfadeCoroutine = StartCoroutine(CrossfadeToNewLoop(newMusic, currentPlaybackPosition));
    }

    private System.Collections.IEnumerator CrossfadeToNewLoop(FlowMusic newMusic, float playbackPosition)
    {
        // Fade out current music
        float elapsedTime = 0f;
        float loopVolume = loopSource.volume;

        while (elapsedTime < fadeDuration)
        {
            loopSource.volume = Mathf.Lerp(loopVolume, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        loopSource.volume = 0f;

        // Stop the current loop
        loopSource.Stop();

        // Switch to the new loop clip
        loopSource.clip = newMusic.loopClip;
        loopSource.time = playbackPosition; // Sync to the previous playback position
        loopSource.Play();
        loopSource.loop = true;

        // Fade in the new loop
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            loopSource.volume = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        loopSource.volume = 1f;
    }
}

