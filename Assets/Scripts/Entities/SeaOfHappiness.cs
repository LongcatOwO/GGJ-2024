using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeaOfHappiness : MonoBehaviour
{
    //This class acts to destroy all objects the enters its collider, sending them to "a happy place".
    private void OnTriggerEnter(Collider other)
    {
        GameObject scoreObject = GameObject.Find("ScoreObj");

        if(other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            //player 1 scores
            if (playerController.IsSecondaryPlayer)
            {
                scoreObject.GetComponent<Score>().Player1Score++;
            }
            //player 2 scores
            else
            {
                scoreObject.GetComponent<Score>().Player2Score++;
            }
        }

        Destroy(other.gameObject);

        //insert UI stuff (Game Over screen here)

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
