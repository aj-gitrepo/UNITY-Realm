using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    [SerializeField] bool isPlaceable;
    void OnMouseDown() 
    {
        if(isPlaceable) 
        {
            // Debug.Log(transform.position.name)
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceable = false;
        }
    }
}

// tick isPlaceable in the Tile prefab and uncheck it in Tile_RoadStraight and in Tile_RoadCorner
// Quaternion.identity - for rotation
