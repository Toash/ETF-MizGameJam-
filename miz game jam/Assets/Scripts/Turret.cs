using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Gun _gun;
    private Stats _stats;
    private float _timer;
    private void Awake()
    {
        _gun = GetComponentInChildren<Gun>();
        _stats = GetComponent<Stats>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1 / _gun._fireRate)
        {
            _gun.Fire();
            _timer = 0;
        }
    }
}
