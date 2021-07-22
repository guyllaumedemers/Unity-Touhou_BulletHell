using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonMono<UIManager>, IFlow
{
    GameObject optionPanel;
    GameObject pausePanel;
    GameObject[] menuPanels;
    private UIManager() { }

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

    public void ShowPauseMenu()
    {
        //TODO Manage Menu Action
        PauseGame();
    }

    public void HidePauseMenu()
    {
        //TODO Manage Menu Action
        ResumeGame();
    }

    public void ShowOption()
    {
        optionPanel.SetActive(true);
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            foreach (GameObject go in menuPanels) go.SetActive(false);
            return;
        }
    }

    public void HideOption()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            GoBack();
            return;
        }
        optionPanel.SetActive(false);
    }

    public void GoMainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            GoBack();
            return;
        }
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void GoBack()
    {
        //TODO Only handle the GoBack from the option menu for now
        if (optionPanel.activeSelf)
        {
            foreach (GameObject go in menuPanels) go.SetActive(true);
            optionPanel.SetActive(false);
        }
    }

    private void PauseGame() => Time.timeScale = 0.0f;

    private void ResumeGame() => Time.timeScale = 1.0f;

    public void Quit() => Application.Quit();


    /**********************FLOW****************************/
    public void PreIntilizationMethod()
    {
        optionPanel = GameObject.FindGameObjectWithTag(Globals.optionTag);
        optionPanel.SetActive(false);
        menuPanels = GameObject.FindGameObjectsWithTag(Globals.mainTag);
    }

    public void InitializationMethod() { }

    public void UpdateMethod() { }
}
