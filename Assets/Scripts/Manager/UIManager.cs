using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonMono<UIManager>, IFlow
{
    /*********************ACTIONS**************************/

    public void StartGame() => SceneManager.LoadScene(1, LoadSceneMode.Single);

    public void StartPractice()
    {
        //TODO Create a tutorial version of the game
    }

    public void StartReplay()
    {
        //TODO I have no idea what this will be use for
    }

    public void ShowScore()
    {
        //TODO Scores are going to be displayed on screen overlaying the current menu while the background canvas will be blured out
    }

    public void ShowOptions()
    {
        //TODO Options are going to be displayed on screen overlaying the current menu while the background canvas will be blured out
    }

    public void Quit() => Application.Quit();


    /**********************FLOW****************************/

    public void InitializationMethod() { }

    public void PreIntilizationMethod() { }

    public void UpdateMethod() { }
}
