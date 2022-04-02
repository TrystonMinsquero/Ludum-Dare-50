using System;
using System.Collections;
using UnityEngine;

public enum Direction
{
    UP,
    UP_RIGHT,
    RIGHT,
    DOWN_RIGHT,
    DOWN,
    DOWN_LEFT,
    LEFT,
    UP_LEFT
}

public class PlayerVisuals : MonoBehaviour
{
    private PlayerController _controller;
    private SpriteRenderer _sr;
    public float Scale
    {
        get { return transform.localScale.y; }
        set { transform.localScale = new Vector3(value, value, value); }
    }

    private void SetFacingLeft(bool value)
    {
        
        if (value)
            transform.localScale = new Vector3(-Scale, Scale, Scale);
        else
            transform.localScale = new Vector3(Scale, Scale, Scale);
    }

    public void Start()
    {
        Scale = 1;
        _controller = GetComponent<PlayerController>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_controller.LookInput.x > 0 && _sr.flipX == true)
            _sr.flipX = false;
        else if (_controller.LookInput.x < 0 && _sr.flipX == false)
            _sr.flipX = true;
    }
}
