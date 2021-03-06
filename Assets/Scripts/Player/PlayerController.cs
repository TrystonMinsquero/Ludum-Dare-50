using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public Vector2 MoveInput { get; private set; }

    public Vector2 LookInput { get; private set; }

    public bool DashInput { get; private set; }
    public bool ThrowInput { get; private set; }
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
            Vector2 mousePos = mainCam.ScreenToViewportPoint(Mouse.current.position.ReadValue());
            LookInput =  (mousePos - (Vector2.one * .5f)).normalized;
        }
        else
            LookInput = ctx.ReadValue<Vector2>();
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        DashInput = ctx.performed;
    }
    public void Throw(InputAction.CallbackContext ctx) {ThrowInput = ctx.performed;}
    public void Activate(InputAction.CallbackContext ctx) {ActivateInput = ctx.performed;}


    private void Update()
    {
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
