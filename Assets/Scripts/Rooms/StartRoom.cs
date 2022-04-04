using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class StartRoom : Room
{
    [TextArea(2,4)]
    public string tutorialText;
    public int timeAdded = 10;
    public Transform spawnPoint;
    public bool absoluteStart;

    protected override void OnCompleteRoom()
    {
        LevelManager.player.AddTime(timeAdded);
    }

    protected override void OnEnterRoom()
    {
        base.OnEnterRoom();
        Debug.Log(tutorialText);
    }

    private void Update()
    {
        if (absoluteStart)
            LevelManager.player.SetTime(LevelManager.player.startTime);
    }

    public void Spawn(Player player)
    {
        player.transform.position = spawnPoint.position;
    }
}
