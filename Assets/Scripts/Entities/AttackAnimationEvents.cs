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
    [SerializeField] private SlammingWeapon weapon;
    [SerializeField] private float minimumRequiredAttackChargeProgress;
    [SerializeField] private float chargeEffectMultiplier;

    public void EndWeaponCharge()
    {
        animator.SetBool("isChargingWeapon", false);

        OnAttackEnded?.Invoke();
    }

    public void ChargeWeapon()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("StartWeaponCharging"))
        {
            float slamProgress = Mathf.Clamp01(1 - animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

            animator.Play("EndWeaponCharging", 0, slamProgress);

            if (Mathf.Abs(slamProgress - 1) >= minimumRequiredAttackChargeProgress)
            {
                OnAttackExecuted?.Invoke((1 - slamProgress) * chargeEffectMultiplier);
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