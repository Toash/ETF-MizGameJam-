using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HUDStaminaBar : MonoBehaviour
{
    private Slider _slider;
    private Stamina _stamina;
    private TMP_Text _text;


    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _stamina = GameObject.FindWithTag("Player").GetComponent<Stamina>();
        _text = GetComponentInChildren<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _stamina.GetStamina / _stamina.GetMaxStamina;
        _text.text = _stamina.GetStamina.ToString("0") + " / " + _stamina.GetMaxStamina;
    }
}
