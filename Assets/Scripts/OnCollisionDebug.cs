using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDebug : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collosion detected");
    }
}
