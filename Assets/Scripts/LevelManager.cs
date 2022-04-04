using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static StartRoom[] startRooms;
    public static LevelManager Instance { get; private set; }
    public static Player player;
    public static Weapon weapon;

    private static StartRoom _checkpoint;
    private List<Upgrade> _upgradesAppliedCurrently;
    private static Upgrade[] _upgradesApplied;

    private void Awake()
    {
        // if (Instance)
        //     Destroy(gameObject);
        // else
        //     Instance = this;
        
        _upgradesAppliedCurrently = new List<Upgrade>();
        

        if(startRooms == null)
            startRooms = FindObjectsOfType<StartRoom>();

        if (!_checkpoint)
        {
            foreach(var room in startRooms)
                if (room.absoluteStart)
                    _checkpoint = room;
            _upgradesApplied = new Upgrade[0];
        }

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

    private void LoadCheckpoint()
    {
        Debug.Log("Load called");
        foreach (var upgrade in _upgradesApplied)
        {
            Upgrades.ApplyUpgrade(upgrade);
        }
        Debug.Log(player);
        _checkpoint?.Spawn(player.transform);
    }

    private void Start()
    {
        Debug.Log("Start called");
        player = FindObjectOfType<Player>();
        weapon = FindObjectOfType<Weapon>();
        LoadCheckpoint();
        
    }

    private void OnUpgradeApplied(Upgrade upgrade)
    {
        _upgradesAppliedCurrently.Add(upgrade);
    }

    public static void SaveCheckpoint(StartRoom startRoom)
    {
        
        //Debug.Log($"Saved progress to {startRoom} from {startRoom.transform.parent.name}");
        _checkpoint = GetStartRoom(startRoom);
        _upgradesApplied = Instance._upgradesAppliedCurrently.ToArray();
    }

    private static StartRoom GetStartRoom(StartRoom startRoom)
    {
        StartRoom _room = null;
        foreach (var room in startRooms)
            if (room.songName == startRoom.songName)
                _room = room;

        return _room;
    }
}
