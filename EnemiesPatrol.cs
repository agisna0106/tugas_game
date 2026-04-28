using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPatrol : MonoBehaviour
{
    public Transform enemy;
    public float speed = 1;

    void Start()
    {
        Patrol();
    }

    void Patrol()
    {
        if (enemy.position.x >= 2f)
        {
            speed = speed * -1;
        }
        else if (enemy.position.x <= -2f)
        {
            speed = speed * -1;
        }
        enemy.Translate(speed * Time.deltaTime, 0, 0);
    }
     void Update()
    {
        Patrol();
    }
}
