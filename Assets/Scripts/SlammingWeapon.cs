using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SlammingWeapon : BaseWeaponScript
{
    //This class handles the activation of the "slamming weapon" as well as its hit effects.

    //[field: SerializeField] public WeaponInfo Info { get; private set; }

    //[Header("Component References")]
    //[SerializeField] private SlamAttackExecutor attackExecutor;
    //[SerializeField] private Collider hostCollider;
    //[SerializeField] private Collider weaponCollider;

    //[Header("Hit Properties")]
    //[SerializeField] private float slamMagnitude;

    //private GameObject hitTargetGameObject;

    private void Start()
    {
        weaponCollider = GetComponent<Collider>();

        Physics.IgnoreCollision(hostCollider, weaponCollider);

        weaponCollider.enabled = false;
    }

    //Registers the events that will invoke the methods of this weapon.
    private void OnEnable()
    {
        SubscribeAttackExecutorEvents();
    }
    
    //Unregisters the events that will invoke the methods of this weapon.
    private void OnDisable()
    {
        UnsubscribeAttackExecutorEvents();
    }

    //If a viable target enters the weapon's collider, trigger the hit effect.
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null || other.GetComponent<SlammableTarget>() != null)
        {
            if(hitTargetGameObject == other.gameObject)
            {
                return;
            }

            hitTargetGameObject = other.gameObject;

            hitTargetGameObject.transform.position += Vector3.down * slamMagnitude;
        }
    }

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

    //Initializes the weapon's component references.
    public void InitializeWeapon(GameObject hostGameObject)
    {
        hostCollider = hostGameObject.GetComponent<Collider>();
        attackExecutor = hostGameObject.GetComponentInChildren<SlamAttackExecutor>();
        SubscribeAttackExecutorEvents();
    }

    //Readies the weapon for attack. Enables the weapon's collider to allow "OnTriggerEnter()" calls.
    public void InitializeAttack(float slamMagnitude)
    {
        this.slamMagnitude = slamMagnitude;

        weaponCollider.enabled = true;
    }

    //Disables the weapon's attack functionality by disabling the weapon's collider.
    public void EndAttack()
    {
        weaponCollider.enabled = false;

        hitTargetGameObject = null;
    }
    public void OnTriggerExit(Collider other)
    {
        Debug.LogError("This should happen");
    }
}
