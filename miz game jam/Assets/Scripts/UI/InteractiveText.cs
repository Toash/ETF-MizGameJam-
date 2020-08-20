using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractiveText : MonoBehaviour
{
    public static InteractiveText i;

    private TMP_Text _text;

    private void Awake()
    {
        if (i == null)
        {
            i = this;
        }
        else
        {
            Destroy(this);
        }

        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _text.text = "";
    }
    

    public void ChangeText(string text)
    {
        _text.text = text;
    }
}
