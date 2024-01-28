using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    //This class defines the host game object as a character and as a result, can be "slammed".

    [Header("Component References")]
    [SerializeField] private Mover mover;
    [SerializeField] private AttackAnimationEvents attackEvents;
    [SerializeField] private Transform weaponSlot;
    [SerializeField] private Animator animator;

    [SerializeField] private SlammableWeapon wieldedMeleeWeapon;

    // @param direction[Vector2]
    // @param distance[float]
    // @param duration[float]
    private event Action<Vector2, float, float> OnCharacterKnockback;

    private Coroutine knockbackCoroutine;

    private void OnEnable()
    {
        attackEvents.OnAttackInitiated += LockMovement;

        attackEvents.OnAttackEnded += UnlockMovement;

        attackEvents.OnAttackCancelled += UnlockMovement;
    }

    private void OnDisable()
    {
        attackEvents.OnAttackInitiated -= LockMovement;

        attackEvents.OnAttackEnded -= UnlockMovement;

        attackEvents.OnAttackCancelled -= UnlockMovement;
    }

    public void PickupWeapon(SlammableWeapon weapon)
    {
        if(wieldedMeleeWeapon != null)
        {
            Instantiate(weapon.Info.PickupForm, transform.position, Quaternion.identity);

            Destroy(wieldedMeleeWeapon.gameObject);
        }

        wieldedMeleeWeapon = weapon;

        wieldedMeleeWeapon.transform.SetParent(weaponSlot, false);

        animator.runtimeAnimatorController = weapon.Info.AnimatorController;
    }

    public void LockMovement()
    {
        mover.SetMovementLock(true);
    }

    public void UnlockMovement()
    {
        mover.SetMovementLock(false);
    }

    public void ApplyKnockback(Vector2 direction, float distance, float duration)
    {
        if (knockbackCoroutine != null)
        {
            StopCoroutine(knockbackCoroutine);
        }

        knockbackCoroutine = StartCoroutine(ProcessKnockback(direction, distance, duration));
        OnCharacterKnockback?.Invoke(direction, distance, duration);
    }

    private IEnumerator ProcessKnockback(Vector2 direction, float distance, float duration)
    {
        const float lerpFactor = 1.0f;

        Vector3 targetPosition = transform.position + new Vector3(direction.x, 0, direction.y) * distance;

        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            Vector3.Lerp(transform.position, targetPosition, lerpFactor * Time.deltaTime);
            yield return null;
            timeElapsed += Time.deltaTime;
        }
    }
}
