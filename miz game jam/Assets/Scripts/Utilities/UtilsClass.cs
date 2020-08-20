using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass
{

    public static Vector3 WorldMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static GameObject GetPlayer()
    {
        return GameObject.FindWithTag("Player");
    }
}
