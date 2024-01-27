using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedWeapon : MonoBehaviour
{
    [field: SerializeField]
    public WeaponInfo Info { get; private set; }
}
