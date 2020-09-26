using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CreateCanyonMaze : MonoBehaviour
{
    public GameObject completeTile;
    public int xMazeSize;
    public int zMazeSize;
    public int yOffset;
    public float mazeLeftLimit;
    public float mazeSouthLimit;
    public float completeTileSideLength = 6f;
    public float gapBetweenTile = 2f;
    public GameObject mazeController;
    // Start is called before the first frame update


    void Start()
    {
        Graph mazeGraph = new Graph(5, 5); //create a graph that represents the maze we're making
        Graph.Node[,] maze = mazeGraph.makeUST();

        /*//for debug below
        Graph.Node[,] maze = new Graph.Node[5,5];
        for(int i=0;i<5; i++)
        {
            for (int j=0;j<5;j++)
            {
                maze[i, j] = new Graph.Node(i, j);
                if(i!=4) maze[i, j].rightEdge = true;
                if(j!=4) maze[i, j].upEdge = true;
            }
        }
        
        //for debug above*/
        GameObject[,] tiles = new GameObject[maze.GetLength(0), maze.GetLength(1)]; //make an array of maze tiles, their indeices correponding to the maze matrix.
        for(int i=0;i<maze.GetLength(0);i++) //create the maze, initializing only the heart
        {
            float tilePosX = mazeLeftLimit + i * (completeTileSideLength+gapBetweenTile) + (0.5f) * completeTileSideLength;
            for (int j=0;j<maze.GetLength(1);j++)
            {
                float tilePosZ = mazeSouthLimit + j * (completeTileSideLength+gapBetweenTile) + (0.5f) * completeTileSideLength;
                Vector3 location = new Vector3(tilePosX, yOffset, tilePosZ);
                tiles[i, j] = Instantiate(completeTile, location, completeTile.transform.rotation);
            }
        }

        for(int i=0; i<tiles.GetLength(0); i++)
        {
            for (int j=0;j<tiles.GetLength(1);j++)
            {
                GameObject thisTile = tiles[i, j]; //thisTile and thisNode are corresponding
                Graph.Node thisNode = maze[i, j];
                if(thisNode.rightEdge) //if this node has a right edge
                {
                    thisTile.transform.GetChild((int)TileChildren.EAST).gameObject.SetActive(true); //activate the right extension of this tile
                    tiles[i + 1, j].transform.GetChild((int)TileChildren.WEST).gameObject.SetActive(true); //activate the left extension of the game object on the right side
                }
                if (thisNode.upEdge) //if this node has an up (north) edge
                {
                    thisTile.transform.GetChild((int)TileChildren.NORTH).gameObject.SetActive(true); //activate the up extension of this tile
                    tiles[i, j + 1].transform.GetChild((int)TileChildren.SOUTH).gameObject.SetActive(true); //activate the south extension of the tile to the north
                    //this would not cause out of bound since there wouldn't be an edge in that direction to start with
                }
            }
        }
        mazeController.GetComponent<MazeControl>().initialize(tiles); //initialize the maze controller who will keep track of whether the maze is still solvable

    }

}
