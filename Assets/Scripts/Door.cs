using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public bool open;
    private Behaviour[] _turnOffOnOpen;
    private TilemapRenderer _renderer;

    public void Close()
    {
        foreach (var behaviour in _turnOffOnOpen)
        {
            behaviour.enabled = true;
        }

        _renderer.enabled = true;
        open = false;
    }

    public void Open()
    {
        if(_turnOffOnOpen != null)
            foreach (var behaviour in _turnOffOnOpen)
            {
                behaviour.enabled = false;
            }
        else
            Debug.LogWarning("This door has no door!?!");
        
        _renderer.enabled = false;
        open = true;
    }

    private void Awake()
    {
        _turnOffOnOpen = new Behaviour[1];
        _turnOffOnOpen[0] = GetComponent<TilemapCollider2D>();
        _renderer = GetComponent<TilemapRenderer>();
    }
}
