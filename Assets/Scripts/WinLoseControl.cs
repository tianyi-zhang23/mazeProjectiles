using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WinLoseControl : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject player;
    public float minZToWin = 268f;
    private bool hasWon;
    //this script controls the winning and losing of the game. Other components must use call win() and lose() to make the player win or lose.
    //It will still check that the player has walked past the canyon, since the player is supposed to destroy the maze after he/she has crossed the canyon.
    public void win()
    {
        if (player.transform.position.z >= minZToWin)
        {
            hasWon = true;
            winPanel.SetActive(true);
        }
    }

    public void lose()
    {
        StartCoroutine(loseCoroutine());
        
    }  
    
    IEnumerator loseCoroutine()
    {
        yield return new WaitForSeconds(3);
        if (!hasWon)
        {
            losePanel.SetActive(true);
            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<FirstPersonController>().enabled = false;
        }
    }
}
