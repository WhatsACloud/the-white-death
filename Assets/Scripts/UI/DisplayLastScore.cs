using UnityEngine;
using TMPro;

public class DisplayLastScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // TMP text field to display the score

    void Start()
    {
        DisplaySecondLastScore();
    }

    void DisplaySecondLastScore()
    {
        string history = PlayerPrefs.GetString("History");
        // Split the history into an array
        string[] entries = history.Split(',');

        // Check if there are at least two values in the array
        if (entries.Length >= 2)
        {
            // Get the second-to-last value
            string secondLastScore = entries[entries.Length - 2];

            // Check if it's not empty or null
            if (!string.IsNullOrEmpty(secondLastScore))
            {
                long time = long.Parse(secondLastScore);
                scoreText.text = "Your last time: " + (((float)time)/1000-(((float)time)%10/1000)).ToString()+"s";
            }
        }
        else
        {
            // If no second-to-last value, clear the text or do nothing
            scoreText.text = "";
        }
    }
}

