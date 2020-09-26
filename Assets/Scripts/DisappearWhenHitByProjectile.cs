using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearWhenHitByProjectile : MonoBehaviour
{
    GameObject mazeController;
    //this script is for the tiles of the maze, which should disappear when hit by a fired projectile
    //this script also calls the maze controller to inform the controller of the distruction of this tile

    private void Start()
    {
        mazeController = GameObject.Find("MazeController");

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("FiredProjectile"))
        {
            //if one of the tiles that make up the complete tile is hit, everything of this entire tile disappears.
            for(int i=0;i< gameObject.transform.parent.gameObject.transform.childCount; i++)
            {
                gameObject.transform.parent.gameObject.transform.GetChild(i).gameObject.SetActive(false); //mae every sub tile disappear.
            }
            mazeController.GetComponent<MazeControl>().tileDestroyed(); //informs the maze controller that a tile has been destroyed. This comes after the current gameObject has been deactivated since the MazeControl script depends on this.
        }
    }
}
