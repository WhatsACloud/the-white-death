using UnityEngine;
using TMPro;
using System;

public class TimerController : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private long referenceTime = 0;
    public bool isRunning = false; // Track if the timer is running

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        ResetTimer(); // Initialize the timer 
    }

    /// <summary>
    /// Starts the timer.
    /// </summary>
    public void StartTimer()
    {
        if (!isRunning)
        {
            referenceTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            isRunning = true;
        }
    }

    /// <summary>
    /// Stops the timer.
    /// </summary>
    public void StopTimer()
    {
        isRunning = false;
        timerText.text = "";
    }

    /// <summary>
    /// Resets the timer.
    /// </summary>
    public void ResetTimer()
    {
        referenceTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        timerText.text = "0.0s";
        isRunning = false;
    }

    /// <summary>
    /// Returns the current time elapsed in milliseconds.
    /// </summary>
    public long Now()
    {
        return isRunning
            ? (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - referenceTime
            : 0; // Return 0 if the timer is not running
    }

    void Update()
    {
        if (isRunning)
        {
            long time = Now();
            // Calculate seconds and milliseconds
            float seconds = (float)time / 1000;
            float milliseconds = (float)(time % 1000) / 1000;

            // Format to show precise time, e.g., "12.345s"
            timerText.text = $"{seconds + milliseconds:F3}s"; // 3 decimal places
        }
    }
}

