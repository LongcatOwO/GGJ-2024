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

    [Header("Move Properties")]    
    [SerializeField] private float moveSpeed;

    [Header("Target Properties")]
    [SerializeField] private Character targetCharacter;

    [SerializeField] private CombatantBehaviour activeBehaviour;
    [SerializeField] private bool isAttacking;
    private float attackRecoveryRequiredTime;

    [Header("Attack Time Properties")]
    [SerializeField] private float minWeaponChargeTime = 0.30f;
    [SerializeField] private float maxWeaponChargeTime = 1.1f;
    [SerializeField] private float minAttackRecoveryTime = 0.2f;
    [SerializeField] private float maxAttackRecoveryTime = 1f;

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

        proximityDetector.OnCharacterLeaveProximity += InitiatePursuit;

        attackEvents.OnAttackEnded += ResolveAttackEnd;
    }

    
    private void OnDisable()
    {
        proximityDetector.OnCharacterDetectedInProximity -= InitiateAttack;

        proximityDetector.OnCharacterLeaveProximity -= InitiatePursuit;

        attackEvents.OnAttackEnded -= ResolveAttackEnd;
    }

    private void Update()
    {
        if(targetCharacter == null && activeBehaviour != CombatantBehaviour.Idle)
        {
            activeBehaviour = CombatantBehaviour.Idle;

            return;
        }

        if (!groundingChecker.IsGrounded || isAttacking)
        {
            return;
        }

        switch (activeBehaviour)
        {
            case CombatantBehaviour.Idle:
                {
                    return;
                }
            case CombatantBehaviour.PursueTarget:
                {
                    PursueTarget();
                }
                break;
            case CombatantBehaviour.AttackTarget:
                {
                    if (!isAttacking)
                    {
                        if (attackRecoveryRequiredTime > 0)
                        {
                            attackRecoveryRequiredTime -= Time.deltaTime;

                            return;
                        }

                        isAttacking = true;

                        attackEvents.StartWeaponCharge();

                        StartCoroutine(ExecuteWeaponAttackAtRandomCharge());
                    }
                    return;
                }
        }
    }

    public void SetTarget(Character targetCharacter)
    {
        this.targetCharacter = targetCharacter;

        InitiatePursuit();
    }

    private void InitiatePursuit()
    {
        activeBehaviour = CombatantBehaviour.PursueTarget;
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

    private IEnumerator ExecuteWeaponAttackAtRandomCharge()
    {
        float randomChargeTime = UnityEngine.Random.Range(minWeaponChargeTime, maxWeaponChargeTime);

        float elapsedTime = 0f;

        while(elapsedTime < randomChargeTime)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        attackEvents.ExecuteWeaponAttack();

        attackRecoveryRequiredTime = UnityEngine.Random.Range(minAttackRecoveryTime, maxAttackRecoveryTime);
    }

    private void ResolveAttackEnd()
    {
        isAttacking = false;
    }

}
