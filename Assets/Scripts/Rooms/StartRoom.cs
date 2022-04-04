using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class StartRoom : Room
{
    public int timeAdded = 10;
    public Transform spawnPoint;
    public bool absoluteStart;
    public TutorialGuide guide;
    public string songName;

    private bool leftRoom;

    protected override void OnCompleteRoom()
    {
        if(!absoluteStart)
            LevelManager.player.AddTime(timeAdded);
        leftRoom = true;
    }

    protected override void OnEnterRoom()
    {
        base.OnEnterRoom();
        MusicManager.Play(songName);
        // Debug.Log("Entered start room");
        if(guide)
            guide.Speak();
    }

    private void Update()
    {
        if (absoluteStart && !leftRoom)
            LevelManager.player.SetTime(LevelManager.player.startTime);
    }

    public void Spawn(Transform player)
    {
        player.position = spawnPoint.position;
    }
}
