using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Enemy> enemies;
    public Door[] doors;
    public bool completed = false;
    private Light[] _lights;

    public void TurnOn()
    {
        if (completed)
            return;
        
        foreach(Door door in doors)
            door.Close();
        
        SetLightsActive(true);
    }

    public void CompleteRoom()
    {
        foreach(Door door in doors)
            door.Open();
        SetLightsActive(true);
        
        completed = true;
    }

    public void Start()
    {
        _lights = GetComponentsInChildren<Light>();
        
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
            TurnOn();
        }
    }

    private void SetLightsActive(bool enabled)
    {
        foreach (Light light in _lights)
        {
            light.enabled = enabled;
        }
    }
}
