using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(WeaponPicker))]
public class PlayerController : MonoBehaviour
{
    public bool IsSecondaryPlayer { get { return isSecondaryPlayer; } }

    [SerializeField] private bool isSecondaryPlayer;

    private AttackAnimationEvents slamAttacker;
    private Mover _mover;
    private WeaponPicker _weaponPicker;
    
    
    void Awake()
    {
        slamAttacker = GetComponentInChildren<AttackAnimationEvents>();

        _mover = GetComponent<Mover>();

        _weaponPicker = GetComponent<WeaponPicker>();
    }

    void OnEnable()
    {
        if (!isSecondaryPlayer)
        {
            PlayerInputHandler.Instance.OnMoveInput += _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPickupInput += _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnAttackInputDown += slamAttacker.StartWeaponCharge;

            PlayerInputHandler.Instance.OnAttackInputUp += slamAttacker.ChargeWeapon;
        }
        else
        {
            PlayerInputHandler.Instance.OnPlayerTwoMoveInput += _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPlayerTwoPickupInput += _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputDown += slamAttacker.StartWeaponCharge;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputUp += slamAttacker.ChargeWeapon;
        }
    }

    void OnDisable()
    {
        if (!isSecondaryPlayer)
        {
            PlayerInputHandler.Instance.OnMoveInput -= _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPickupInput -= _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnAttackInputDown -= slamAttacker.StartWeaponCharge;

            PlayerInputHandler.Instance.OnAttackInputUp -= slamAttacker.ChargeWeapon;
        }
        else
        {
            PlayerInputHandler.Instance.OnPlayerTwoMoveInput -= _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPlayerTwoPickupInput -= _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputDown -= slamAttacker.StartWeaponCharge;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputUp -= slamAttacker.ChargeWeapon;
        }
    }
}
