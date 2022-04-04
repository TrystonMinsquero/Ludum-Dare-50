using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public StartRoom nextLevelsStartRoom;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        { 
            Debug.Log("Next Level");
            if(nextLevelsStartRoom)
                nextLevelsStartRoom.Spawn(col.transform);
            else
                Debug.LogWarning($"Start Room not assined for {name}");
        }
    }
}
