using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private float inertiaFactor;
    [SerializeField] private bool isUsingInertia;

    private float forwardMovement;
    private float sideMovement;

    private float currentSideSpeed;
    private float currentForwardSpeed;

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
        //Normal movt
        if (!isUsingInertia)
        {
            if (forwardMovement != 0)
            {
                transform.position += Mathf.Sign(forwardMovement) * moveSpeed * Time.deltaTime * -transform.forward;
            }

            if (sideMovement != 0)
            {
                transform.position += Mathf.Sign(sideMovement) * moveSpeed * Time.deltaTime * transform.right;
            }
        }
        //Inertia movt (ice physics)
        else
        {
            if (forwardMovement != 0)
            {
                currentForwardSpeed = Mathf.Lerp(currentForwardSpeed, Mathf.Sign(forwardMovement) * moveSpeed, inertiaFactor * Time.deltaTime);
            }
            else
            {
                currentForwardSpeed = Mathf.Lerp(currentForwardSpeed, 0, inertiaFactor * Time.deltaTime);
            }

            if (sideMovement != 0)
            {
                currentSideSpeed = Mathf.Lerp(currentSideSpeed, Mathf.Sign(sideMovement) * moveSpeed, inertiaFactor * Time.deltaTime);
            }
            else
            {
                currentSideSpeed = Mathf.Lerp(currentSideSpeed, 0, inertiaFactor * Time.deltaTime);
            }
            transform.position += currentForwardSpeed * Time.deltaTime * -transform.forward;
            transform.position += currentSideSpeed * Time.deltaTime * transform.right;
        }
    }

    private void ResolveMoveInput(Vector2 vector)
    {
       forwardMovement = vector.x;

        sideMovement = vector.y;
    }
}
