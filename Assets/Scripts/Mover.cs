using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //This class handles the movement of its host.

    [Header("Component References")]
    [SerializeField] private CapsuleCollider hostCapsuleCollider;

    [Header("Movement Properties")]
    [SerializeField] private float moveSpeed;

    [Header("Movement Collision Check Properties")]
    [SerializeField] private LayerMask moveCollisionLayerMask;
    [SerializeField] private float movementCheckDistanceError;

    private bool isLocked;
    private float forwardMovementDirection;
    private float sidewaysMovementDirection;
    private float effectiveMoveCapsuleCastRadius;
    private float effectiveMoveCapsuleCastHeight;

    private void Awake()
    {
        float moveCapsuleCastRadius = hostCapsuleCollider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
        
        effectiveMoveCapsuleCastRadius = moveCapsuleCastRadius + movementCheckDistanceError;

        effectiveMoveCapsuleCastHeight = (hostCapsuleCollider.height / 2 - moveCapsuleCastRadius) * transform.lossyScale.y;
    }

    private void OnEnable()
    {
        PlayerInputHandler.Instance.OnMoveInput += ResolveMoveInput;
    }

    private void OnDisable()
    {
        PlayerInputHandler.Instance.OnMoveInput -= ResolveMoveInput;
    }

    //Evaluates whether the movement will be obstructed via collision, then executes unobstructed movement.
    private void Update()
    {
        if (isLocked)
        {
            return;
        }

        Vector3 moveDirection = Vector3.zero;

        Vector3 topCapsuleSpherePosition = transform.position + effectiveMoveCapsuleCastHeight * Vector3.up;

        Vector3 bottomCapsuleSpherePosition = transform.position - effectiveMoveCapsuleCastHeight * Vector3.up;

        float moveDistance = moveSpeed * Time.deltaTime;

        if (forwardMovementDirection != 0)
        {
            Vector3 forwardMoveDirection = Mathf.Sign(forwardMovementDirection) * transform.right;

            Vector3 forwardCapsuleCastOriginPositionOffset = forwardMoveDirection * movementCheckDistanceError;

            if (!Physics.CapsuleCast(topCapsuleSpherePosition - forwardCapsuleCastOriginPositionOffset, bottomCapsuleSpherePosition - forwardCapsuleCastOriginPositionOffset, effectiveMoveCapsuleCastRadius, forwardMoveDirection, moveDistance + movementCheckDistanceError, moveCollisionLayerMask))
            {
                moveDirection += forwardMoveDirection;
            }
        }

        if(sidewaysMovementDirection != 0)
        {
            Vector3 sidewaysMoveDirection = Mathf.Sign(sidewaysMovementDirection) * transform.forward;

            Vector3 sidewaysCapsuleCastOriginPositionOffset = sidewaysMoveDirection * movementCheckDistanceError;

            if (!Physics.CapsuleCast(topCapsuleSpherePosition - sidewaysCapsuleCastOriginPositionOffset, bottomCapsuleSpherePosition - sidewaysCapsuleCastOriginPositionOffset, effectiveMoveCapsuleCastRadius, sidewaysMoveDirection, moveDistance + movementCheckDistanceError, moveCollisionLayerMask))
            {
                moveDirection += sidewaysMoveDirection;
            }
        }

        if(moveDirection == Vector3.zero)
        {
            return;
        }
        else
        {
            moveDirection = moveDirection.normalized;
        }

        transform.position += moveDirection * moveDistance;
    }

    public void SetMovementLock(bool isLocked)
    {
        this.isLocked = isLocked;
    }

    private void ResolveMoveInput(Vector2 vector)
    {
        forwardMovementDirection = vector.x;

        sidewaysMovementDirection = vector.y;
    }
}
