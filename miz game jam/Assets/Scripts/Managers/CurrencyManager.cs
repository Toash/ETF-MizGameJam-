using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager i;
    public static int Points;


    [Header("Configurable")] 
    public int StartingPoints = 50;

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

        Points = StartingPoints;
    }
}
