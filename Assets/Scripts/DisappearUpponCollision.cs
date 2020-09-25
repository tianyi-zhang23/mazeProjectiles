using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearUpponCollision : MonoBehaviour
{
    //this script is intended for the fired projectiles. They should disappear upon hitting something
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
