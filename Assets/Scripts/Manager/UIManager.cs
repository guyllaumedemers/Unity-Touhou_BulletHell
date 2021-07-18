using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonMono<UIManager>, IFlow
{
    GameObject OptionPanel;
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

    public void ShowOptions()
    {
        //TODO Options are going to be displayed on screen overlaying the current menu while the background canvas will be blured out
        OptionPanel.SetActive(true);
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            foreach (GameObject go in menuPanels) go.SetActive(false);
            return;
        }
        PauseGame();
    }

    public void HideOptions()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            GoBack();
            return;
        }
        OptionPanel.SetActive(false);
        ResumeGame();
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
        if (OptionPanel.activeSelf)
        {
            foreach (GameObject go in menuPanels) go.SetActive(true);
            OptionPanel.SetActive(false);
        }
    }

    private void PauseGame() => Time.timeScale = 0.0f;

    private void ResumeGame() => Time.timeScale = 1.0f;

    public void Quit() => Application.Quit();


    /**********************FLOW****************************/
    public void PreIntilizationMethod()
    {
        OptionPanel = GameObject.FindGameObjectWithTag(Globals.optionTag);
        OptionPanel.SetActive(false);
        menuPanels = GameObject.FindGameObjectsWithTag(Globals.mainTag);
    }

    public void InitializationMethod() { }

    public void UpdateMethod() { }
}
