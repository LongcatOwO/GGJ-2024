using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //This class handles the movement of its host.

    [SerializeField] private float moveSpeed;

    private bool isLocked;
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
        if (isLocked)
        {
            return;
        }

        if (forwardMovement != 0)
        {
            transform.position += Mathf.Sign(forwardMovement) * moveSpeed * Time.deltaTime * transform.right;
        }

        if(sideMovement != 0)
        {
            transform.position += Mathf.Sign(sideMovement) * moveSpeed * Time.deltaTime * transform.forward;
        }
    }

    public void SetMovementLock(bool isLocked)
    {
        this.isLocked = isLocked;
    }

    private void ResolveMoveInput(Vector2 vector)
    {
        forwardMovement = vector.x;

        sideMovement = vector.y;
    }
}
