using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameScreen : MonoBehaviour
{

    public void ShowScreen()
    {
        Pause();
        GetComponent<Canvas>().enabled = true;
    }
    
    public void HideScreen()
    {
        Pause();
        GetComponent<Canvas>().enabled = false;
    }

    public static void Pause()
    {
        Time.timeScale = 0;
        MusicManager.currentSong?.source.Pause();
    }

    public static void UnPause()
    {
        Time.timeScale = 1;
        MusicManager.currentSong?.source.UnPause();
    }
}
