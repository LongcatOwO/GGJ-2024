using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SlammingWeapon : MonoBehaviour
{
    //This class handles the activation of the "slamming weapon" as well as its hit effects.

    [Header("Component References")]
    [SerializeField] private SlamAttackExecutor attackExecutor;
    [SerializeField] private Collider hostCollider;
    [SerializeField] private Collider weaponCollider;

    [Header("Hit Properties")]
    [SerializeField] private float slamMagnitude;

    private GameObject hitTargetGameObject;

    private void Start()
    {
        weaponCollider = GetComponent<Collider>();

        Physics.IgnoreCollision(hostCollider, weaponCollider);

        weaponCollider.enabled = false;
    }

    //Registers the events that will invoke the methods of this weapon.
    private void OnEnable()
    {
        attackExecutor.OnSlamMagnitudeExecuted += InitializeAttack;

        attackExecutor.OnSlamEnded += EndAttack;
    }
    
    //Unregisters the events that will invoke the methods of this weapon.
    private void OnDisable()
    {
        attackExecutor.OnSlamMagnitudeExecuted += InitializeAttack;

        attackExecutor.OnSlamEnded += EndAttack;
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

    //Initializes the weapon's component references.
    public void InitializeWeapon(GameObject hostGameObject)
    {
        hostCollider = hostGameObject.GetComponent<Collider>();

        attackExecutor = hostGameObject.GetComponentInChildren<SlamAttackExecutor>();
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
}
