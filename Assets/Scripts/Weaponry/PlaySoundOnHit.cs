using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SlammableWeapon))]
public class PlaySoundOnHit : MonoBehaviour
{
    private AudioSource _audio;
    private SlammableWeapon _weapon;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
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
        _audio.Play(0);
    }
}
