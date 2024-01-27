using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int Player1Score = 0;
    public int Player2Score = 0;


    // Start is called before the first frame update
    void Start()
    {
        //persistance
        DontDestroyOnLoad(gameObject);
    }
}
