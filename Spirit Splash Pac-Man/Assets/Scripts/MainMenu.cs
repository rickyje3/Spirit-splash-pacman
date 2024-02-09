using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Credits()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadSceneAsync(0);
    }
}