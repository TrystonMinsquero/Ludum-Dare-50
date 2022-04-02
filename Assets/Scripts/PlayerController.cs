using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Controls _controls;
     
    public Vector2 MoveInput { get; private set; }
    public bool ThrowInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool ActivateInput { get; private set; }
    
    // Start is called before the first frame update
    void Awake()
    {
        _controls = new Controls();
        _controls.Enable();
    }

    private void Update()
    {
        MoveInput = _controls.Gameplay.Movement.ReadValue<Vector2>();
        ThrowInput = _controls.Gameplay.Throw.triggered;
        DashInput = _controls.Gameplay.Dash.triggered;
        ActivateInput = _controls.Gameplay.Activate.triggered;
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
