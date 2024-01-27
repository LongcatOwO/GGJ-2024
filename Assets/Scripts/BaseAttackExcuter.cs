using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseAttackExcuter : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] public Animator animator;
    [SerializeField] public SlammingWeapon weapon;
    [SerializeField] public float minimumRequiredSlamProgress;
    [SerializeField] public float slamMagnitudeMultiplier;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
    virtual protected void ChargeWeapon()
    { 
    
    }
    virtual protected void SlamWeapon()
    {
        
    }

}
