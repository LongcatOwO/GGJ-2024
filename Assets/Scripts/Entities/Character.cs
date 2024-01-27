using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Character : MonoBehaviour
{
    //This class defines the host game object as a character and as a result, can be "slammed".

    [Header("Component References")]
    [SerializeField] private Mover mover;
    [SerializeField] private AttackAnimationEvents slamAttacker;
    [SerializeField] private Transform weaponSlot;

    private SlammingWeapon wieldedMeleeWeapon;

    private void OnEnable()
    {
        slamAttacker.OnAttackInitiated += LockMovement;

        slamAttacker.OnAttackEnded += UnlockMovement;

        slamAttacker.OnAttackCancelled += UnlockMovement;
    }

    private void OnDisable()
    {
        slamAttacker.OnAttackInitiated -= LockMovement;

        slamAttacker.OnAttackEnded -= UnlockMovement;

        slamAttacker.OnAttackCancelled -= UnlockMovement;
    }

    public void PickupWeapon(SlammingWeapon weapon)
    {
        if(wieldedMeleeWeapon != null)
        {
            Instantiate(weapon.Info.DroppedForm, transform.position, Quaternion.identity);

            Destroy(wieldedMeleeWeapon);
        }

        wieldedMeleeWeapon = weapon;

        wieldedMeleeWeapon.InitializeWeapon(gameObject);        

        wieldedMeleeWeapon.transform.SetParent(weaponSlot, false);
    }

    public void LockMovement()
    {
        mover.SetMovementLock(true);
    }

    public void UnlockMovement()
    {
        mover.SetMovementLock(false);
    }
}
