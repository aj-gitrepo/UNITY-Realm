using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable; //this is private and cannot be read from outside so using the below 
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;

    PathFinding pathFinder;

    Vector2Int coordinates = new Vector2Int();

    void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinding>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown() 
    {
        if(gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates)) 
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position); //may run out of money and return false
            isPlaceable = !isPlaced;
            gridManager.BlockNode(coordinates);
        }
    }
}

// tick isPlaceable in the Tile prefab and uncheck it in Tile_RoadStraight and in Tile_RoadCorner
// Quaternion.identity - for rotation

