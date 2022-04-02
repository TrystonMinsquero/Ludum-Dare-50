using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<Mark> marks;
    
    public void SetRotation(Vector2 lookDir)
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(lookDir.y, lookDir.x) - 45);
    }
}
