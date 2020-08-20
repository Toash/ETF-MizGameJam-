using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Gun : MonoBehaviour
{
    [HideInInspector] public float _gunDamage;
    [HideInInspector] public float _criticalChance;
    [HideInInspector] public float _criticalMultiplier;
    [HideInInspector] public int _bulletsPerShot;
    [HideInInspector] public bool _invincibleBullet;
    [HideInInspector] public float _fireRate;
    [HideInInspector] public float _bulletSpeed;
    [HideInInspector] public GameObject _bullet;
    [HideInInspector] public bool _playerGun;
    [HideInInspector] public AudioClip _gunSound;




    public void LookAtTarget(Vector3 target)
    {
        var dir = target - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }

    public void Fire()
    {
        AudioManager.i.PlaySoundAtPosition(transform.position,_gunSound,.2f);

        switch (_bulletsPerShot)
        {
            case 1:
                CreateBullet(0);
                break;
            case 2:
                CreateBullet(0);
                CreateBullet(5);
                break;
            case 3:
                CreateBullet(-5);
                CreateBullet(0);
                CreateBullet(5);
                break;
            case 4:
                CreateBullet(-2);
                CreateBullet(-1);
                CreateBullet(1);
                CreateBullet(2);
                break;
            case 5:
                CreateBullet(-10);
                CreateBullet(-5);
                CreateBullet(0);
                CreateBullet(5);
                CreateBullet(10);
                break;
            case 6:
                CreateBullet(-10);
                CreateBullet(-5);
                CreateBullet(0);
                CreateBullet(5);
                CreateBullet(10);
                CreateBullet(15);
                break;
            case 7:
                CreateBullet(-15);
                CreateBullet(-10);
                CreateBullet(-5);
                CreateBullet(0);
                CreateBullet(5);
                CreateBullet(10);
                CreateBullet(15);
                break;
            case 8:
                CreateBullet(-15);
                CreateBullet(-10);
                CreateBullet(-5);
                CreateBullet(0);
                CreateBullet(5);
                CreateBullet(10);
                CreateBullet(15);
                CreateBullet(20);
                break;
            case 9:
                CreateBullet(-20);
                CreateBullet(-15);
                CreateBullet(-10);
                CreateBullet(-5);
                CreateBullet(0);
                CreateBullet(5);
                CreateBullet(10);
                CreateBullet(15);
                CreateBullet(20);
                break;
            case 10:
                CreateBullet(-20);
                CreateBullet(-15);
                CreateBullet(-10);
                CreateBullet(-5);
                CreateBullet(0);
                CreateBullet(5);
                CreateBullet(10);
                CreateBullet(15);
                CreateBullet(20);
                CreateBullet(25);
                break;
            default:
                CreateBullet(-20);
                CreateBullet(-15);
                CreateBullet(-10);
                CreateBullet(-5);
                CreateBullet(0);
                CreateBullet(5);
                CreateBullet(10);
                CreateBullet(15);
                CreateBullet(20);
                CreateBullet(25);
                break;
        }
    }

    private void CreateBullet(float rotationOffset)
    {
        GameObject bullet = Instantiate(_bullet, transform.position, transform.rotation * Quaternion.Euler(0,0,rotationOffset));
        Bullet bulletC = bullet.GetComponent<Bullet>();
        if (_playerGun)
        {
            bulletC._playerBullet = true;
        }
        else
        {
            bulletC._playerBullet = false;
        }

        bulletC._damage = _gunDamage;
        bulletC._speed = _bulletSpeed;
        bulletC._criticalChance = _criticalChance;
        bulletC._criticalMultiplier = _criticalMultiplier;
        bulletC._invincibleBullet = _invincibleBullet;
    }
}
