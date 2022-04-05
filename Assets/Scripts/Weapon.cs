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
    public float offset = -45f;

    private bool isEmbedded;
    private bool isInMotion;
    private bool canPickUp;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        marks = new List<Mark>() {new BasicMark(10, "basic mark")};
    }

    public void SetRotation(Vector2 lookDir)
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(lookDir.y, lookDir.x) + offset);
    }

    public void Throw(Vector2 direction)
    {
        Debug.Log("Throw weapon!");
        SFXManager.Play("Throw");
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
    public void Drop()
    {
        isEmbedded = false;
        canPickUp = true;
        isInMotion = false;
    }

    private void StopThrow()
    {
        _rb.velocity = Vector2.zero;
        //canPickUp = true;
        isInMotion = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(canPickUp && other.CompareTag("Player"))
        {
            Debug.Log("Pick up");
            if (other.TryGetComponent<WeaponHolder>(out var player))
                player.PickUpWeapon(this);
            else
                Debug.LogWarning("WTF");
        }
        
        if(!isInMotion)
            return;
        
        if (other.CompareTag("Wall"))
        {
            canPickUp = true;
            StopThrow();
        }
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            StopThrow();
            enemy.HitByWeapon(this);
            isEmbedded = true;
        }
    }

    public bool CanPickUp() => canPickUp;
    public bool IsEmbedded() => isEmbedded;
    public bool IsInMotion() => isInMotion;
}
