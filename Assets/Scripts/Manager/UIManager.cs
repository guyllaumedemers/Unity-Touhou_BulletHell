using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : SingletonMono<UIManager>, IFlow
{
    private UIManager() { }

    #region public functions

    public void Startgame() => LoadScene(1);            //TODO Should be loading the loading scene and then go to game scene -> also should wait for the blinking time to complete

    public void ExitGame() => Application.Quit();       //TODO Add a dialogue box so the player can config if he wants to exit the game

    public void ShowPauseMenu() => PageController.Instance.TurnPageOn(PageTypeEnum.PauseMenu);

    public void HidePauseMenu() => PageController.Instance.TurnPageOff(PageTypeEnum.PauseMenu);

    public void ShowOptionsMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) PageController.Instance.TurnPageOff(PageTypeEnum.Menu, PageTypeEnum.OptionMenu, true);
        else PageController.Instance.TurnPageOff(PageTypeEnum.PauseMenu, PageTypeEnum.OptionMenu, true);
    }

    public void HideOptionsMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) PageController.Instance.TurnPageOff(PageTypeEnum.OptionMenu, PageTypeEnum.Menu, true);
        else PageController.Instance.TurnPageOff(PageTypeEnum.OptionMenu, PageTypeEnum.PauseMenu, true);
    }

    public void ShowScores() => PageController.Instance.TurnPageOff(PageTypeEnum.Menu, PageTypeEnum.ScoreMenu);

    public void HideScores() => PageController.Instance.TurnPageOff(PageTypeEnum.ScoreMenu, PageTypeEnum.Menu);

    public void ShowKeyConfig()
    {
        //TODO
    }

    public void HideKeyConfig()
    {
        //TODO
    }

    public void ResetConfig()
    {
        //TODO
    }

    //HINT Fullscreen function and Windowed get passed the opposite button Text component so that upon click the other button text gets grey out
    public void FullScreen(TextMeshProUGUI text)
    {
        if (!text)
        {
            LogWarning("There is no text assigned to the Fullscreen Event");
            return;
        }
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        text.color = Color.grey;
    }

    public void Windowed(TextMeshProUGUI text)
    {
        if (!text)
        {
            LogWarning("There is no text assigned to the Fullscreen Event");
            return;
        }
        Screen.fullScreenMode = FullScreenMode.Windowed;
        text.color = Color.grey;
    }

    public void GoBackMainMenu()
    {
        //TODO Save Option - Pop Up Menu Save option
        LoadScene(0);
    }

    private void LoadScene(int index)
    {
        if (index < 0 || index > SceneManager.sceneCount)
        {
            LogWarning("Scene Index invalid");
            return;
        }
        SceneManager.LoadScene(index);
    }

    #endregion

    #region private functions

    private void LogWarning(string msg) => Debug.LogWarning("[UI Manager] " + msg);

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod() => PageController.Instance.PreIntilizationMethod();

    public void InitializationMethod() => PageController.Instance.InitializationMethod();

    public void UpdateMethod() => PageController.Instance.UpdateMethod();

    #endregion
}
