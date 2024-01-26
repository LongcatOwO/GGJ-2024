using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeaponPicker : MonoBehaviour
{
    SlammingWeapon _weaponInRange;

    public SlammingWeapon GetWeaponInRange()
    {
        return _weaponInRange;
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
