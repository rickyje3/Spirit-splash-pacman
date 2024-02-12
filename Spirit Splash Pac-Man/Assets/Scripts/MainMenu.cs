using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource buttonSound;
    bool pressed;
    bool exit;
    float pressedTimer;
    float pressedTime = 0.5f;

    int mainMenuScene = 0;
    int level1Scene = 1;
    int level2Scene = 2;
    int creditsScene = 3;

    int selectedLevel;

    private void Update()
    {
        if(pressed == true)
        {
            pressedTimer -= Time.deltaTime;
            if ((pressedTimer < 0) && (exit == true))
            {
                Application.Quit();
                print("Exit Success");
            }
            else if(pressedTimer < 0)
            {
                SceneManager.LoadSceneAsync(selectedLevel);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            print("Exit Success");
        }
    }
    public void StartGame()
    {
        selectedLevel = level1Scene;
        if (pressed != true)
        {
            buttonSound.Play(); 
            pressedTimer = pressedTime;
            pressed = true;
        }
    }

    public void Credits()
    {
        selectedLevel = creditsScene;
        if (pressed != true)
        {
            buttonSound.Play(); 
            pressedTimer = pressedTime;
            pressed = true;
        }
    }

    public void ExitGame()
    {
        if (pressed != true)
        {
            buttonSound.Play(); 
            exit = true;
            pressed = true;
        }
    }

    public void MainMenuButton()
    {
        selectedLevel = mainMenuScene;
        if (pressed != true)
        {
            buttonSound.Play(); 
            pressedTimer = pressedTime;
            pressed = true;
        }
    }

}