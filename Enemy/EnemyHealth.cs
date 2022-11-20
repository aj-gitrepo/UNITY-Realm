using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    int currentHitPoint = 0;

    Enemy enemy;
    void OnEnable()
    {
        currentHitPoint = maxHitPoints;
    }

    void Start() 
    {
        enemy = GetComponent<Enemy>();
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
            gameObject.SetActive(false); //instead of destroying the gameObject when health is 0
            enemy.RewardGold();
        }
    }

}
