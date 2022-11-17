using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    Transform weapon;
    Transform target;


    // Responsible for aquiring our target, firing and facing the target
    void Start() 
    {
        target = FindObjectOfType<EnemyMover>().transform;
        weapon = transform.Find("BallistaTopMesh"); //weapon = gameObject.transform.GetChild(1);
    }

    void Update() 
    {
        AimWeapon();
    }

    void AimWeapon()
    {
        weapon.LookAt(target);
    }
}

// Add this script to Ballista prefab

// Remember, we don't need to set our target in unity inspector because that's going to be handled in our code.
