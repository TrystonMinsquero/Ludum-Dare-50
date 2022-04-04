using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _speed;
    private Vector2 _direction;
    private int _damage;
    public float maxTime = 10f;
    public float offset;
    
    public void Throw(Vector2 dir, RangedEnemy enemy)
    {
        _rb = GetComponent<Rigidbody2D>();
        _speed = enemy.projectTileSpeed;
        _direction = dir;
        _damage = enemy.damageAmount;
    }
    
    public void SetRotation(Vector2 lookDir)
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(lookDir.y, lookDir.x) + offset);
    }

    private void Update()
    {
        if (_rb)
            _rb.velocity = _direction.normalized * _speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Wall"))
            Destroy(gameObject);
        if (col.CompareTag("Player"))
        {
            if (TryGetComponent<Player>(out var player))
            {
                if (!player.GetComponent<PlayerMovement>().IsDashing())
                {
                    player.TakeDamage(_damage);
                    Destroy(gameObject);
                }
            }
            
        }
    }
}
