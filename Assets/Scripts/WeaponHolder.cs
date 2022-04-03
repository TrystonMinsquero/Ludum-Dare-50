using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Weapon weapon;
    private PlayerController _controller;
    public bool HasWeapon {get{return weapon!=null;}}
    public Transform weaponSpot;

    private Vector2 weaponPosition;
    public float throwSpeed;
    public float rotateSpeed;


    private void ThrowWeapon(Vector2 lookDir)
    {
        Debug.Log("Throw weapon!");
        if (!HasWeapon)
            return;
    }

    public void PickUpWeapon(Weapon weapon)
    {
        if(weapon == null)
            return;
        this.weapon = weapon;
        this.weapon.transform.localPosition = weaponPosition;
    }

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        weaponPosition = weaponSpot.localPosition;

    }

    private void Update()
    {
        if(_controller.ThrowInput)
            ThrowWeapon(_controller.LookInput);
        if(!HasWeapon)
            return;
        weapon.SetRotation(_controller.LookInput);

        Vector3 position = weaponPosition;
        if (_controller.LookInput.x < 0)
            position.x = -weaponPosition.x;
        weapon.transform.localPosition = position;
    }
}
