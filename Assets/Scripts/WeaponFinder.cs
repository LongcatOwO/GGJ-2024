using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Character))]
[RequireComponent(typeof(PlayerInputHandler))]
public class WeaponPicker : MonoBehaviour
{
    Character _character;
    PlayerInputHandler _inputHandler;
    SlammingWeapon _weaponInRange;

    void Awake()
    {
        _character = GetComponent<Character>();
        _inputHandler = GetComponent<PlayerInputHandler>();
        _inputHandler.OnPickupInput += OnPickupInput;
    }

    SlammingWeapon GetWeaponInRange()
    {
        return _weaponInRange;
    }

    void OnPickupInput()
    {
        if (_weaponInRange == null) return;
        _character.PickupWeapon(_weaponInRange);
        _weaponInRange = null;
    }

    void OnTriggerEnter(Collider collider)
    {
        var weapon = collider.GetComponent<SlammingWeapon>();
        if (weapon == null) return;
        _weaponInRange = weapon;
    }

    void OnTriggerExit(Collider collider)
    {
        var weapon = collider.GetComponent<SlammingWeapon>();
        if (_weaponInRange == weapon)
            _weaponInRange = null;
    }
}
