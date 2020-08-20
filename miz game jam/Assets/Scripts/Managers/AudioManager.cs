using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{

    public static AudioManager i;

    public AudioClip PlayerHitSFX;
    public AudioClip HitSFX;
    public AudioClip DashSFX;
    public AudioClip CoinSFX;
    public AudioClip PowerupSFX;
    public AudioClip SplatSFX;

    private void Awake()
    {
        if (i == null)
        {
            i = this;
        }
        else
        {
            Destroy(this);
        }
    }



    public void PlaySoundAtPosition(Vector3 position, AudioClip sound, float volume = 1.0f)
    {
        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = sound;
        soundObject.transform.position = position;
        audioSource.PlayOneShot(audioSource.clip,volume);
        Destroy(soundObject,audioSource.clip.length);
    }
}
