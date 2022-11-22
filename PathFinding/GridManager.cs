using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Node node;
    void Start()
    {
        Debug.Log(node.coordinates);
        Debug.Log(node.isWalkable);

    }

    void Update()
    {
        
    }
}

//  what the grid manager class is going to do is store a reference to all the nodes in our world and organize them into a grid for us.
