using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public abstract class Room : MonoBehaviour
{
    public List<Enemy> enemies;
    public Door[] doors;
    public bool completed = false;
    private Light2D[] _lights;
    
    
    public event Action<Room> EnteredRoom = delegate(Room room) {  };
    public event Action<Room> CompletedRoom = delegate(Room room) {  };

    public void TurnOn(Transform target = null)
    {
        SetLightsActive(true);
        CheckForRoomCompletion();
        
        if (completed)
            return;

        foreach(Door door in doors)
            door.Close();
        foreach (var enemy in enemies)
        {
            enemy.SetTarget(target);
        }
    }

    private void Update()
    {
        if(!completed)
            if (enemies == null || enemies.Count <= 0)
            {
                CompleteRoom();
            }
    }

    protected void CheckForRoomCompletion()
    {
        if(enemies == null || enemies.Count <= 0)
            CompleteRoom();
    }

    private void EnemyDied(Enemy enemy)
    {
        enemy.EnemyDied -= EnemyDied;
        enemies.Remove(enemy);
        for(int i = enemies.Count - 1; i >= 0; i--)
            if (enemies[i] == null || enemies[i].isDying)
                enemies.RemoveAt(i);
        CheckForRoomCompletion();
    }

    public void CompleteRoom()
    {
        foreach(Door door in doors)
            door.Open();
        SetLightsActive(true);
        
        completed = true;
        CompletedRoom.Invoke(this);
    }

    protected abstract void OnCompleteRoom();

    public void Awake()
    {
        _lights = GetComponentsInChildren<Light2D>();
        foreach (Enemy enemy in enemies)
        {
            enemy.EnemyDied += EnemyDied;
        }
        
    }

    private void Start()
    {
        CheckForRoomCompletion();
        if(!completed)
            SetLightsActive(false);
    }

    private void EnterRoom(WeaponHolder player)
    {
        if(!player.weapon)
            player.PickUpWeapon(LevelManager.weapon);
        EnteredRoom.Invoke(this);
        TurnOn(player.transform);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<WeaponHolder>(out var player))
        {
            EnterRoom(player);
        }
    }

    public void SetLightsActive(bool enabled)
    {
        foreach (Light2D light in _lights)
        {
            light.enabled = enabled;
        }
    }

}
