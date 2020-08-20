using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Configurable")] 
    [SerializeField] private int _defaultLayer;
    [SerializeField] private int _iFrameLayer;

    [SerializeField] private float _runMultiplier = 2f;
    [SerializeField] private float _moveSpeed = 12f;
    [SerializeField] private float _dashSpeed = 12f;
    [SerializeField] private float _dashCooldown = 1.5f;
    [SerializeField] private float _dashCost = 25f;
    [SerializeField] private float _healthRegen = 10f;



    private Rigidbody2D _rb;
    private Vector2 _moveVector;
    private Gun _gun;
    private TrailRenderer _trailRenderer;
    private Coroutine _dashCoroutine;
    private Stamina _staminaClass;
    private Health _health;
    private Stats _stats;

    private bool _dashing;
    private bool _sprinting;

    //timers
    private float _timer = 0;
    private float _dashTimer = 0;




    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gun = GetComponentInChildren<Gun>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
        _staminaClass = GetComponent<Stamina>();
        _health = GetComponent<Health>();
        _stats = GetComponent<Stats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _timer = Mathf.Infinity;
        _dashTimer = Mathf.Infinity;
    }

    // Update is called once per frame

    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            if (!_dashing)
            {
                _trailRenderer.enabled = _sprinting;
            }

            GetPlayerInput();
            IncreaseTimers();
            StaminaRegen();
            HealthRegen();
            _gun.LookAtTarget(UtilsClass.WorldMousePosition());
            if (Input.GetMouseButton(0) && _timer >= (1 / _gun._fireRate))
            {
                Shoot();
            }
            if (Input.GetKeyDown(KeyCode.Space) && _dashTimer >= (1 / _dashCooldown) && _dashCoroutine == null && _staminaClass._stamina > _dashCost)
            {
                Dash();
            }
            else if (!_dashing)
            {
                PlayerMovement();
                _rb.velocity = Vector2.zero;
            }
        }
    }


    private void StaminaRegen()
    {
        if (_sprinting)
        {
            _staminaClass._stamina = Mathf.Min(100, _staminaClass._stamina -= _staminaClass._staminaDegen * Time.deltaTime);
        }
        else
        {
            _staminaClass._stamina = Mathf.Min(100, _staminaClass._stamina += _staminaClass._staminaRegen * Time.deltaTime);
        }
    }

    private void HealthRegen()
    { 
        _health.GetHealth = Mathf.Min(100, _health.GetHealth += _healthRegen * Time.deltaTime);
    }
    
    private void Dash()
    {
        AudioManager.i.PlaySoundAtPosition(transform.position,AudioManager.i.DashSFX,.3f);
        ParticleManager.i.PlayParticleAtPosition(transform,ParticleManager.i.DashFX);
        _staminaClass._stamina = Mathf.Max(0, _staminaClass._stamina -= _dashCost);
        _dashCoroutine = StartCoroutine(IEDash());
        _dashTimer = 0;
    }

    private void Shoot()
    {
        _gun.Fire();
        _timer = 0;
    }

    private void IncreaseTimers()
    {
        _timer += Time.deltaTime;
        _dashTimer += Time.deltaTime;
    }

    private IEnumerator IEDash()
    {
        _dashing = true;
        gameObject.layer = _iFrameLayer;
        _trailRenderer.enabled = true;
        _rb.AddForce(_moveVector.normalized * _dashSpeed * 1000 * Time.fixedDeltaTime,ForceMode2D.Impulse);
        yield return new WaitForSeconds(.25f);
        _dashing = false;
        gameObject.layer = _defaultLayer;
        _trailRenderer.enabled = false;
        _dashCoroutine = null;
    }

    public void PlayerInvincible()
    {
        gameObject.layer = _iFrameLayer;
    }

    public void PlayerNotInvincible()
    {
        gameObject.layer = _defaultLayer;
    }



    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _staminaClass._stamina > 0)
        {
            _rb.MovePosition(_rb.position + (_moveVector.normalized) * _moveSpeed * _runMultiplier * Time.fixedDeltaTime);
            _sprinting = true;
        }
        else
        {
            _rb.MovePosition(_rb.position + (_moveVector.normalized) * _moveSpeed * Time.fixedDeltaTime);
            _sprinting = false;
        }
    }

    private void GetPlayerInput()
    {
        _moveVector.x = Input.GetAxisRaw("Horizontal");
        _moveVector.y = Input.GetAxisRaw("Vertical");
    }
}
