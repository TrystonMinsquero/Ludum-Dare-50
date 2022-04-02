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

    public void TurnOn()
    {
        if (completed)
            return;
        
        foreach(Door door in doors)
            door.Close();
        
        SetLightsActive(true);
    }

    private void Update()
    {
        // if(!completed)
        //     if(enemies == null || enemies.Count <= 0)
        //         CompleteRoom();
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
        _lights = GetComponentsInChildren<Light2D>();
        Debug.Log(_lights.Length);
        
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
        foreach (Light2D light in _lights)
        {
            light.enabled = enabled;
        }
    }
}
