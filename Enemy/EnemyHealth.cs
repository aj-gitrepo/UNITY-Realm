using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    int currentHitPoint = 0;
    void Start()
    {
        currentHitPoint = maxHitPoints;
    }

    void OnParticleCollision(GameObject other) 
    {
        ProcessHit();
    }

    void ProcessHit() 
    {
        currentHitPoint--;
        if(currentHitPoint < 1)
        {
            Destroy(gameObject);
        }
    }

}
