using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public bool open;
    public Behaviour[] turnOffOnOpen;
    public TilemapRenderer renderer;

    public void Close()
    {
        foreach (var behaviour in turnOffOnOpen)
        {
            behaviour.enabled = true;
        }

        renderer.enabled = true;
        open = false;
    }

    public void Open()
    {
        foreach (var behaviour in turnOffOnOpen)
        {
            behaviour.enabled = false;
        }
        
        renderer.enabled = false;
        open = true;
    }

    private void Start()
    {
        if (open)
            Open();
        else
            Close();
    }
}
