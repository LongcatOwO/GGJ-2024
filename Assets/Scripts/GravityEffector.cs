using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundingChecker))]
public class GravityEffector : MonoBehaviour
{
    private GroundingChecker groundingChecker;
    private Vector3 fallVelocity;
    private bool isFalling;

    private void Awake()
    {
        groundingChecker = GetComponent<GroundingChecker>();
    }

    private void OnEnable()
    {
        groundingChecker.OnChangeToNewGroundingState += ResolveGroundingStateChange;
    }

    private void OnDisable()
    {
        groundingChecker.OnChangeToNewGroundingState -= ResolveGroundingStateChange;
    }

    private void Update()
    {
        if (isFalling)
        {
            fallVelocity += Physics.gravity * Time.deltaTime;

            transform.position += fallVelocity * Time.deltaTime;
        }
    }

    private void ResolveGroundingStateChange(bool newGroundingState)
    {
        if (newGroundingState)
        {
            if (isFalling)
            {
                fallVelocity = Vector3.zero;

                isFalling = false;
            }
        }
        else
        {
            isFalling = true;
        }
    }
}
