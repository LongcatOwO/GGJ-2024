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
        if(other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            //player 1 scores
            if (playerController.IsSecondaryPlayer)
            {
                scoreComponent.Player1Score++;
            }
            //player 2 scores
            else
            {
                scoreComponent.Player2Score++;
            }
        }

        Destroy(other.gameObject);

        //insert UI stuff (Game Over screen here)

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
