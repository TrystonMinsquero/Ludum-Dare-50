using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Room : MonoBehaviour
{
    public List<Enemy> enemies;
    public Door[] doors;
    public bool completed = false;
    private Light2D[] _lights;
    
    
    public event Action<Room> EnteredRoom = delegate(Room room) {  };
    public event Action<Room> CompletedRoom = delegate(Room room) {  };

    public void TurnOn()
    {
        SetLightsActive(true);
        
        if (completed)
            return;

        foreach(Door door in doors)
            door.Close();
    }

    private void Update()
    {
        if(!completed)
            if (enemies == null || enemies.Count <= 0)
            {
                CompleteRoom();
            }
    }

    public void CompleteRoom()
    {
        foreach(Door door in doors)
            door.Open();
        SetLightsActive(true);
        
        completed = true;
        CompletedRoom.Invoke(this);
    }

    public void Start()
    {
        _lights = GetComponentsInChildren<Light2D>();
        
        if(completed)
            CompleteRoom();
        else
            SetLightsActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log($"{name} Entered");
            EnteredRoom.Invoke(this);
            TurnOn();
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
