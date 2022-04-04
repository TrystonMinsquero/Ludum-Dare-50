using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    private Room[] rooms;
    private Room activeRoom;


    private void Awake()
    {
        rooms = GetComponentsInChildren<Room>();
    }

    private void Start()
    {
        TurnOnAllCompletedRooms();
    }

    private void Update()
    {
        
    }

    public void TurnOnAllCompletedRooms()
    {
        foreach (var room in rooms)
        {
            room.SetLightsActive(room.completed);
            if (room == activeRoom)
                room.SetLightsActive(room);
        }
    }

    public void EnterNewRoom(Room newActiveRoom)
    {
        newActiveRoom.TurnOn();
        SFXManager.Play("Doors");
        activeRoom = newActiveRoom;
        if(!newActiveRoom.completed) 
            foreach (var room in rooms)
                if (room != newActiveRoom)
                {
                    room.SetLightsActive(false);
                    var startRoom = room as StartRoom;
                    if(startRoom)
                        startRoom.guide.ShutUp();
                }
    }

    public void CompleteRoom(Room room)
    {
        TurnOnAllCompletedRooms();
    }

    private void OnEnable()
    {
        foreach (var room in rooms)
        {
            room.EnteredRoom += EnterNewRoom;
            room.CompletedRoom += CompleteRoom;
        }
    }
    
    private void OnDisable()
    {
        foreach (var room in rooms)
        {
            room.EnteredRoom -= EnterNewRoom;
            room.CompletedRoom -= CompleteRoom;
        }
    }
}
