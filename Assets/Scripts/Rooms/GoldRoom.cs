using System;

public class GoldRoom : Room
{
    public int timeAdded = 30;
    
    protected override void OnCompleteRoom()
    {
        LevelManager.player.AddTime(timeAdded);
    }
}
