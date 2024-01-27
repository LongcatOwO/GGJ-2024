using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : ScriptableObject
{
    [SerializeField]
    public GameObject HeldForm { get; private set; }

    [SerializeField]
    public GameObject DroppedForm { get; private set; }
}
