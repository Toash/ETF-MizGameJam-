using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldHealthBar : MonoBehaviour
{

    private Health _health;
    private Slider _slider;

    private void Awake()
    {
        _health = GetComponentInParent<Health>();
        _slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _health.GetHealthPercentage();
    }
}
