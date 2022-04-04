using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Pathfinding;

public abstract class Enemy : MonoBehaviour
{
    [Header("Generic")] 
    public string enemyName;
    public bool isMarked;
    public int damageAmount = 5;
    public float attackInterval = .1f;
    public float chargeUpTime;
    public bool canMoveWhileCharging;
    public float moveSpeedInit = 3f;
    public List<Mark> activeMarks;
    public float pushForce;

    public float moveSpeed = 3f;
    [HideInInspector]
    public bool isDying;
    protected AIDestinationSetter _setter;
    protected AIPath _aiPath;
    protected BoxCollider2D _bc;
    protected Animator _anim;
    protected SpriteRenderer _sr;
    [SerializeField]
    protected SpriteRenderer _markedSR;
    protected bool chargingUp;
    protected bool isAttacking;
    protected bool isStunned;
    
    public event Action<Enemy> EnemyDied = delegate(Enemy enemy) {  };

    protected float canAttackTime;

    private Transform embeddedWeapon;
    private Vector3 embeddedWeaponOffset;

    private void Awake()
    {
        _setter = GetComponent<AIDestinationSetter>();
        _aiPath = GetComponent<AIPath>();
        _bc = GetComponent<BoxCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        SetSpeed(moveSpeed);
        activeMarks = new List<Mark>();
    }

    protected virtual void Update()
    {
        SetAnimation();
        if(isDying || isStunned)
            return;
        
        if (embeddedWeapon)
            embeddedWeapon.position = embeddedWeaponOffset + transform.position;
        _markedSR.enabled = activeMarks.Count > 0;
        
        //Soft push
        var collions = Physics2D.OverlapBoxAll(transform.position, _bc.size, 0);
        foreach (var col in collions) 
            if (col.CompareTag("Enemy") && col != _bc)
            {
                Push(col, pushForce);
            }
    }

    protected IEnumerator ChargeUpThenAttack(float time)
    {
        // Debug.Log($"{name} Charging Up!");
        chargingUp = true;
        Vector3 pos = transform.position;

        float endTime = Time.time + time;
        while (Time.time < endTime)
        {
            if (!canMoveWhileCharging)
            {
                transform.position = pos;
                _aiPath.canMove = false;
                _aiPath.maxSpeed = 0;
            }
            yield return null;
        }
        StartCoroutine(Attack());
        chargingUp = false;
    }

    public void SetTarget(Transform target)
    {
        _setter.target = target;
    }


    protected abstract IEnumerator Attack();

    protected void EndAttack()
    {
        
        // Debug.Log($"{name} Attacked!");
        canAttackTime = Time.time + attackInterval;
        chargingUp = false;
        _aiPath.canMove = true;
        _aiPath.maxSpeed = moveSpeed;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
        _aiPath.maxSpeed = speed;
        _anim.speed = speed / moveSpeed;

    }

    public void Stun()
    {
        isStunned = true;
        _aiPath.canMove = false;
        _aiPath.maxSpeed = 0;
    }
    public void UnStun()
    {
        isStunned = false;
        _aiPath.canMove = true;
        _aiPath.maxSpeed = moveSpeed;
    }

    public void HitByWeapon(Weapon weapon)
    {
        embeddedWeapon = weapon.transform;
        _markedSR.enabled = true;
        embeddedWeaponOffset = (embeddedWeapon.position - transform.position);
        ReceiveMarks(weapon.marks);
    }

    public void ReceiveMarks(List<Mark> marks)
    {
        foreach (var mark in marks)
        {
            ReceiveMark(mark);
        }
    }

    private void Die()
    {
        // play death anim
        EnemyDied.Invoke(this);
        _aiPath.canMove = false;
        _aiPath.maxSpeed = 0;
        canAttackTime = Mathf.Infinity;
        _bc.enabled = false;
        _markedSR.enabled = false;
        StopAllCoroutines();
        StartCoroutine(PlayDeathAnim(1f));
    }

    private IEnumerator PlayDeathAnim(float duration)
    {
        // Debug.Log("Dying");
        Vector3 pos = transform.position;
        isDying = true;
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            transform.position = pos;
            yield return null;
        }
        // Debug.Log("Dead");
        Destroy(gameObject);
        isDying = false;
    }

    public void ReceiveMark(Mark mark)
    {
        activeMarks.Add(mark);
        StartCoroutine(mark.ApplyMark(this));
    }

    private void Push(Collider2D entity, float force)
    {
        // Debug.Log("Push");
        Vector3 direction = (entity.transform.position - transform.position).normalized;
        entity.transform.position += direction * force * Time.deltaTime;
    }

    public void DropWeapon()
    {
        embeddedWeapon.GetComponent<Weapon>().Drop();
        embeddedWeapon = null;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Player>(out var player))
        {
            bool isDashing = player.GetComponent<PlayerMovement>().IsDashing();
            if(!isDashing && !(this as DummyEnemy))
                player.TakeDamage(1);
            if (isDashing && embeddedWeapon)
            {
                player.GetComponent<WeaponHolder>().PickUpWeapon(embeddedWeapon.GetComponent<Weapon>());
                player.AddTime(player.timeOnKill);
                embeddedWeapon = null;
                Die();
            }
        }

    }

    protected abstract void SetAnimation();


}
