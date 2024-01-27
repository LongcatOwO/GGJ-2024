using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(IPlayerInputHandler))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(WeaponPicker))]
public class PlayerController : MonoBehaviour
{
    Character _character;
    IPlayerInputHandler _inputHandler;
    Mover _mover;
    WeaponPicker _weaponPicker;

    void Awake()
    {
        _character = GetComponent<Character>();
        _inputHandler = GetComponent<IPlayerInputHandler>();
        _mover = GetComponent<Mover>();
        _weaponPicker = GetComponent<WeaponPicker>();
    }

    void OnEnable()
    {
        _inputHandler.OnMoveInput += _mover.ResolveMoveInput;
        _inputHandler.OnPickupInput += _weaponPicker.Pickup;
        _inputHandler.OnAttackInputDown += _character.GetSlamAttackExecutor().RaiseWeapon;
        _inputHandler.OnAttackInputUp += _character.GetSlamAttackExecutor().SlamWeapon;
    }

    void OnDisable()
    {
        _inputHandler.OnMoveInput -= _mover.ResolveMoveInput;
        _inputHandler.OnPickupInput -= _weaponPicker.Pickup;
        _inputHandler.OnAttackInputDown -= _character.GetSlamAttackExecutor().RaiseWeapon;
        _inputHandler.OnAttackInputUp -= _character.GetSlamAttackExecutor().SlamWeapon;
    }
}
