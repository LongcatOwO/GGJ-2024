using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "ScriptableObjects/WeaponInfo")]
public class WeaponInfo : ScriptableObject
{
    [field: SerializeField]
    public GameObject HeldForm { get; private set; }

    [field: SerializeField]
    public GameObject PickupForm { get; private set; }

    [field: SerializeField]
    public RuntimeAnimatorController AnimatorController { get; private set; }
}
