using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInteractiveText : MonoBehaviour
{

    public bool Entered;

    private void Update()
    {
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Entered = true;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Entered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Entered = false;
            InteractiveText.i.ChangeText("");
        }
    }
}
