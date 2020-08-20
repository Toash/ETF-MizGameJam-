using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager i;

    public ParticleSystem BloodSplatFX;
    public ParticleSystem BloodHitFX;
    public ParticleSystem DashFX;
    public ParticleSystem BulletHitFX;


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


    public void PlayParticleAtPosition(Transform position, ParticleSystem particle)
    {
        ParticleSystem reference = Instantiate(particle, position.position, position.rotation);
        Destroy(reference,reference.main.duration);
    }

}
