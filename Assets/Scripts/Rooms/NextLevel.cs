using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public StartRoom nextLevelsStartRoom;
    public bool finishGame;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (finishGame)
                SceneManager.LoadScene("WinMenu");
            Debug.Log("Next Level");
            if(nextLevelsStartRoom)
                nextLevelsStartRoom.Spawn(col.transform);
            else
                Debug.LogWarning($"Start Room not assined for {name}");
        }
    }
}
