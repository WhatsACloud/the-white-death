using UnityEngine;
using System;
using TMPro; // Required for TextMeshPro

public class LeaderboardController : MonoBehaviour
{
    private TextMeshProUGUI leaderboardText;
    public int entryLength = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leaderboardText=GetComponent<TextMeshProUGUI>();
        string history = PlayerPrefs.GetString("History");
        string[] entries = history.Split(',');
        //time then index because sorting order.
        Tuple<long, long>[] runs = new Tuple<long, long>[entries.Length-1];
        for (int i=0;i<(entries.Length-1);i++){
            runs[i] = new Tuple<long, long>(long.Parse(entries[i]),i); // 0 is the first run ever etc
        }

        Array.Sort(runs);
        leaderboardText.text="";
        for (int i=0;i<(entries.Length-1) && i<entryLength;i++){
            Tuple<long, long> run=runs[i];
            long time = run.Item1;
            leaderboardText.text += (i+1).ToString() + ". " + (((float)time)/1000-(((float)time)%10/1000)).ToString()+"s";
            leaderboardText.text += " (run #" + (run.Item2+1).ToString() + ")";
            leaderboardText.text += "\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
