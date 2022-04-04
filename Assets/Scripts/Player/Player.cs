
using UnityEngine;

public class Player : MonoBehaviour
{
    public int time = 120;
    public float TimeOfDeath { get; private set; }


    public void TakeDamage(float damage)
    {
        TimeOfDeath -= damage;
    }

    public void AddTime(float time)
    {
        TimeOfDeath += time;
    }
    

    private void Start()
    {
        TimeOfDeath = Time.time + time;
    }
}
