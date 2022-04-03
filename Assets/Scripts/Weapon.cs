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
    }

    public void SetRotation(Vector2 lookDir)
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(lookDir.y, lookDir.x) - 45);
    }

    public void Throw(Vector2 direction)
    {
        StartCoroutine(Throwing(direction));
    }

    private IEnumerator Throwing(Vector2 direction)
    {
        isInMotion = true;
        
        while (isInMotion)
        {
            yield return null;
        }

        canPickUp = true;

    }

    private void StopThrow()
    {
        
    }

    private void OnTriggerEnter2D(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            
        }
    }
}
