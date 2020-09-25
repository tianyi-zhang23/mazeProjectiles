using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script controls the number of projectile source the player has.
public class ProjectileSourceCtrl : MonoBehaviour
{
    public Text text;
    private int numOfProjSource = 0;
    public void addProjectileSource()
    {
        numOfProjSource += 1;
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
}
