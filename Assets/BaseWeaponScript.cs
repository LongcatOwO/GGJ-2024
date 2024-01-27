using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponScript : MonoBehaviour
{
    // Start is called before the first frame update
    [field: SerializeField] public WeaponInfo Info { get; private set; }

    [Header("Component References")]
    [SerializeField] protected SlamAttackExecutor attackExecutor;
    [SerializeField] protected Collider hostCollider;
    [SerializeField] protected Collider weaponCollider;
    [Header("Hit Properties")]
    [SerializeField] protected float slamMagnitude;

    protected GameObject hitTargetGameObject;

}
