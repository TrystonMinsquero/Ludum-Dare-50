using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private Room[] rooms;
    private Room activeRoom;

    private void Awake()
    {
        rooms = GetComponentsInChildren<Room>();
    }

    private void Update()
    {
        
    }

    private void TurnOffAllLights()
    {
        
    }
}
