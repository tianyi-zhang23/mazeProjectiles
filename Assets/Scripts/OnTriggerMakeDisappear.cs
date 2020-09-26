using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerMakeDisappear : MonoBehaviour
{
    /*This scrpt is intended for the game objects (projectiles and players) that serve as limits. When a projectile
     * goes beyond these limits, the object traversing the frontier should be destroyed. When a player goes through,
     * the player loses the game*/
    public GameObject playerController;
    public GameObject player;
    public GameObject winLoseController;
    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.ReferenceEquals(other.gameObject, player) || GameObject.ReferenceEquals(other.gameObject, playerController)) //if it is a player that goes through the limit
            winLoseController.GetComponent<WinLoseControl>().lose();
        else //if it is not a player (is a projectile), the destroy it.
            GameObject.Destroy(other.gameObject);
        
    }
}
