using UnityEngine;
using TMPro; // Required for TextMeshPro
using System;

public class TimerController : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    public long referenceTime=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText=GetComponent<TextMeshProUGUI>();
        referenceTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }

    // Update is called once per frame
    void Update()
    {
        long time = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond)-referenceTime;
        timerText.text = (((float)time)/1000-(((float)time)%10/1000)).ToString()+"s";
    }
}
