using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicursalPathGeneration : MonoBehaviour
{
    //This script generates a unicursal path leading from the starting point to the ending point.
    // Start is called before the first frame update
    public float xLimit; //left boundary of the area to create unicursal path
    public float zLimit; //upper boundary of the area to create unicursal path
    public int xNumOfNodes; //number of nodes along the x-axis
    public int zNumOfNodes; //number of nodes along the z-axis
    public float sideLengthOfTile; //side length of each tile.
    //The width of the area to generate unicursal path will be 2*sideLengthOfTile*xNumOfNodes+1, and similarly for the height.
    public GameObject tile;

    void Start()
    {
        Graph aGraph = new Graph(6, 10);
        Stack<Graph.Node> aPath = aGraph.makeUnicursal(3, 0, 3, 9);
        foreach (Graph.Node i in aPath)
        {
           Debug.Log(i.xInd + " " + i.zInd);
        }

    }

}
