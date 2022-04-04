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
    public float timeForLevel = 120;

    private bool leftRoom;
    public static event Action<StartRoom> StartRoomEntered = delegate(StartRoom room) {  };
    public static StartRoom AbsoluteStartRoom;

    protected override void Awake()
    {
        base.Awake();
        if (absoluteStart)
            AbsoluteStartRoom = this;
    }

    protected override void OnCompleteRoom()
    {
        if(!absoluteStart)
            LevelManager.player.AddTime(timeAdded);
        leftRoom = true;
    }

    protected override void OnEnterRoom()
    {
        base.OnEnterRoom();
        // Debug.Log("Entered start room");
        if(guide)
            guide.Speak();
        if (!leftRoom)
        {
            StartRoomEntered.Invoke(this);
            MusicManager.Play(songName);
        }
    }

    private void Update()
    {
        if (absoluteStart && !leftRoom)
            LevelManager.player.SetTime(timeForLevel);
    }

    public void Spawn(Transform player)
    {
        player.position = spawnPoint.position;
        LevelManager.player.SetTime(timeForLevel);
        leftRoom = false;
    }
}
