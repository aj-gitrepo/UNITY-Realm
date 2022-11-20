using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem projectileParticles;
    Transform weapon;
    Transform target;


    // Responsible for aquiring our target, firing and facing the target
    void Start() 
    {
        weapon = transform.Find("BallistaTopMesh"); //weapon = gameObject.transform.GetChild(1);
    }

    void Update() 
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }

            target = closestTarget;
        }
    }

    void AimWeapon()
    {
        if(target == null) //to avoid null error in target
        {
            return;
        }

        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);

        if(targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}

// Add this script to Ballista prefab

// Remember, we don't need to set our target in unity inspector because that's going to be handled in our code.
