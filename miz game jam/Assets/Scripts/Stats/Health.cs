using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Configurable")]
    public bool IsPlayer = false;
    public int StartingHealth = 100;

    private float _health;
    private float _maxHealth;
    private SpriteRenderer _renderer;
    private Color _initialColor;

    private Coroutine _playerCoroutine;
    

    public float GetHealth
    {
        get { return _health; }
        set { _health = value; }
    }
    public float GetMaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    private void Awake()
    {
        _health = StartingHealth;
        _maxHealth = StartingHealth;
        _renderer = GetComponentInChildren<SpriteRenderer>();

    }
    
    void Start()
    {
        if (_renderer != null)
        {
            _initialColor = _renderer.color;
        }
    }

    public void HitEffects()
    {
        if (IsPlayer)
        {
            AudioManager.i.PlaySoundAtPosition(transform.position,AudioManager.i.PlayerHitSFX,.25f);
            ParticleManager.i.PlayParticleAtPosition(transform,ParticleManager.i.BloodHitFX);
        }
        else
        {
            AudioManager.i.PlaySoundAtPosition(transform.position,AudioManager.i.HitSFX,.1f);
            ParticleManager.i.PlayParticleAtPosition(transform,ParticleManager.i.BloodHitFX);
        }

    }

    private IEnumerator FlashRenderer()
    {
        _renderer.color = Color.white;
        yield return new WaitForSeconds(.1f);
        _renderer.color = _initialColor;
    }
    
    public void TakeDamage(float damage)
    {
        if (IsPlayer && _playerCoroutine == null)
        {
            _playerCoroutine = StartCoroutine(PlayerTakeDamage(damage));
        }
        else if(!IsPlayer)
        {
            EnemyTakeDamage(damage);
            HitEffects();
        }
        
        if (_health <= 0)
        {
            Die();
        }
    }

    private void EnemyTakeDamage(float damage)
    {
        _health = Mathf.Max(0, _health -= damage);
        if (_renderer != null)
        {
            StartCoroutine(FlashRenderer());
        }
    }

    public IEnumerator PlayerTakeDamage(float damage)
    {
        HitEffects();
        GetComponent<PlayerController>().PlayerInvincible();
        _health = Mathf.Max(0, _health -= damage);
        _renderer.color = Color.red;
        yield return new WaitForSeconds(.25f);
        GetComponent<PlayerController>().PlayerNotInvincible();
        _renderer.color = _initialColor;
        _playerCoroutine = null;
    }

    public void Die()
    {
        if (IsPlayer)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            AudioManager.i.PlaySoundAtPosition(transform.position,AudioManager.i.SplatSFX,.3f);
            ParticleManager.i.PlayParticleAtPosition(this.transform,ParticleManager.i.BloodSplatFX);
            Gold.Create(transform.position, GetComponent<EnemyController>().GoldOnDeath);
            Destroy(this.gameObject);
        }
    }

    public float GetHealthPercentage()
    {
        return _health / _maxHealth;
    }
}
