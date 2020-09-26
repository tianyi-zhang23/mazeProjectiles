using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeControl : MonoBehaviour
{
    public int entryX;
    public int entryZ;
    public int exitX;
    public int exitZ;
    public GameObject winLoseController;
    //this script is informed when a maze tile is destroyed. It then checks if the maze is still solvable. If not, it calls the WinLose Controller to inform that the player won.
    // Start is called before the first frame update
    GameObject[,] mazeTiles;
    Boolean[,] alreadyOnStack;

    /*
    void Start()
    {
        alreadyOnStack = new Boolean[5, 5];
    }*/

    public void initialize(GameObject[,] maze) //initialize the controller. It keeps an array of all the maze tiles.
    {
        mazeTiles = maze;
    }

    private Boolean checkSolvable()
    {
        alreadyOnStack = new Boolean[mazeTiles.GetLength(0), mazeTiles.GetLength(1)];
        Boolean returnVal = recursiveCheckSolvable(entryX, entryZ);
        return returnVal;
        
    }

    private Boolean recursiveCheckSolvable(int x,int z) //GameObject t has to be in the mazeTiles array. x and z are the current indices of t. This method check if there is a path from t to exit tile
    {
        alreadyOnStack[x, z] = true; //when the method concerning (x,z) starts executing, indicates that it is already on the runtime stack. Makes sure that future recursive calls don't come back.
        if (x==exitX && z==exitZ) //if the current tile is an exit tile, then there is a math of length 0
            return true;
        else
        {
            Boolean returnVal = false; //identity for OR operation
            
            GameObject eastExtension = mazeTiles[x, z].transform.GetChild((int)TileChildren.EAST).gameObject;
            if (eastExtension.activeSelf && !alreadyOnStack[x+1,z] && mazeTiles[x+1,z].transform.GetChild((int)TileChildren.HEART).gameObject.activeSelf) //if the east extension is active (there is an edge), the east neighbor has not yet been visited, and the east neighbor has not been destroyed
            {
                returnVal = returnVal || recursiveCheckSolvable(x + 1, z);
                
            }

            GameObject westExtension = mazeTiles[x, z].transform.GetChild((int)TileChildren.WEST).gameObject; //idem
            if(westExtension.activeSelf && !alreadyOnStack[x-1,z] && mazeTiles[x - 1, z].transform.GetChild((int)TileChildren.HEART).gameObject.activeSelf)
            {
                returnVal = returnVal || recursiveCheckSolvable(x - 1, z);
            }

            GameObject northExtension = mazeTiles[x, z].transform.GetChild((int)TileChildren.NORTH).gameObject;
            if(northExtension.activeSelf && !alreadyOnStack[x,z+1] && mazeTiles[x , z+1].transform.GetChild((int)TileChildren.HEART).gameObject.activeSelf)
            {
                returnVal = returnVal || recursiveCheckSolvable(x, z + 1);
            }

            GameObject southExtension = mazeTiles[x, z].transform.GetChild((int)TileChildren.SOUTH).gameObject;
            if(southExtension.activeSelf && !alreadyOnStack[x,z-1] && mazeTiles[x , z-1].transform.GetChild((int)TileChildren.HEART).gameObject.activeSelf)
            {
                returnVal = returnVal || recursiveCheckSolvable(x, z - 1);
            }

            return returnVal;
        }
    }

    public void tileDestroyed() //informs the controller that a tile has been destroyed.
    {
        if(!checkSolvable()) //if the maze is no longer solvable, then inform the WinLose controller that the player has won.
        {
            winLoseController.GetComponent<WinLoseControl>().win();
        }
    }
}
