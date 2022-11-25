using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    [Tooltip("Unity Grid Size - Should match UnityEditot snap Settings.")]
    [SerializeField] int unityGridSize = 10; //current snap settings in unity
    public int UnityGridSize { get { return unityGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>(); //<key, value>
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } } //getter method
    void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates) //Node - return type here
    {
        if(grid.ContainsKey(coordinates)) //to avoid error if the coordinate does not exist
        {
            return grid[coordinates];
        }

        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    //  convert the world positions to grid coordinates and back again.
    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();

        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();

        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;

        return position;
    }

    


    void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}

//  what the grid manager class is going to do is store a reference to all the nodes in our world and organize them into a grid for us.

//  using a vector to int here (in gridSize) will allow us to specify both the x width and the Y height of our 2D grid. But if you're only ever working on square grids, then you could just use this as an integer which would handle both the X and the Y.

// Now, one thing to note about the grid manager that we're going to create is it's not going to be able to handle nodes at negative numbers. So it's going to start at the coordinates of zero zero and then work positively in the X and Y.

// the way we want our create grid method to work is to start off at all zero zero position and then loop through every single element in our grid and add a new node object for that position.

// to test grid manager enter the grid size in the serialized field and play the game