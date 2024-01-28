using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponList", menuName = "ScriptableObjects/WeaponList")]
public class WeaponList : ScriptableObject
{
    [field: SerializeField]
    public WeaponInfo[] weaponInfos { get; private set; }
}
