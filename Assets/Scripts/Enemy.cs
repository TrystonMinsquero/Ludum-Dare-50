using UnityEngine;
using Pathfinding;

public abstract class Enemy : MonoBehaviour
{
    public bool isMarked;
    public float damageAmount;
    public float attackInterval;
    
    

    public abstract void Attack();
    
    
    
    

}
