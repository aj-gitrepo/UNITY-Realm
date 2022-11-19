using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 5;
    [SerializeField] float spawnTime = 1f;

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
        
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            Instantiate(enemyPrefab, transform);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}

// to see overloads Shift + Ctrl + Space
// here for Instantiate in SpawnEnemy using the Overload
// Object Object.Instantiate(Object original, Transform parent)

