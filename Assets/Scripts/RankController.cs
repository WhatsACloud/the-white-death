using UnityEngine;
using TMPro; // Required for TextMeshPro

public class RankController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private TextMeshProUGUI rankText;
    void Start()
    {
        rankText=GetComponent<TextMeshProUGUI>();
        rankText.text="";
        string history = PlayerPrefs.GetString("History");
        int entries = history.Split(',').Length-1;
        for (int n=0;n<entries;n++){
            rankText.text+=(n+1).ToString()+".\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
