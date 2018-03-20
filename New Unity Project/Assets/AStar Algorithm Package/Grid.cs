using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{

    public bool displayGridGrizmos;

    public LayerMask unWalkableMask; // Layer in Unity
    public Vector2 gridWorldSize; // Area in world Size that the grid will cover. // Set Locally
    public float nodeRadius; // Defines the radius of each indivdual node
    Node[,] grid; //Create a 2 Dimensional Array of Nodes which will represent our grid

    float nodeDiameter; // Total size of a Node
    int gridSizeX, gridSizeY; // Grid for node


    void Awake()
    {
        // Based on our nodeRadius how many nodes can we fit into our grid.
        nodeDiameter = nodeRadius * 2;
        //We cant have half a node or w.e so we will round numbers. with mathf.roundtoint round to nearest int
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    void CreateGrid()
    {
        //Amount of nodes in x and amount of nodes in y 
        grid = new Node[gridSizeX, gridSizeY];
        // GridWorldSize is Vector2 So the indirection makes sense.
        // This is like saying  u = 0 - (1,0,0 * 60 / 2) - (0,1,0 * 60/2)
        // u = 0 - (30,0,0) - (0,30,0)
        // u = ((-30,0,0) - (0,-30,0))
        // u = -30,-30,0

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;


        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // [] + ] + []  Adding radius to than adding the full node. than direction
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                // This is going to be true if we dont hit anything in the world map
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unWalkableMask)); // IMPORTANT TO USE AN MASK
                // grid[3,3] = new Node (walkable, worldPoint);
                // grid[0,0]
                // grid[1,0]
                // grid[2,0]
                // grid[0,1]
                // grid[0,2]
                // grid[1,1]
                // grid[2,1]
                // grid[2,2]
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;


                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if ((checkX >= 0 && checkX < gridSizeX) && (checkY >= 0 && checkY < gridSizeY))
                    neighbours.Add(grid[checkX, checkY]);
            }
        }
        return neighbours;
    }



    // Converts World point to GridPoint
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        // World position is where the object is in world space 

        // Convert world poistion to percentage for both x and y position. Of how far along the grid it is
        // for the x far left 0 if its in the middle .5 if its in the far right itll have 1
        // if the position fo the world position is 30 so its in the far left - 15. -15 + (-15) = 0 / 30 = 0
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        // Now we need to clamp so that we only get a value of 1 or 0 and inbetween
        // This will help for if the object is out of the map for whatever reason than dont mess up any calculation or mess up our index
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        //Debug.Log("X = " + percentX + " Y = " + percentY);
        // now we want to get the x and y intercepts of the 2d grid array
        // Remember that when you make an array grid[5] itll be grid[0.....4] not to 5 so gridsize5 - 1 to keep in array than * percentage
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX); // remember arrays are 0 based. Say percentX is 1 multiply by max index - 1 so were not outside.
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    void OnDrawGizmos()
    {
        //gizmos work very percedural
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null && displayGridGrizmos)
        {
            foreach (Node n in grid)
            {
                // ?: yup... i finally used it. If walkable then white else if red. (Ternary Operator)
                Gizmos.color = (n.walkable) ? Color.white : Color.blue;
                //if (path != null)
                //    if (path.Contains(n))
                      //  Gizmos.color = Color.black;
                // -.1 is just for spacing purpuses
                // Draw all nodes inside grid using its world space. Vector.one i just for sizing
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
