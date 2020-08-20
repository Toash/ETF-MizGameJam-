using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _goldOnDeath = 10;


    private Transform _playerPos;
    private Gun _gun;
    private float timer = 0;
    private Stats _stats;
    private Rigidbody2D _rb;
    private bool _movementEnabled = true;


    public int GoldOnDeath => _goldOnDeath;

    private void Awake()
    {
        _playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _gun = GetComponentInChildren<Gun>();
        _stats = GetComponent<Stats>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            IncreaseTimers();
            _gun.LookAtTarget(_playerPos.position);
            if (Vector2.Distance(_playerPos.position, transform.position) < _attackRange / 1.5f && _movementEnabled)
            {
                Flee();
            }
            else if (Vector2.Distance(_playerPos.position, transform.position) > _attackRange && _movementEnabled)
            {
                MoveTowardsPlayer();
            }
            else if (timer >= 1 / _stats.AttackSpeed)
            {
                Attack();
            }
        }
    }

    private void IncreaseTimers()
    {
        timer += Time.deltaTime;
    }

    private void Attack()
    {
        _gun.Fire();
        timer = 0;
    }

    void MoveTowardsPlayer()
    {
        Vector2 dir = (_playerPos.position - transform.position).normalized;
        _rb.MovePosition(_rb.position + dir * _moveSpeed * Time.fixedDeltaTime);
    }

    private void Flee()
    {
        Vector2 dir = (transform.position - _playerPos.position).normalized;
        _rb.MovePosition(_rb.position + dir * _moveSpeed * Time.fixedDeltaTime);
    }

    private void OnDestroy()
    {
        SpawnManager.i.EnemyCount--;
    }
}
