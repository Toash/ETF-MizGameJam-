using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Configurable")] 
    public GameObject PauseMenuGameObject;
    
    
    public static bool IsPaused;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
        }

        if (IsPaused)
        {
            PauseMenuGameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PauseMenuGameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
        IsPaused = false;
    }
    
    public void AdjustVolume (float newVolume) {
        AudioListener.volume = newVolume;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
