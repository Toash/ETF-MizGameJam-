using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    public static DontDestroyOnLoad i;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (i == null)
        {
            i = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
