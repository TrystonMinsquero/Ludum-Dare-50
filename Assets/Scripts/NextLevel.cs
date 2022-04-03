using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public Transform nextLevelCoordinates;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            
            Debug.Log("Next Level");
            col.transform.position = nextLevelCoordinates.position;
        }
    }
}
