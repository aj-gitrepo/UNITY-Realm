using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable; //this is private and cannot be read from outside so using the below 
    public bool IsPlaceable { get { return isPlaceable; } }

    void OnMouseDown() 
    {
        if(isPlaceable) 
        {
            // Debug.Log(transform.position.name)
            // Instantiate(towerPrefab, transform.position, Quaternion.identity);
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position); //may run out of money and return false
            isPlaceable = !isPlaced;
        }
    }
}

// tick isPlaceable in the Tile prefab and uncheck it in Tile_RoadStraight and in Tile_RoadCorner
// Quaternion.identity - for rotation
