using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Weapon weapon;
    private PlayerController _controller;
    public bool HasWeapon {get{return weapon!=null;}}

    public float throwSpeed;
    public float rotateSpeed;

    private void ThrowWeapon(Vector2 lookDir)
    {
        Debug.Log("Throw weapon!");
        if (!HasWeapon)
            return;
    }

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(_controller.ThrowInput)
            ThrowWeapon(_controller.LookInput);
    }
}
