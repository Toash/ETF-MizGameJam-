using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Configurable")] 
    public bool IsPlayer = false;

    public int BulletsPerShot;
    public bool InvincibleBullets;
    public float Damage;
    public float CriticalChance;
    public float CriticalMultiplier;
    public float AttackSpeed;
    public float BulletSpeed;
    public GameObject Bullet;

    [Header("Audio")] 
    public AudioClip AttackSound;

    private Gun _gun;

    private void Awake()
    {
        _gun = GetComponentInChildren<Gun>();
    }

    private void Update()
    {
        _gun._playerGun = IsPlayer;
        _gun._bulletsPerShot = BulletsPerShot;
        _gun._invincibleBullet = InvincibleBullets;
        _gun._gunDamage = Damage;
        _gun._criticalChance = CriticalChance;
        _gun._criticalMultiplier = CriticalMultiplier;
        _gun._fireRate = AttackSpeed;
        _gun._bulletSpeed = BulletSpeed;
        _gun._bullet = Bullet;
        _gun._gunSound = AttackSound;
    }
}
