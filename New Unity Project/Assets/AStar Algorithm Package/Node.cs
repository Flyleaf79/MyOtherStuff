using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node>
{
    public bool walkable; // We know that there is two layers that mean if the Node is Walkable and Unwalkable
    public Vector3 worldPosition; // We need to know the nodes world poisition
    public int gridX;
    public int gridY;

    public int gCost; // g Distance from the starting node
    public int hCost; // h Distance from the end Node

    public Node parent;


    int heapIndex; // Node keeps track of its heapindex value;

    //Some way of assignning these nodes
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) // Constructor
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    // contract with IHeapItem interface
    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost); // compare myNodeFcost to another nodes fcost;
        if (compare == 0) // the two fcost are equal
        {
            compare = hCost.CompareTo(nodeToCompare.hCost); // h cost will be tie breakers
            // remember that we want to return 1 if the node has a higher priority than the item were compareing it to
            // in terms of integers, int compare to int  will return 1 if the integer is higher. with our nodes its actually revered
            // we want to return 1 if its lower 

        }
        return -compare;
    }
}
