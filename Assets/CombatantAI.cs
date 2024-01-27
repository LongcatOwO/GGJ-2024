using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantAI : MonoBehaviour
{
    private enum CombatantBehaviour { Idle, PursueTarget, AttackTarget }

    [Header("Component References")]
    [SerializeField] private Mover mover;
    [SerializeField] private AttackAnimationEvents attackEvents;
    [SerializeField] private ProximityCharacterDetector proximityDetector;
    [SerializeField] private GroundingChecker groundingChecker;

    [Header("Behaviour Properties")]
    private CombatantBehaviour activeBehaviour;
    [SerializeField] private float moveSpeed;

    [Header("Target Properties")]
    [SerializeField] private Character targetCharacter;    

    private void Start()
    {
        if(targetCharacter != null)
        {
            activeBehaviour = CombatantBehaviour.PursueTarget;
        }
    }

    private void OnEnable()
    {
        proximityDetector.OnCharacterDetectedInProximity += InitiateAttack;

        proximityDetector.OnCharacterLeaveProximity += PursueTarget;
    }
    
    private void OnDisable()
    {
        proximityDetector.OnCharacterDetectedInProximity -= InitiateAttack;

        proximityDetector.OnCharacterLeaveProximity -= PursueTarget;
    }

    private void Update()
    {
        if (!groundingChecker.IsGrounded)
        {
            return;
        }

        switch (activeBehaviour)
        {
            case CombatantBehaviour.Idle:
                {
                    return;
                }
                break;
            case CombatantBehaviour.PursueTarget:
                {
                    PursueTarget();
                }
                break;
            case CombatantBehaviour.AttackTarget:
                {
                    return;
                }
                break;
        }
    }

    private void PursueTarget()
    {
        Vector3 targetPosition = Vector3.ProjectOnPlane(targetCharacter.transform.position, transform.up);

        targetPosition.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void InitiateAttack()
    {
        activeBehaviour = CombatantBehaviour.AttackTarget;
    }
}
