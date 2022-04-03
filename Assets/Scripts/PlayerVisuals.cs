using System;
using System.Collections;
using UnityEngine;


public class PlayerVisuals : MonoBehaviour
{
    public TrailRenderer trail;
    private PlayerController _controller;
    private PlayerMovement _pm;
    private SpriteRenderer _sr;
    private Animator _anim;

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
        _anim = GetComponent<Animator>();
        _pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_controller.LookInput.x > 0 && _sr.flipX == true)
            _sr.flipX = false;
        else if (_controller.LookInput.x < 0 && _sr.flipX == false)
            _sr.flipX = true;
        
        float x = _pm.GetDirectionVector().x;
        if (x > 0 && _sr.flipX == true)
            _sr.flipX = false;
        else if (x < 0 && _sr.flipX == false)
            _sr.flipX = true;

        if(trail)
            trail.enabled = _pm.IsDashing();
        
        
        SetAnimation();
    }

    public void SetAnimation()
    {
        var pm = GetComponent<PlayerMovement>();
        Direction dir = pm.GetDirection();
        // Debug.Log("dir: " + dir);
        string stateName = "viking";
        if (pm.IsDashing())
            stateName += "Dash";
        else if (_controller.MoveInput.magnitude > .1f)
            stateName += "Walk";
        else
            stateName += "Idle";

        switch (dir)
        {
            case Direction.UP:
                stateName += "Up";
                break;
            case Direction.RIGHT:
                stateName += "Side";
                break;
            case Direction.LEFT:
                stateName += "Side";
                break;
            case Direction.DOWN:
                stateName += "Down";
                break;
            case Direction.UP_RIGHT:
                stateName += "Up";
                break;
            case Direction.UP_LEFT:
                stateName += "Up";
                break;
            case Direction.DOWN_RIGHT:
                stateName += "Down";
                break;
            case Direction.DOWN_LEFT:
                stateName += "Down";
                break;
        }

        if (_controller.MoveInput.magnitude > .1f &&
            (dir == Direction.UP_RIGHT || dir == Direction.UP_LEFT || dir == Direction.DOWN_RIGHT ||
             dir == Direction.DOWN_LEFT))
            stateName += "Side";
            

        _anim.Play(stateName);

    }

}
