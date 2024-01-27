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
    [SerializeField] private Transform weaponSlot;

    [Header("Player Propertiers")]
    [SerializeField] private bool isSecondaryPlayer;

    private SlammingWeapon wieldedMeleeWeapon;

    private void OnEnable()
    {
        slamAttackExecutor.OnSlamInitiated += LockMovement;

        slamAttackExecutor.OnSlamEnded += UnlockMovement;

        slamAttackExecutor.OnSlamCancelled += UnlockMovement;

        if (!isSecondaryPlayer)
        {
            PlayerInputHandler.Instance.OnMoveInput += mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnAttackInputDown += slamAttackExecutor.RaiseWeapon;

            PlayerInputHandler.Instance.OnAttackInputUp += slamAttackExecutor.SlamWeapon;
        }
        else
        {
            PlayerTwoInputHandler.Instance.OnMoveInput += mover.ResolveMoveInput;

            PlayerTwoInputHandler.Instance.OnAttackInputDown += slamAttackExecutor.RaiseWeapon;

            PlayerTwoInputHandler.Instance.OnAttackInputUp += slamAttackExecutor.SlamWeapon;
        }
    }

    private void OnDisable()
    {
        slamAttackExecutor.OnSlamInitiated -= LockMovement;

        slamAttackExecutor.OnSlamEnded -= UnlockMovement;

        slamAttackExecutor.OnSlamCancelled -= UnlockMovement;

        if (!isSecondaryPlayer)
        {
            PlayerInputHandler.Instance.OnMoveInput -= mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnAttackInputDown -= slamAttackExecutor.RaiseWeapon;

            PlayerInputHandler.Instance.OnAttackInputUp -= slamAttackExecutor.SlamWeapon;


        }
        else
        {
            PlayerTwoInputHandler.Instance.OnMoveInput -= mover.ResolveMoveInput;

            PlayerTwoInputHandler.Instance.OnAttackInputDown -= slamAttackExecutor.RaiseWeapon;

            PlayerTwoInputHandler.Instance.OnAttackInputUp -= slamAttackExecutor.SlamWeapon;
        }
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
