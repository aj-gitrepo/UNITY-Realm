using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] float waitTime = 1f;

    void Start()
    {
        // coroutines are called differntly
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath() //coroutine
    {
        foreach(Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(waitTime);
        }
    }
}


// Using InvokeRepeat method didn't work well
// IEnumerator -  think of it as returning something countable that the system can use.

// returning something from a routine is a little bit different to returning something from a method.
// use yield return
// yield really just means give up control. And in the current context, this is saying give up control and then come back to me in 1 sec.

// It's going to end things here for now. It's going to go back into start and carry on finishing off whatever it needs to do. And then after 1/2 of time, we're going to pop back in on our routine and say, Hey, you told me to come back in 1 sec. I'm back. What do you want me to do? Well, the current train is then going to continue executing as it normally would, as if it were a regular method. So we're going to hit the end of our for each loop, we're going to loop back to the top and we're going to print the next waypoint in our list. We're then going to hit the yield statement again and our routine is going to tell the system, I've done what I need to do. Come back to me in one more second.

// coroutines are called differntly
