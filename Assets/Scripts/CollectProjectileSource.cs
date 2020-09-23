using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this script detects player's collision with the projectile source, makes 
//the projectile source disappear, and add one to the projectile source count
//in the UI.
public class CollectProjectileSource : MonoBehaviour
{
    Boolean hasAdded = false;
    void OnTriggerEnter(Collider col)
    {
        if(!hasAdded) //this check is needed to prevent the same projectile source from added twice.
        {
            GetComponent<Renderer>().enabled = false; //make the projectile source disappear
            GameObject controllerGameObject = GameObject.Find("ProjectileSourceNumberController");
            ProjectileSourceCtrl controllerScript = (ProjectileSourceCtrl)controllerGameObject.GetComponent(typeof(ProjectileSourceCtrl));
            controllerScript.addProjectileSource();
            hasAdded = true; //make sure that the same projectile can only be added once, even if collision happened twice (consecutively)
        }
        
    }
}
