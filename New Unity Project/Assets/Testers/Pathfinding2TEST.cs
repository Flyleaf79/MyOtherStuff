using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Diagnostics; // PERFORMANCE CHECKER.
/*
public class Pathfinding2 : MonoBehaviour
{

    #region Psudo Code
    //*
    Open // the set of nodes to be evaluted
    Closed // the set of nodes already evaluted
    add the start node to Open

        loop
        current = node in open with the lowest f_cost
        remove current from Open
        add current to closed

        if current is the tartget node // path has been found
        return

        foreach neighbour of the current node
        if neighbour is not traversable or neighbour is in Closed
        skip to the next neighbour

        if new path to neighbour is shorter or neighbour is not in Open 
        set f_cost of neighbour
        set parent of neighbour to current
        if neighbour is not in Open
        add neighbour to Open

    #endregion


    public Transform seeker, target;
    Grid grid;

    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            FindPath(seeker.position, target.position);
        }
    }
    
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();


        // First thing we need to do is convert node position to world position. We already have that in grid
        Node startNode = grid.NodeFromWorldPoint(startPos); // Its first initial position
        Node targetNode = grid.NodeFromWorldPoint(targetPos); // target Position

        //OLD SYSTEM// List<Node> openSet = new List<Node>(); // set of nodes to be evaluated

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>(); // set of nodes already evaluated

        openSet.Add(startNode); // its own point is a open node.

        while (openSet.Count > 0)
        {
            //old system// Node currentNode = openSet[0]; // the first current node is set to its own start position so that we dont evaluate it.
            Node currentNode = openSet.RemoveFirst(); // new system
            #region old System
            /*
            for (int i = 1; i < openSet.Count; i++) // = 1 so that we check another node inside the open set.
            {
                // Remember if both fcost are the same than our next open in A* is to check for hCost which is distance from the object.

                // currentNode : fcost = 48, gCost = 10, hCost = 38 
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode); 
            
            #endregion

            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                sw.Stop();
                print("Path found: " + sw.ElapsedMilliseconds + " ms");
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                    continue;

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                        
                    }
                    openSet.UpdateItem(neighbour);
                }
            }

        }
    }


    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        grid.path = path;
    }


    int GetDistance(Node nodeA, Node nodeB)
    {
        // First we will need to grab the lowest number of either x or y. (number of points they have) and along either the x or y it will give us
        // How many diagonal lines we need to get to our target. Now we just need to subtract the higher number by the lower number in order to reach the target.
        // in the following equation we used numbers like 14 and 10. 
        // 14y + 10(x-y) = if x > y
        // 14x + 10(y-x) = if y > x

        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridY);
        int distY = Mathf.Abs(nodeB.gridY - nodeA.gridX);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        return 14 * distX * 10 * (distY - distX);
    }

}
*/
