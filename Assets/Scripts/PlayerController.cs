using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(WeaponPicker))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isSecondaryCharacter;

    Mover _mover;
    WeaponPicker _weaponPicker;
    private SlamAttackExecutor slamAttackExecutor;

    void Awake()
    {
        slamAttackExecutor = GetComponentInChildren<SlamAttackExecutor>();

        _mover = GetComponent<Mover>();

        _weaponPicker = GetComponent<WeaponPicker>();
    }

    void OnEnable()
    {
        if (!isSecondaryCharacter)
        {
            PlayerInputHandler.Instance.OnMoveInput += _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPickupInput += _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnAttackInputDown += slamAttackExecutor.RaiseWeapon;

            PlayerInputHandler.Instance.OnAttackInputUp += slamAttackExecutor.SlamWeapon;
        }
        else
        {
            PlayerInputHandler.Instance.OnPlayerTwoMoveInput += _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPlayerTwoPickupInput += _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputDown += slamAttackExecutor.RaiseWeapon;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputUp += slamAttackExecutor.SlamWeapon;
        }
    }

    void OnDisable()
    {
        if (!isSecondaryCharacter)
        {
            PlayerInputHandler.Instance.OnMoveInput -= _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPickupInput -= _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnAttackInputDown -= slamAttackExecutor.RaiseWeapon;

            PlayerInputHandler.Instance.OnAttackInputUp -= slamAttackExecutor.SlamWeapon;
        }
        else
        {
            PlayerInputHandler.Instance.OnPlayerTwoMoveInput -= _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPlayerTwoPickupInput -= _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputDown -= slamAttackExecutor.RaiseWeapon;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputUp -= slamAttackExecutor.SlamWeapon;
        }
    }
}
