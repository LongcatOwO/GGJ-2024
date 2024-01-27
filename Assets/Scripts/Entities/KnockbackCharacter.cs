using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Can be put on anything that is meant to knockback a character such as a projectile.
[RequireComponent(typeof(Collider))]
public class KnockbackCharacter : MonoBehaviour
{
    [field: SerializeField]
    public float KnockbackDistance { get; set; }

    [field: SerializeField]
    public float KnockbackDuration { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<Character>(
                out Character character))
            return;

        Vector3 posDif = collision.transform.position - transform.position;
        character.ApplyKnockback(new Vector2(posDif.x, posDif.z).normalized,
                                 KnockbackDistance, KnockbackDuration);
        Destroy(gameObject);
    }
}
