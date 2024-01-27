using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeaponScript : MonoBehaviour
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
    private void Start()
    {
        weaponCollider = GetComponent<Collider>();

        Physics.IgnoreCollision(hostCollider, weaponCollider);

        weaponCollider.enabled = false;
        WeaponStart();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null || other.GetComponent<SlammableTarget>() != null)
        {
            if (hitTargetGameObject == other.gameObject)
            {
                return;
            }

        }
        OnWeaponHit(other);
    }
    //Registers the events that will invoke the methods of this weapon.
    private void OnEnable()
    {
        SubscribeAttackExecutorEvents();
    }
    abstract protected void WeaponStart();
    //Unregisters the events that will invoke the methods of this weapon.
    private void OnDisable()
    {
        UnsubscribeAttackExecutorEvents();
    }
    abstract protected void OnWeaponHit(Collider other);
    abstract protected void InitializeAttack(float slamMagnitude);

    //Disables the weapon's attack functionality by disabling the weapon's collider.
    abstract protected void EndAttack();
    private void SubscribeAttackExecutorEvents()
    {
        if (attackExecutor == null) return;
        attackExecutor.OnSlamMagnitudeExecuted += InitializeAttack;
        attackExecutor.OnSlamEnded += EndAttack;
    }

    private void UnsubscribeAttackExecutorEvents()
    {
        if (attackExecutor == null) return;
        attackExecutor.OnSlamMagnitudeExecuted -= InitializeAttack;
        attackExecutor.OnSlamEnded -= EndAttack;
    }
}
