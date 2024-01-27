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

    [Header("Target Properties")]
    [SerializeField] private Character targetCharacter;

    [Header("Movement Properties")]    
    [SerializeField] private float moveSpeed;

    [Header("Attack Properties")]
    [SerializeField] private Vector2 weaponChargeTimeRandomRange = new Vector2(0.30f, 1.1f);

    [Header("Recovery Time")]
    [SerializeField] private Vector2 attackRecoveryTimeRandomRange = new Vector2(0.2f, 2f);

    private CombatantBehaviour activeBehaviour;
    private bool isAttacking;
    //The time required to "recover" from executing an attack to carrying out the next action.
    private float attackRecoveryTime;

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
        //Check to see if there is a viable target. If not, set the combatant's active behaviour to idle.
        if (targetCharacter == null && activeBehaviour != CombatantBehaviour.Idle)
        {
            activeBehaviour = CombatantBehaviour.Idle;

            return;
        }
        
        //If the combatant is not grounded OR is attacking, skip resolving for its active behaviour until it is not.
        if (!groundingChecker.IsGrounded || isAttacking)
        {
            return;
        }

        //If the combatant is still recovering from carrying out an attack, skip resolving for its active behaviour.
        if (attackRecoveryTime > 0)
        {
            attackRecoveryTime -= Time.deltaTime;

            return;
        }        

        //Resolve for the the combatant's active behaviour.
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
        float randomChargeTime = UnityEngine.Random.Range(weaponChargeTimeRandomRange.x, weaponChargeTimeRandomRange.y);

        float elapsedTime = 0f;

        while(elapsedTime < randomChargeTime)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        attackEvents.ExecuteWeaponAttack();

        attackRecoveryTime = UnityEngine.Random.Range(attackRecoveryTimeRandomRange.x, attackRecoveryTimeRandomRange.y);
    }

    private void ResolveAttackEnd()
    {
        isAttacking = false;
    }

}
