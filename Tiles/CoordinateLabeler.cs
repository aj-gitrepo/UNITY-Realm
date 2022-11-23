#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] //executes in both editmode and in game mode
[RequireComponent(typeof(TextMeshPro))] //tomake sure TextMeshPro is always attached to this
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f); //orange
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    GridManager gridManager;

    void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>(); //as it is included in scene
        label = GetComponent<TextMeshPro>();
        label.enabled = false; //to turn off visibility in game mode and enable with C only when needed
        DisplayCoordinates(); //updates only on Awake i.e only once when the game is started
    }

    void Update()
    {
        if(!Application.isPlaying) //if not in game mode
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);
        
        if(node == null) { return; }

        if(!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if(node.isPath)
        {
            label.color = pathColor;
        }
        else if(node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z); // z- because 2d in 3d world
        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
#endif

// Add this script to he text 

// UnityEditor.EditorSnapSettings.move.x - as coordinates are in interms of 10 to convert them in terms of 1
// as the grid scale is set in terms of 10

// #if (UNITY_EDITOR) and #endif - to avoid error while build
