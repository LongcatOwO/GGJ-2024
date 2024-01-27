using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Character : MonoBehaviour
{
    //This class defines the host game object as a character and as a result, can be "slammed".

    [Header("Component References")]
    [SerializeField] private Mover mover;
    [SerializeField] private SlamAttackExecutor slamAttackExecutor;

    private SlammingWeapon wieldedMeleeWeapon;

    private void OnEnable()
    {
        slamAttackExecutor.OnSlamInitiated += LockMovement;

        slamAttackExecutor.OnSlamEnded += UnlockMovement;

        slamAttackExecutor.OnSlamCancelled += UnlockMovement;
    }
    
    private void OnDisable()
    {
        slamAttackExecutor.OnSlamInitiated -= LockMovement;

        slamAttackExecutor.OnSlamEnded -= UnlockMovement;

        slamAttackExecutor.OnSlamCancelled -= UnlockMovement;
    }

    public void PickupWeapon(SlammingWeapon weapon)
    {
        if(wieldedMeleeWeapon != null)
        {
            Destroy(weapon);
        }

        wieldedMeleeWeapon = weapon;

        wieldedMeleeWeapon.InitializeWeapon(gameObject);        
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
