using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [HideInInspector]public float _damage;
    [HideInInspector]public float _speed;
    [HideInInspector]public bool _playerBullet;
    [HideInInspector] public bool _invincibleBullet;
    [HideInInspector] public float _criticalChance;
    [HideInInspector] public float _criticalMultiplier;

    private bool _isCritical;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _isCritical = Random.Range(0, 100) < _criticalChance;
        if (_isCritical)
        {
            _damage *= _criticalMultiplier;
        }
        Destroy(this.gameObject,10);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_playerBullet)
        {
            if (other.CompareTag("Enemy"))
            {
                if (_isCritical)
                {
                    other.GetComponent<Health>().TakeDamage(_damage);
                }
                else
                {
                    other.GetComponent<Health>().TakeDamage(_damage);
                }

                DamagePopup.Create(transform.position, _damage.ToString(),false,_isCritical);
                other.GetComponent<Health>().HitEffects();
                Destroy(this.gameObject);
            }
            else if(other.CompareTag("Hitable"))
            {
                ParticleManager.i.PlayParticleAtPosition(transform,ParticleManager.i.BulletHitFX);
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Health>().TakeDamage(_damage);
                //other.GetComponent<Health>().HitEffects();
                Destroy(this.gameObject);
            }
            else if(other.CompareTag("Hitable") || other.CompareTag("PlayerBullet") && !_invincibleBullet)
            {
                ParticleManager.i.PlayParticleAtPosition(transform,ParticleManager.i.BulletHitFX);
                Destroy(this.gameObject);
            }
        }
        
    }
}
