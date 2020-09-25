using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeControl : MonoBehaviour
{
    //this script is informed when a maze tile is destroyed. It then checks if the maze is still solvable. If not, it calls the WinLose Controller to inform that the player won.
    // Start is called before the first frame update
    private class DecoratedTile
    {
        GameObject tile;
        Boolean destroyed = false;
        public DecoratedTile(GameObject tile)
        {
            this.tile = tile;
        }
    }
    DecoratedTile[,] decoratedMazeTiles;
    public void initialize(GameObject[,] maze)
    {
        decoratedMazeTiles = new DecoratedTile[maze.GetLength(0), maze.GetLength(1)];
        for(int i=0;i<maze.GetLength(0);i++)
        {
            for (int j=0;j<maze.GetLength(1);j++)
            {
                decoratedMazeTiles[i, j] = new DecoratedTile(maze[i,j]);
            }
        }


    }

    private void checkSolvable()
    {

    }

    public void tileDestroyed(GameObject destroyedObject) //informs the controller that a tile has been destroyed.
    {
        
    }
}
