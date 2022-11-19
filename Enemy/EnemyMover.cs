using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    void Start()
    {
        FindPath(); //findiong path before starting coroutine
        ReturnToStart();
        StartCoroutine(FollowPath());// coroutines are called differntly
    }

    void FindPath()
    {
        path.Clear();
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        // Not an ideal method as the path order may not be correct
        foreach(GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
    }

    void ReturnToStart()
    {
        // to start the position of the RAM from the starting path coordinate instead of (0, 0)
        transform.position = path[0].transform.position;
    }

    IEnumerator FollowPath() //coroutine
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f; //travelPercent in Lerp is from 0 to 1

            transform.LookAt(endPosition); // to make the ram rotate according to direction

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed; //updates every frame
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        Destroy(gameObject); //destroying the gameObject at the end of the path
    }
}


// Using InvokeRepeat method didn't work well
// IEnumerator -  think of it as returning something countable that the system can use.

// returning something from a routine is a little bit different to returning something from a method.
// use yield return
// yield really just means give up control. And in the current context, this is saying give up control and then come back to me in 1 sec.

// It's going to end things here for now. It's going to go back into start and carry on finishing off whatever it needs to do. And then after 1/2 of time, we're going to pop back in on our routine and say, Hey, you told me to come back in 1 sec. I'm back. What do you want me to do? Well, the current train is then going to continue executing as it normally would, as if it were a regular method. So we're going to hit the end of our for each loop, we're going to loop back to the top and we're going to print the next waypoint in our list. We're then going to hit the yield statement again and our routine is going to tell the system, I've done what I need to do. Come back to me in one more second.

// coroutines are called differntly
