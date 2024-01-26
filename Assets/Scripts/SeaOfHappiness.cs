using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaOfHappiness : MonoBehaviour
{
    //This class acts to destroy all objects the enters its collider, sending them to "a happy place".
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
