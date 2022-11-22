using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    // Negative pool size may break the code
    [SerializeField] [Range(0, 50)] int poolSize = 5;

    //if this gets negative or becomes 0 it may create prob in SpawnEnemy and so using range
    [SerializeField] [Range(0.1f, 30f)] float spawnTime = 1f; 

    GameObject[] pool;

    void Awake() {
        // executes before start
        PopulatePool();
    }
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for(int i = 0; i < poolSize; i++)
        {
            if(pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTime);
        }
    }
}

// to see overloads Shift + Ctrl + Space
// here for Instantiate in PopulatePool using the Overload
// Object Object.Instantiate(Object original, Transform parent)

