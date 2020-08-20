using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
