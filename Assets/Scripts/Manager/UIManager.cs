using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonMonoPersistent<UIManager>, IFlow
{
    private UIManager() { }

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
        //LoadScene(0);
    }

    #endregion

    #region private functions

    private void LoadUIElementsDefault()
    {
        TextMeshProUGUI[] textTodefault = GameObject.FindGameObjectsWithTag(Globals.onStartupDefault).Select(x => x.GetComponentInChildren<TextMeshProUGUI>()).ToArray();
        for (int i = 0; i < textTodefault.Length; ++i)
        {
            textTodefault[i].color = Color.grey;
        }
    }

    private void LogWarning(string msg) => Debug.LogWarning("[UI Manager] " + msg);

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod()
    {
        PageController.Instance.PreIntilizationMethod();
        LoadUIElementsDefault();
    }

    public void InitializationMethod() => PageController.Instance.InitializationMethod();

    public void UpdateMethod() { }

    #endregion
}
