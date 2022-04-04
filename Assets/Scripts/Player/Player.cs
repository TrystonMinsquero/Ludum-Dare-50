
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float TimeOfDeath { get; private set; }

    public float damageModifer = 1f;
    public float timeOnKill = 0f;

    public Color hitColor = Color.red;
    public float invulnerabilityTimeAfterHit = .5f;

    private bool _isInvulnerable;

    private void Awake()
    {
        TimeOfDeath = 100; // for waiting, get overwritten
    }

    private IEnumerator BecomeInvulnerable(float time)
    {
        var sr = GetComponent < SpriteRenderer>();
        _isInvulnerable = true;
        Color prevColor = sr.color;
        sr.color = hitColor;
        yield return new WaitForSeconds(time);
        sr.color = prevColor;
        _isInvulnerable = false;
    }
    
    public void TakeDamage(float damage)
    {
        if (!_isInvulnerable)
        {
            Debug.Log($"Took {damage * damageModifer} damage");
            TimeOfDeath -= damage * damageModifer;
            StartCoroutine(BecomeInvulnerable(invulnerabilityTimeAfterHit));
        }
    }

    public void AddTime(float time)
    {
        TimeOfDeath += time;
    }

    public void SetTime(float time)
    {
        TimeOfDeath = Time.time + time;
    }

    public void Die()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void Update()
    {
        if (Time.time >= TimeOfDeath)
        {
            Die();
        }
            
    }
}
