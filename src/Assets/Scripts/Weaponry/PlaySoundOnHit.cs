using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlammableWeapon))]
public class PlaySoundOnHit : MonoBehaviour
{
    [SerializeField] private AudioSource _onHitSound;
    private SlammableWeapon _weapon;

    private void Awake()
    {
        _onHitSound = GetComponent<AudioSource>();
        _weapon = GetComponent<SlammableWeapon>();
    }

    private void OnEnable()
    {
        _weapon.OnSlamHit += PlaySound;
    }

    private void OnDisable()
    {
        _weapon.OnSlamHit -= PlaySound;
    }

    private void PlaySound(Collider collider)
    {
        _onHitSound.Play(0);
    }
}
