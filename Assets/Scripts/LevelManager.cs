using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public static Player player;
    public static Weapon weapon;

    private static StartRoom _checkpoint;
    public static List<Upgrade> upgradesApplied;
    private static Upgrade[] _upgradesApplied;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
        upgradesApplied = new List<Upgrade>();

    }

    private void OnEnable()
    {
        Upgrades.UpgradeApplied += OnUpgradeApplied;
        StartRoom.StartRoomEntered += SaveCheckpoint;
    }

    private void OnDisable()
    {
        Upgrades.UpgradeApplied -= OnUpgradeApplied;
        StartRoom.StartRoomEntered -= SaveCheckpoint;
    }

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        weapon = GameObject.FindObjectOfType<Weapon>();

        if (_checkpoint)
        {
            _checkpoint.Spawn(player.transform);
            foreach (var upgrade in _upgradesApplied)
            {
                Upgrades.ApplyUpgrade(upgrade);
            }
        }
        else
            _checkpoint = StartRoom.AbsoluteStartRoom;
    }

    private void OnUpgradeApplied(Upgrade upgrade)
    {
        upgradesApplied.Add(upgrade);
    }

    private void SaveCheckpoint(StartRoom startRoom)
    {
        _checkpoint = startRoom;
        _upgradesApplied = upgradesApplied.ToArray();
    }
}
