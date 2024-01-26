using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class GroundingChecker : MonoBehaviour
{
    public event Action<bool> OnChangeToNewGroundingState;

    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckError;

    private bool isGrounded;
    private float effectiveSphereCastDistance;
    private float effectiveSphereCastRadius;

    private void Awake()
    {
        CapsuleCollider hostCapsuleCollider = GetComponent<CapsuleCollider>();

        effectiveSphereCastRadius = hostCapsuleCollider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);

        effectiveSphereCastDistance = hostCapsuleCollider.height / 2 * transform.lossyScale.y + groundCheckError -effectiveSphereCastRadius;
    }

    private void Start()
    {
        isGrounded = EvaluateGrounding();

        OnChangeToNewGroundingState(isGrounded);
    }

    private void Update()
    {
        bool newGroundingState = EvaluateGrounding();

        if (newGroundingState)
        {
            if (!isGrounded)
            {
                isGrounded = true;

                OnChangeToNewGroundingState?.Invoke(true);
            }            
        }
        else
        {
            if (isGrounded)
            {
                isGrounded = false;

                OnChangeToNewGroundingState?.Invoke(false);
            }
        }
    }

    public bool EvaluateGrounding()
    {
        Ray groundCheckRay = new Ray(transform.position, Vector2.down);

        if (Physics.SphereCast(groundCheckRay, effectiveSphereCastRadius, effectiveSphereCastDistance, groundLayerMask))
        {
            return true;
        }

        return false;
    }
}
