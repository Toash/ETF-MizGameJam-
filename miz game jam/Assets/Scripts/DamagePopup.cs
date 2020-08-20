using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 position, string damage, bool gold = false,bool crit = false,bool pickup = false)
    {
        var reference = Instantiate(GameAssets.instance.DamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = reference.GetComponent<DamagePopup>();
        damagePopup.Setup(damage,gold,crit,pickup);
        return damagePopup;
    }

    private static int _sortingOrder;
    
    private const float DISAPPEAR_TIMER_MAX = .5f;
    
    private TextMeshPro _textMeshPro;
    private float _disappearTimer = 0;
    private Color _textColor;
    private Vector3 moveVector;
    
    private void Awake()
    {
        _textMeshPro = transform.GetComponent<TextMeshPro>();
    }

    private void Setup(string damage, bool gold,bool crit,bool pickup)
    {
        _textMeshPro.SetText(damage);
        if (!crit && !gold && !pickup)
        {
            _textMeshPro.fontSize = 4;
            _textColor = Color.white;
        }
        else if (gold)
        {
            _textMeshPro.fontSize = 3;
            _textColor = Color.yellow;
        }
        else if(crit)
        {
            _textMeshPro.fontSize = 6;
            _textColor = Color.red;
        }
        else if(pickup)
        {
            _textMeshPro.fontSize = 2;
            _textColor = Color.white;
        }

        _textMeshPro.color = _textColor;
        _disappearTimer = DISAPPEAR_TIMER_MAX;

        _sortingOrder++;
        _textMeshPro.sortingOrder = _sortingOrder;
        
        moveVector = new Vector3(Random.Range(-.7f,.7f),1) * 6f;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (_disappearTimer > DISAPPEAR_TIMER_MAX / 2)
        {
            //first half of popup
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            //second half
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        _disappearTimer -= Time.deltaTime;
        if (_disappearTimer < 0)
        {
            float disapeearSpeed = 3f;
            _textColor.a -= disapeearSpeed * Time.deltaTime;
            _textMeshPro.color = _textColor;
            if (_textColor.a < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
