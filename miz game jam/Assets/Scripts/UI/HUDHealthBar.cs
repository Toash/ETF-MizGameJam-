using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HUDHealthBar : MonoBehaviour
{
    private Slider _slider;
    private Health _playerHealth;
    private TMP_Text _text;


    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        _text = GetComponentInChildren<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _playerHealth.GetHealthPercentage();
        _text.text = _playerHealth.GetHealth.ToString("0") + " / " + _playerHealth.GetMaxHealth;
    }
}
