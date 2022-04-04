using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : InGameScreen
{
    public static GameOverUI Instance;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public static void ShowScreen()
    {
        ((InGameScreen) Instance).ShowScreen();
    }
    
    public static void HideScreen()
    {
        ((InGameScreen) Instance).HideScreen();
    }
    
    public void TryAgain()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Quit()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
