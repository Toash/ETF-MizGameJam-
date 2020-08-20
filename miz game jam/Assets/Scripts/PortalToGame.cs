using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToGame : MonoBehaviour
{

    private void Update()
    {
        if (GetComponent<ChangeInteractiveText>().Entered)
        {
            InteractiveText.i.ChangeText("press e to go to game");
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }
}
