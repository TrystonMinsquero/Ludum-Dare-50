using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public static Player player;
    public static Weapon weapon;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        weapon = GameObject.FindObjectOfType<Weapon>();
    }
}
