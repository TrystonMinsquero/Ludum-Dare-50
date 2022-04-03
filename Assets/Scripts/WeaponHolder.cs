using System;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Weapon weapon;
    public Transform weaponSpot;
    private PlayerController _controller;
    private Vector3 _weaponPosition;


    private void ThrowWeapon(Vector2 lookDir)
    {
        if (weapon == null)
            return;
        if (lookDir.magnitude > .1f)
        {
            weapon.Throw(lookDir);
            weapon = null;
        }   
    }

    public void PickUpWeapon(Weapon weapon)
    {
        if (!weapon.CanPickUp())
            return;
        
        if(weapon == null || this.weapon )
            return;
        this.weapon = weapon;
        this.weapon.transform.position = transform.position + _weaponPosition;
    }

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _weaponPosition = weaponSpot.localPosition;
        if (weapon.transform.parent == transform)
            weapon.transform.SetParent(null);
    }

    private void Update()
    {

        if (weapon == null)
        {
            //todo: remove this and fix regular collider
            var collisions = Physics2D.OverlapCircleAll(transform.position, .6f);
            foreach (var collider in collisions)
            {
                if (collider.TryGetComponent<Weapon>(out var weapon))
                    PickUpWeapon(weapon);
                Debug.Log(collider);
            }
            return;
        }
        
        
        if(_controller.ThrowInput)
            ThrowWeapon(_controller.LookInput);
        weapon?.SetRotation(_controller.LookInput);

        Vector3 position = _weaponPosition;
        if (_controller.LookInput.x < 0)
            position.x = -_weaponPosition.x;
        if(weapon)
            weapon.transform.position = transform.position + position;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.TryGetComponent<Weapon>(out var weapon))
            PickUpWeapon(weapon);
    }
}
