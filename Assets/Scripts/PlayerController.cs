using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public Vector2 MoveInput { get; private set; }

    public Vector2 LookInput { get; private set; }

    public bool ThrowInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool ActivateInput { get; private set; }

    private Controls _controls;
    private Camera mainCam;
    private PlayerInput _playerInput;

    // Start is called before the first frame update
    void Awake()
    {
        _controls = new Controls();
        _controls.Enable();
        mainCam = Camera.main;
        _playerInput = GetComponent<PlayerInput>();

    }

    public void Move(InputAction.CallbackContext ctx) {MoveInput = ctx.ReadValue<Vector2>();}

    public void Look(InputAction.CallbackContext ctx)
    {
        if (_playerInput.currentControlScheme == "Keyboard & Mouse")
        {
            LookInput = (mainCam.ScreenToViewportPoint(Mouse.current.position.ReadValue()) - transform.position).normalized;
        }
        else
            LookInput = ctx.ReadValue<Vector2>();
    }
    public void Throw(InputAction.CallbackContext ctx) {ThrowInput = ctx.performed;}
    public void Dash(InputAction.CallbackContext ctx) {DashInput = ctx.performed;}
    public void Activate(InputAction.CallbackContext ctx) {ActivateInput = ctx.performed;}


    private void Update()
    {
        // MoveInput = _controls.Gameplay.Movement.ReadValue<Vector2>();
        // ThrowInput = _controls.Gameplay.Throw.triggered;
        // DashInput = _controls.Gameplay.Dash.triggered;
        // ActivateInput = _controls.Gameplay.Activate.triggered;
        // Debug.Log(mainCam.ScreenToViewportPoint(Mouse.current.position.ReadValue()) );
        if (_playerInput.currentControlScheme == "Keyboard & Mouse")
        {
            Vector2 mousePos = mainCam.ScreenToViewportPoint(Mouse.current.position.ReadValue());
            LookInput =  (mousePos - (Vector2.one * .5f)).normalized;
        }
           
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
