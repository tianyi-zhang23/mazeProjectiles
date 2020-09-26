using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script controls the number of projectile source the player has.
public class ProjectileSourceCtrl : MonoBehaviour
{
    public Text text;
    public GameObject winLoseController;
    private int numOfProjSource = 0;
    private int numOfProjSourceUncollected;
    private bool projSourceUncollectedInitiated = false;

    public void setTotalNumberOfProjSourceUncollected(int n)
    {
        numOfProjSourceUncollected = n;
        projSourceUncollectedInitiated = true;
    }    
    public void addProjectileSource()
    {
        numOfProjSource += 1;
        numOfProjSourceUncollected--; //there is one fewer projectile available to collect.
        updateText();
    }

    public void deleteProjectileSource() //decrement projectile source
    {
        numOfProjSource -= (numOfProjSource>0)?1:0; //only decrement number of projectile source if there still is one left
        updateText();
    }

    public bool hasProjectileSource() //returns true if there is still projectile source
    {
        return numOfProjSource > 0;
    }

    private void updateText()
    {
        text.text = "You have " + numOfProjSource + " projectile source(s)";
    }

    void Update()
    {
        if(projSourceUncollectedInitiated && numOfProjSourceUncollected==0 && numOfProjSource==0) //if there is no more projectile source to collect and the player has wasted all the proj sources, then the player loses.
        {
            winLoseController.GetComponent<WinLoseControl>().lose();
        }
    }
}
