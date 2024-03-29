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

    [SerializeField]
    private float _spawnHeightOffset;

    private SlammableWeapon _weapon;

    private void Awake()
    {
        _weapon = GetComponent<SlammableWeapon>();
    }

    private void OnEnable()
    {
        _weapon.OnSlamHit += SpawnWeapons;
    }

    private void OnDisable()
    {
        _weapon.OnSlamHit -= SpawnWeapons;
    }

    private void SpawnWeapons(Collider collider)
    {
        for (int i = 0; i < _spawnCount; ++i)
        {
            int rand = Random.Range(0, _weaponList.weaponInfos.Length);
            float angle = Random.Range(0.0f, 360.0f);
            Vector3 offset = Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(_spawnRadius, 0, 0);
            Vector3 position = collider.transform.position + offset + new Vector3(0, _spawnHeightOffset);
            Instantiate(_weaponList.weaponInfos[rand].PickupForm, position, Quaternion.identity);
        }
    }
}
