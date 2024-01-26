using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamAttackExecutor : MonoBehaviour
{
    //This class handlers the attack animation of melee "slammable" weapons.

    public event Action<float> OnSlamMagnitudeExecuted;
    public event Action OnSlamEnded;

    [SerializeField] private Animator animator;
    [SerializeField] private SlammingWeapon weapon;
    [SerializeField] private float minimumRequiredSlamProgress;
    [SerializeField] private float slamMagnitudeMultiplier;

    private void OnEnable()
    {
        PlayerInputHandler.Instance.OnAttackInputDown += RaiseWeapon;

        PlayerInputHandler.Instance.OnAttackInputUp += SlamWeapon;
    }
    
    private void OnDisable()
    {
        PlayerInputHandler.Instance.OnAttackInputDown -= RaiseWeapon;

        PlayerInputHandler.Instance.OnAttackInputUp -= SlamWeapon;
    }

    public void EndWeaponSlam()
    {
        animator.SetBool("isSlammingWeapon", false);

        OnSlamEnded?.Invoke();        
    }

    private void SlamWeapon()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Character_RaiseWeapon"))
        {
            float slamProgress = Mathf.Clamp01(1 - animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

            animator.Play("Character_SlamWeapon", 0, slamProgress);

            if (Mathf.Abs(slamProgress - 1) >= minimumRequiredSlamProgress)
            {
                OnSlamMagnitudeExecuted?.Invoke((1 - slamProgress) * slamMagnitudeMultiplier);
            }
        }
        else if(animator.GetBool("isSlammingWeapon"))
        {
            animator.Play("Character_Idle");

            animator.SetBool("isSlammingWeapon", false);
        }
    }

    private void RaiseWeapon()
    {
        if (!animator.GetBool("isSlammingWeapon"))
        {
            animator.SetBool("isSlammingWeapon", true);

            animator.SetTrigger("raiseWeaponTrigger");
        }
    }
}
