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

    public void deleteProjectileSource()
    {
        numOfProjSource -= 1;
        updateText();
    }

    private void updateText()
    {
        text.text = "You have " + numOfProjSource + " projectile source(s)";
    }
}
