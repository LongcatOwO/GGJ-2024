using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationEvents : MonoBehaviour
{
    //This class handlers the attack animation of melee "slammable" weapons.

    public event Action OnAttackInitiated;
    public event Action<float> OnAttackExecuted;
    public event Action OnAttackEnded;
    public event Action OnAttackCancelled;

    [SerializeField] private Animator animator;
    [SerializeField] private SlammableWeapon weapon;
    [SerializeField] private float minimumRequiredAttackChargeProgress;

    public void EndWeaponCharge()
    {
        animator.SetBool("isChargingWeapon", false);

        OnAttackEnded?.Invoke();
    }

    public void ExecuteWeaponAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("StartWeaponCharging"))
        {
            float chargeProgress = Mathf.Clamp01(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

            animator.Play("EndWeaponCharging", 0, 1 - chargeProgress);

            if (chargeProgress >= minimumRequiredAttackChargeProgress)
            {
                OnAttackExecuted?.Invoke(chargeProgress);
            }
            else
            {
                OnAttackCancelled?.Invoke();
            }
        }
        else if(animator.GetBool("isChargingWeapon"))
        {
            animator.Play("WeaponIdle");

            animator.SetBool("isChargingWeapon", false);

            OnAttackCancelled?.Invoke();
        }
    }

    public void StartWeaponCharge()
    {
        if (!animator.GetBool("isChargingWeapon"))
        {
            animator.SetBool("isChargingWeapon", true);

            animator.SetTrigger("startWeaponCharging");

            OnAttackInitiated?.Invoke();
        }
    }
}
