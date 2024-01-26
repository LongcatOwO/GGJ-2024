using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SlammingWeapon : MonoBehaviour
{
    [SerializeField] private AttackExecutor attackExecutor;
    [SerializeField] private Collider hostCollider;

    [SerializeField] private Collider weaponCollider;

    [SerializeField] private float slamMagnitude;

    private SlammableTarget hitTarget;

    private void Start()
    {
        weaponCollider = GetComponent<Collider>();

        Physics.IgnoreCollision(hostCollider, weaponCollider);

        weaponCollider.enabled = false;
    }

    private void OnEnable()
    {
        attackExecutor.OnSlamMagnitudeExecuted += InitializeAttack;

        attackExecutor.OnSlamEnded += EndAttack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SlammableTarget target))
        {
            if(hitTarget == target)
            {
                return;
            }

            hitTarget = target;

            hitTarget.transform.position += Vector3.down * slamMagnitude;
        }
    }

    public void InitializeAttack(float slamMagnitude)
    {
        this.slamMagnitude = slamMagnitude;

        weaponCollider.enabled = true;
    }

    public void EndAttack()
    {
        weaponCollider.enabled = false;

        hitTarget = null;
    }
}
