using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Character : MonoBehaviour
{
    //This class defines the host game object as a character and as a result, can be "slammed".

    [Header("Component References")]
    [SerializeField] private Mover mover;
    [SerializeField] private AttackAnimationEvents attackEvents;
    [SerializeField] private Transform weaponSlot;

    private SlammableWeapon wieldedMeleeWeapon;

    private void OnEnable()
    {
        attackEvents.OnAttackInitiated += LockMovement;

        attackEvents.OnAttackEnded += UnlockMovement;

        attackEvents.OnAttackCancelled += UnlockMovement;
    }

    private void OnDisable()
    {
        attackEvents.OnAttackInitiated -= LockMovement;

        attackEvents.OnAttackEnded -= UnlockMovement;

        attackEvents.OnAttackCancelled -= UnlockMovement;
    }

    public void PickupWeapon(SlammableWeapon weapon)
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
