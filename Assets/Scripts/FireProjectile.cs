using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class FireProjectile : MonoBehaviour
{
    public GameObject projectileSourceController; //the controller's script has the number of projectile sources
    public GameObject projectile; //the projectile to be shot
    public GameObject playerCamera;
    public float forceScale;
    //This script allows the player to file a projectile once he/she clicks the left mouse button.
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && projectileSourceController.GetComponent<ProjectileSourceCtrl>().hasProjectileSource()) //on left mouse click
        {
            
            GameObject thisProjectile = Instantiate(projectile, playerCamera.GetComponent<Camera>().transform.position, playerCamera.GetComponent<Camera>().transform.rotation); //the projectile gets the player's rotation value
            Vector3 forceToAdd = Vector3.Scale(new Vector3(forceScale, forceScale, forceScale), playerCamera.GetComponent<Camera>().transform.forward); //find the force vector scaled up according to the value specified, in the direction of the player's camera
            thisProjectile.GetComponent<Rigidbody>().AddForce(forceToAdd,ForceMode.Impulse) ;
            projectileSourceController.GetComponent<ProjectileSourceCtrl>().deleteProjectileSource();
        }
    }
}
