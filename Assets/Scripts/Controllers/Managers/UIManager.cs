using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonMonoPersistent<UIManager>
{
    #region public functions

    public void Startgame() => PageController.Instance.TurnPageOff(PageTypeEnum.Menu, PageTypeEnum.PlayerSelection, true);

    public void ExitGame() => Application.Quit();       //TODO Add a dialogue box so the player can config if he wants to exit the game

    public void ShowPauseMenu() => PageController.Instance.TurnPageOn(PageTypeEnum.PauseMenu);

    public void HidePauseMenu() => PageController.Instance.TurnPageOff(PageTypeEnum.PauseMenu);

    public void ShowOptionsMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneEnum.Menu) PageController.Instance.TurnPageOff(PageTypeEnum.Menu, PageTypeEnum.OptionMenu, true);
        else PageController.Instance.TurnPageOff(PageTypeEnum.PauseMenu, PageTypeEnum.OptionMenu, true);
    }

    public void HideOptionsMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneEnum.Menu) PageController.Instance.TurnPageOff(PageTypeEnum.OptionMenu, PageTypeEnum.Menu, true);
        else PageController.Instance.TurnPageOff(PageTypeEnum.OptionMenu, PageTypeEnum.PauseMenu, true);
    }

    public void ShowScores()
    {
        //TODO
    }

    public void HideScores()
    {
        //TODO
    }

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

    public void FullScreen() => Screen.fullScreenMode = FullScreenMode.FullScreenWindow;

    public void Windowed() => Screen.fullScreenMode = FullScreenMode.Windowed;

    public void GoBackMainMenu()
    {
        //TODO Save Option - Pop Up Menu Save option
        //LoadScene(0);
    }

    #endregion

    public void PreInitializeUIManager() => PageController.Instance.PreInitializePageController();
    public void InitializeUIManager() => PageController.Instance.InitializePageController();

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[UI Manager] " + msg);
    #endregion
}
