using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public void TakeDamage(int damage)
    {
        Debug.Log($"Takes {damage} Damage");
    }
}
