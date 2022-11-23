using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] Vector2Int StartCoordinates;
    [SerializeField] Vector2Int DestinationCoordinates;

    Node startNode;
    Node endNode;
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

        startNode = new Node(StartCoordinates, true);
        endNode = new Node(DestinationCoordinates, true);
    }
    void Start()
    {
        BreadthFirstSearh();
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
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearh()
    {
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

}

// Well, for now, we really just want to specify any particular node that exists in our grid and then have it search around for its four neighbors.

// So these are all four directions and you can really put them in any order that you like. I've just picked this particular order because it eventually works quite well with the map that I'm creating.
