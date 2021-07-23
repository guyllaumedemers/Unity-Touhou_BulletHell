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

    #region UI Functions

    public void StartGame() => LoadScene(1);

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
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            foreach (GameObject go in menuPanels) go.SetActive(false);
            return;
        }
        pausePanel.SetActive(false);
    }

    public void HideOption()
    {
        //TODO Serialize value change
        optionPanel.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GoBack();
            return;
        }
        pausePanel.SetActive(true);
    }

    public void GoMainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GoBack();
            return;
        }
        LoadScene(0);
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

    private void LoadScene(int index)
    {
        AudioManager.Instance.OnSceneLoading();
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    private void PauseGame() => Time.timeScale = 0.0f;

    private void ResumeGame() => Time.timeScale = 1.0f;

    public void Quit() => Application.Quit();

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod()
    {
        RetrieveAllTags();
        DisableAllUIExceptMainMenu();
    }

    public void InitializationMethod() { }

    public void UpdateMethod() { }

    #endregion

    #region UI Initialization and Management

    private void DisableAllUIExceptMainMenu()
    {
        optionPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void RetrieveAllTags()
    {
        optionPanel = GameObject.FindGameObjectWithTag(Globals.optionMenuTag);
        pausePanel = GameObject.FindGameObjectWithTag(Globals.pauseMenuTag);
        menuPanels = GameObject.FindGameObjectsWithTag(Globals.mainMenuTag);
    }

    #endregion
}
