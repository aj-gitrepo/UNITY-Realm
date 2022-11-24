using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] Vector2Int StartCoordinates;
    [SerializeField] Vector2Int DestinationCoordinates;

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>(); //explored
    Queue<Node> frontier = new Queue<Node>();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        if(gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }
    void Start()
    {
        startNode = grid[StartCoordinates];
        destinationNode = grid[DestinationCoordinates];
        GetPath();
    }

    public List<Node> GetPath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearh(); //explores all the nodes between start and end
        return BuildPath(); //calculates path from destination node
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            if(grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        foreach(Node neighbor in neighbors)
        {
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearh()
    {
        // to clear for next path finding
        frontier.Clear();
        reached.Clear();

        bool isRunning = true; //to help in breaking the while loop

        frontier.Enqueue(startNode);
        reached.Add(startNode.coordinates, startNode);

        while(frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if(currentSearchNode.coordinates == DestinationCoordinates)
            {
                isRunning = false;
            }
        }

    }

    List<Node> BuildPath() //returns list of Nodes
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetPath();
            grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetPath();
                return true;
            }
        }
        return false; //if it does not block path or if GridManager is not found 
    }

}

// Well, for now, we really just want to specify any particular node that exists in our grid and then have it search around for its four neighbors.

// So these are all four directions and you can really put them in any order that you like. I've just picked this particular order because it eventually works quite well with the map that I'm creating.
