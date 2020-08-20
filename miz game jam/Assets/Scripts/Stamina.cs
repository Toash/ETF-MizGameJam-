using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public static Stamina instance;
    
    
    
    [Header("Configurable")]
    [SerializeField] private float _maxStamina = 100;
    public float _staminaRegen = 10f;
    public float _staminaDegen = 10f;
    
    
    public float _stamina;
    
    public float GetStamina => _stamina;
    public float GetMaxStamina => _maxStamina;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        _stamina = _maxStamina;
    }

    public void AddStamina(float amount)
    {
        _stamina = Math.Min(_maxStamina, _stamina += amount);
    }
}
