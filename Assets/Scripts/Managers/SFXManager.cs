using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance; // Singleton instance for global access

    [Header("Sound Effects")]
    public List<SFXEntry> sfxEntries; // List to populate in the Inspector

    private Dictionary<string, AudioClip> sfxDictionary; // Internal dictionary
    public AudioSource audioSource; // Assignable AudioSource for playing SFX

    [System.Serializable]
    public class SFXEntry
    {
        public string key;
        public AudioClip clip;
    }

    //void Update()
    //{
    //    if (audioSource.isPlaying)
    //    {
    //        Debug.Log("AudioSource is playing a sound!");
    //    }
    //    else
    //    {
    //        Debug.Log("AudioSource is NOT playing anything.");
    //    }
    //}

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Check for assigned AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned to SFXManager. Please assign it in the Inspector!");
        }

        // Initialize the dictionary
        sfxDictionary = new Dictionary<string, AudioClip>();
        foreach (var entry in sfxEntries)
        {
            if (!sfxDictionary.ContainsKey(entry.key))
            {
                sfxDictionary.Add(entry.key, entry.clip);
            }
        }
    }

    /// <summary>
    /// Plays the SFX corresponding to the given key.
    /// </summary>
    /// <param name="key">The key of the sound effect.</param>
    public void PlaySFX(string key)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned to SFXManager!");
            return;
        }

        if (sfxDictionary.TryGetValue(key, out var clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX with key '{key}' not found!");
        }
    }
}

