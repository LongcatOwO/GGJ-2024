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

    private AttackAnimationEvents attackEvents;
    private Mover _mover;
    private WeaponPicker _weaponPicker;
    
    
    void Awake()
    {
        attackEvents = GetComponentInChildren<AttackAnimationEvents>();

        _mover = GetComponent<Mover>();

        _weaponPicker = GetComponent<WeaponPicker>();
    }

    void OnEnable()
    {
        if (!isSecondaryPlayer)
        {
            PlayerInputHandler.Instance.OnMoveInput += _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPickupInput += _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnAttackInputDown += attackEvents.StartWeaponCharge;

            PlayerInputHandler.Instance.OnAttackInputUp += attackEvents.ExecuteWeaponAttack;
        }
        else
        {
            PlayerInputHandler.Instance.OnPlayerTwoMoveInput += _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPlayerTwoPickupInput += _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputDown += attackEvents.StartWeaponCharge;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputUp += attackEvents.ExecuteWeaponAttack;
        }
    }

    void OnDisable()
    {
        if (!isSecondaryPlayer)
        {
            PlayerInputHandler.Instance.OnMoveInput -= _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPickupInput -= _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnAttackInputDown -= attackEvents.StartWeaponCharge;

            PlayerInputHandler.Instance.OnAttackInputUp -= attackEvents.ExecuteWeaponAttack;
        }
        else
        {
            PlayerInputHandler.Instance.OnPlayerTwoMoveInput -= _mover.ResolveMoveInput;

            PlayerInputHandler.Instance.OnPlayerTwoPickupInput -= _weaponPicker.Pickup;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputDown -= attackEvents.StartWeaponCharge;

            PlayerInputHandler.Instance.OnPlayerTwoAttackInputUp -= attackEvents.ExecuteWeaponAttack;
        }
    }
}
