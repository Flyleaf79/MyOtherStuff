using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; // used something called action

public class PathRequestManager : MonoBehaviour
{
    #region What is Queue
    /*
    Queue is a class.
    "FIFO" Fist in First out collection similiar to its english definition. An example would be a lineup for checkout at a walmart.
    The cashier isnt in its telle, but the line is growing. The person thats in the front of the line is waiting the longest for its service, so when the cashier
    comes back the first person in the line will be serviced. In short term the elements that the "queue" received the longest ago will be handled first.
    */
    #endregion
    
    // Queues each pathRequest(structures) in order of the First comes first serves
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    // Holds the current path request being looked at.
    PathRequest currentPathRequest;
    // static variable of itself so that it changes for each pathrequest to itself and not have to declare a pathRequestManager every time.
    // This is also to be able to aquire information on the RequestPath Method that is static.
    static PathRequestManager instance;
    // Refrenece to the Pathfiniding script
    Pathfinding pathFinding;
    // checks if the Mananger is already processing the path. This bool coworks with Pathfinidng script.
    bool isProcessingPath;


    void Awake()
    {
        // class is an instance of itself
        instance = this;
        pathFinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        //Request a NEWpathrequest with declared values
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        // instance(this class) add newPathRequest to the Queue(pathRequestQueue);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    // This will see if we already processing the request and if not than it will ask the pathfindinng script to process the next one.
    void TryProcessNext()
    {
        // if w4 are not processing a path and the Queue is not empty than..
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue(); // Also gets the first item in the queue
            // we are not processing a path;
            isProcessingPath = true;
            //
            pathFinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        // After processing a Path, PathRequest callback which is an Action set it to the path that was declared when calling this function
        // than pass over the bool that was declared on this function.
        currentPathRequest.callback(path, success);
        // we are now no longer lookiong at a path and so we can continue to look at more paths.
        isProcessingPath = false;

        TryProcessNext();
    }

    #region What Is a struct?
    /*
    A struct is short term for Structure, struct is a group of variables and each variable inside of a struct is called a "member"
    */
    #endregion

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }
}
