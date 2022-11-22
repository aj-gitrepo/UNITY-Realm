using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //to enable it to be serializedField
public class Node
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo; //the parent node from which this node branched

    // Constructor
    public Node (Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
