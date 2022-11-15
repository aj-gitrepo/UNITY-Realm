using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] //executes in both editmode and in game mode
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates(); //updates only on Awake i.e only once when the game is started
    }

    void Update()
    {
        if(!Application.isPlaying) //if not in game mode
        {
            DisplayCoordinates();
            UpdateObjectName();
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
