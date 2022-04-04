using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BlueRoom : Room
{
    public int timeAdded = 10;

    protected override void OnCompleteRoom()
    {
        LevelManager.player.AddTime(timeAdded);
    }
}
