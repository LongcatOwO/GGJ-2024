using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlammableWeapon))]
public class SpawnWeaponOnHit : MonoBehaviour
{
    [SerializeField]
    private WeaponList _weaponList;

    [SerializeField]
    private float _spawnRadius;

    [SerializeField]
    private int _spawnCount;

    private SlammableWeapon _weapon;

    private void Awake()
    {
        _weapon = GetComponent<SlammableWeapon>();
        _weapon.OnSlamHit += SpawnWeapons;
    }

    private void SpawnWeapons(Collider collider)
    {
        for (int i = 0; i < _spawnCount; ++i)
        {
            int rand = Random.Range(0, _weaponList.weaponInfos.Length);
            float angle = Random.Range(0.0f, 360.0f);
            Vector3 offset = Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(_spawnRadius, 0, 0);
            Vector3 position = collider.transform.position + offset;
            Instantiate(_weaponList.weaponInfos[rand].PickupForm, offset, Quaternion.identity);
        }
    }
}
