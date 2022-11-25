using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    [SerializeField] [Range(0f, 5f)] float rotationSpeed = 5f;
    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    PathFinding pathFinder;

    void Awake() 
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinding>();
    }

    void OnEnable() //runs after Awake before Start
    {
        ReturnToStart(); //return to start then calculate the path (when the enemy spawns the 2nd, 3rd time etc)
        RecalculatePath(true); //finding path before starting coroutine, true - recalculating from startposition
        
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coornidates = new Vector2Int();

        if(resetPath)
        {
            coornidates = pathFinder.StartCoordinates;
        }
        else
        {
            coornidates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines(); //to stop the current path movement of enemy
        path.Clear();
        path = pathFinder.GetNewPath(coornidates);
        StartCoroutine(FollowPath()); // to make sure that coroutine starts after calculating path
    }

    void ReturnToStart()
    {
        // to start the position of the RAM from the starting path coordinate instead of (0, 0)
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false); //instead of destroying the gameObject at the end of the path
    }

    IEnumerator FollowPath() //coroutine
    {
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position; //now this will be i=0
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates); //from next node
            float travelPercent = 0f; //travelPercent in Lerp is from 0 to 1

            // transform.LookAt(endPosition); // to make the ram rotate according to direction

            Quaternion startRotation = transform.rotation; 
            Quaternion endRotation = Quaternion.LookRotation(endPosition - startPosition);

            while(travelPercent < 1f)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, 
                    Mathf.Clamp(travelPercent * rotationSpeed, 0, 1));
                travelPercent += Time.deltaTime * speed; //updates every frame
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}


// Using InvokeRepeat method didn't work well
// IEnumerator -  think of it as returning something countable that the system can use.

// returning something from a routine is a little bit different to returning something from a method.
// use yield return
// yield really just means give up control. And in the current context, this is saying give up control and then come back to me in 1 sec.

// It's going to end things here for now. It's going to go back into start and carry on finishing off whatever it needs to do. And then after 1/2 of time, we're going to pop back in on our routine and say, Hey, you told me to come back in 1 sec. I'm back. What do you want me to do? Well, the current train is then going to continue executing as it normally would, as if it were a regular method. So we're going to hit the end of our for each loop, we're going to loop back to the top and we're going to print the next waypoint in our list. We're then going to hit the yield statement again and our routine is going to tell the system, I've done what I need to do. Come back to me in one more second.

// coroutines are called differntly
