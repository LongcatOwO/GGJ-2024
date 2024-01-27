using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeaOfHappiness : MonoBehaviour
{
    //This class acts to destroy all objects the enters its collider, sending them to "a happy place".

    [SerializeField] private Score scoreComponent;

    private void OnTriggerEnter(Collider other)
    {
        MatchHandler.Instance.EvaluateMatchEnd(other.gameObject);

        Destroy(other.gameObject);        
    }
}
