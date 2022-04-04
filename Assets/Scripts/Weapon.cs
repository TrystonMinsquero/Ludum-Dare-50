using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<Mark> marks;
    
    public float throwSpeed;
    public float rotateSpeed;

    public Sprite heldImage;
    public Sprite thrownImage;

    private bool isInMotion;
    private bool canPickUp;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        marks = new List<Mark>() {new BasicMark()};
    }

    public void SetRotation(Vector2 lookDir)
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(lookDir.y, lookDir.x) - 45);
    }

    public void Throw(Vector2 direction)
    {
        Debug.Log("Throw weapon!");
        canPickUp = false;
        StartCoroutine(Throwing(direction));
    }

    private IEnumerator Throwing(Vector2 direction)
    {
        isInMotion = true;
        _rb.velocity = direction.normalized * throwSpeed;
        
        while (isInMotion)
        {
            yield return null;
        }
    }

    private void StopThrow()
    {
        _rb.velocity = Vector2.zero;
        //canPickUp = true;
        isInMotion = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!isInMotion)
            return;
        if(canPickUp && other.CompareTag("Player"))
        {
            Debug.Log("Pick up");
            if (other.TryGetComponent<WeaponHolder>(out var player))
                player.PickUpWeapon(this);
            else
                Debug.LogWarning("WTF");
        }
        if (other.CompareTag("Wall"))
        {
            canPickUp = true;
            StopThrow();
        }
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            StopThrow();
            enemy.HitByWeapon(this);
        }
    }

    public bool CanPickUp() => canPickUp;
}
