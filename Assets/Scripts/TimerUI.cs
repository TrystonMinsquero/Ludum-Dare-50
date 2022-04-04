using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Gradient gradient;
    public Image clock;
    public Text timeText;
    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        if(!LevelManager.player)
            return;
        TimeSpan time = TimeSpan.FromSeconds(LevelManager.player.TimeOfDeath - Time.time);
        if(timeText)
            timeText.text = time.TotalSeconds > 60 ? time.ToString("mm':'ss") : GetMillisecondTime(time);
        else
            Debug.LogWarning("Need to assign time text");
    }
    
    
    private static string GetMillisecondTime(TimeSpan time)
    {
        string ms = time.Milliseconds.ToString();
        if (ms.Length >= 2)
            ms = ms.Substring(0,2);
        else if (ms.Length == 1)
            ms += "0";
        else if (ms.Length == 0)
            ms = "00";
        return time.ToString("ss'.'") + ms;
    }
}

