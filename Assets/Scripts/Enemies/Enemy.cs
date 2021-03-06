using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    protected float moveSpeed = 3f;
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
        SetSpeed(moveSpeedInit);
        activeMarks = new List<Mark>();
    }

    protected virtual void Update()
    {
        SetAnimation();
        if(isDying)
            return;
        
        if (embeddedWeapon)
            embeddedWeapon.position = embeddedWeaponOffset + transform.position;
        _markedSR.enabled = embeddedWeapon;

        if (isStunned)
            return;
        
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
        Debug.Log($"{name} Charging Up!");
        chargingUp = true;
        Vector3 pos = transform.position;
        canAttackTime = Time.time + time;

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
        chargingUp = false;
        StartCoroutine(nameof(Attack));
    }

    public void SetTarget(Transform target)
    {
        _setter.target = target;
    }


    protected abstract IEnumerator Attack();

    protected void EndAttack()
    {
        
        Debug.Log($"{name} Attacked!");
        canAttackTime = Time.time + attackInterval;
        isAttacking = false;
        chargingUp = false;
        _aiPath.canMove = true;
        _aiPath.maxSpeed = moveSpeed;
    }

    public void SetSpeed(float speed)
    {
        _aiPath.canMove = speed > 0;
        moveSpeed = speed;
        _aiPath.maxSpeed = speed;

    }

    public void Stun()
    {
        isStunned = true;
        SetSpeed(0);
        StopCoroutine("Attack");
        if (chargingUp)
        {
            StopCoroutine("ChargeUpThenAttack");
            canAttackTime = Time.time + chargeUpTime;
        }
        else if (isAttacking)
        {
            StopCoroutine("Attack");
            EndAttack();
        }
    }
    public void UnStun()
    {
        isStunned = false;
        SetSpeed(moveSpeedInit);
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
            if(mark.upgradeName.ToLower() == "slow mark".ToLower())
                ReceiveMark(mark);
        foreach (var mark in marks)
            if(mark.upgradeName.ToLower() != "slow mark".ToLower())
                ReceiveMark(mark);
    }

    private void Die()
    {
        // play death anim
        EnemyDied.Invoke(this);
        SetSpeed(0);
        canAttackTime = Mathf.Infinity;
        embeddedWeapon = null;
        _bc.enabled = false;
        _markedSR.enabled = false;
        isDying = true;
        StopAllCoroutines();
        StartCoroutine(PlayDeathAnim(1f));
    }

    private IEnumerator PlayDeathAnim(float duration)
    {
        // Debug.Log("Dying");
        Vector3 pos = transform.position;
        isDying = true;
        float endTime = Time.time + duration;
        _anim.Play(enemyName + "Death");
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
        Debug.Log($"{name} received {mark.upgradeName}");
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
                Die();
            }
        }

    }

    protected abstract void SetAnimation();

    public bool IsStuned() => isStunned;


}
