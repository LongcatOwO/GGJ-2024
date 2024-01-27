using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Character))]
public class WeaponPicker : MonoBehaviour
{
    Character _character;
    DroppedWeapon _weaponInRange;

    void Awake()
    {
        _character = GetComponent<Character>();
    }

    public void Pickup()
    {
        if (_weaponInRange == null) return;
        var heldWeapon = Instantiate(_weaponInRange.Info.HeldForm).GetComponent<SlammingWeapon>();
        _character.PickupWeapon(heldWeapon);
        Destroy(_weaponInRange.gameObject);
        _weaponInRange = null;
    }

    void OnTriggerEnter(Collider collider)
    {
        var weapon = collider.GetComponentInParent<DroppedWeapon>();
        if (weapon == null) return;
        _weaponInRange = weapon;
    }

    void OnTriggerExit(Collider collider)
    {
        var weapon = collider.GetComponentInParent<DroppedWeapon>();
        if (_weaponInRange == weapon)
            _weaponInRange = null;
    }
}
