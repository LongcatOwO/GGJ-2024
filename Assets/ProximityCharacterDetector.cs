using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityCharacterDetector : MonoBehaviour
{
    public event Action OnCharacterDetectedInProximity;
    public event Action OnCharacterLeaveProximity;

    [Header("Component References")]
    [SerializeField] private Collider hostCollider;

    private void Awake()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), hostCollider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            OnCharacterDetectedInProximity?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            OnCharacterLeaveProximity?.Invoke();
        }
    }
}
