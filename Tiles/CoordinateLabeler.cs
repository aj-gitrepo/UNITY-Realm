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
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false; //to turn off visibility in game mode and enable with C only when needed
        waypoint = GetComponentInParent<Waypoint>(); //as it is a sibling to this
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
        if(waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
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

// Add this script to he text 

// UnityEditor.EditorSnapSettings.move.x - as coordinates are in interms of 10 to convert them in terms of 1
// as the grid scale is set in terms of 10
