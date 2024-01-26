using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float forwardMovement;
    private float sideMovement;

    private void OnEnable()
    {
        PlayerInputHandler.Instance.OnMoveInput += ResolveMoveInput;
    }

    private void OnDisable()
    {
        PlayerInputHandler.Instance.OnMoveInput -= ResolveMoveInput;
    }

    private void Update()
    {
        if(forwardMovement != 0)
        {
            transform.position += Mathf.Sign(forwardMovement) * moveSpeed * Time.deltaTime * -transform.forward;
        }

        if(sideMovement != 0)
        {
            transform.position += Mathf.Sign(sideMovement) * moveSpeed * Time.deltaTime * transform.right;
        }
    }

    private void ResolveMoveInput(Vector2 vector)
    {
       forwardMovement = vector.x;

        sideMovement = vector.y;
    }
}
