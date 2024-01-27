using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SlammableWeapon : MonoBehaviour
{
    //This class handles the activation of the "slamming weapon" as well as its hit effects.

    public WeaponInfo Info { get { return info; } }

    [Header("Weapon Reference")]
    [SerializeField] private WeaponInfo info;

    [Header("Component References")]
    [SerializeField] private AttackAnimationEvents attackEvents;
    [SerializeField] private Collider hostCollider;
    [SerializeField] private Collider weaponCollider;

    [Header("Hit Properties")]
    [SerializeField] private float buryPotential;

    private GameObject hitTargetGameObject;
    private float slamForce;

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

            hitTargetGameObject.transform.position += Vector3.down * slamForce * buryPotential;
        }
    }

    private void SubscribeAttackExecutorEvents()
    {
        if (attackEvents == null) return;

        attackEvents.OnAttackExecuted += InitializeAttack;

        attackEvents.OnAttackEnded += EndAttack;
    }

    private void UnsubscribeAttackExecutorEvents()
    {
        if (attackEvents == null) return;

        attackEvents.OnAttackExecuted -= InitializeAttack;

        attackEvents.OnAttackEnded -= EndAttack;
    }

    //Initializes the weapon's component references.
    public void InitializeWeapon(GameObject hostGameObject)
    {
        hostCollider = hostGameObject.GetComponent<Collider>();

        attackEvents = hostGameObject.GetComponentInChildren<AttackAnimationEvents>();

        SubscribeAttackExecutorEvents();
    }

    //Readies the weapon for attack. Enables the weapon's collider to allow "OnTriggerEnter()" calls.
    public void InitializeAttack(float slamForce)
    {
        this.slamForce = slamForce;

        weaponCollider.enabled = true;
    }

    //Disables the weapon's attack functionality by disabling the weapon's collider.
    public void EndAttack()
    {
        weaponCollider.enabled = false;

        hitTargetGameObject = null;
    }
}
