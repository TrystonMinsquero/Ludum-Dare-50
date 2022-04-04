
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int startTime = 120;
    public float TimeOfDeath { get; private set; } = 100;

    public float damageModifer = 1f;
    public float timeOnKill = 0f;


    public void TakeDamage(float damage)
    {
        Debug.Log($"Took {damage * damageModifer} damage");
        TimeOfDeath -= damage * damageModifer;
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
        GameOverUI.ShowScreen();
    }

    private void Update()
    {
        if (Time.time >= TimeOfDeath)
        {
            Die();
        }
            
    }
}
