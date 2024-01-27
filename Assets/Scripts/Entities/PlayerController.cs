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

    private SlamAttackExecuter slamAttacker;
    private Mover _mover;
    private WeaponPicker _weaponPicker;
    
    
    void Awake()
    {
        slamAttacker = GetComponentInChildren<SlamAttackExecuter>();

        _mover = GetComponent<Mover>();

        _weaponPicker = GetComponent<WeaponPicker>();
    }

    void OnEnable()
    {
        if (!isSecondaryPlayer)
        {
            PlayerInputHandler.Instance.OnMoveInput += _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPickupInput += _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnAttackInputDown += slamAttacker.RaiseWeapon;

            PlayerInputHandler.Instance.OnAttackInputUp += slamAttacker.SlamWeapon;
        }
        else
        {
            PlayerInputHandler.Instance.OnPlayerTwoMoveInput += _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPlayerTwoPickupInput += _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputDown += slamAttacker.RaiseWeapon;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputUp += slamAttacker.SlamWeapon;
        }
    }

    void OnDisable()
    {
        if (!isSecondaryPlayer)
        {
            PlayerInputHandler.Instance.OnMoveInput -= _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPickupInput -= _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnAttackInputDown -= slamAttacker.RaiseWeapon;

            PlayerInputHandler.Instance.OnAttackInputUp -= slamAttacker.SlamWeapon;
        }
        else
        {
            PlayerInputHandler.Instance.OnPlayerTwoMoveInput -= _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPlayerTwoPickupInput -= _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputDown -= slamAttacker.RaiseWeapon;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputUp -= slamAttacker.SlamWeapon;
        }
    }
}
