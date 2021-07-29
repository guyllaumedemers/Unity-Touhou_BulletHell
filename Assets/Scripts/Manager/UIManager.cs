using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonMono<UIManager>, IFlow
{
    private UIManager() { }

    #region UI Functions

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

    public void FullScreen()
    {
        //TODO
    }

    public void Windowed()
    {
        //TODO
    }

    public void GoBackMainMenu() => LoadScene(0);       // maybe think about saving of having periodic save

    private void LoadScene(int index) => SceneManager.LoadScene(index);

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod() => PageController.Instance.PreIntilizationMethod();

    public void InitializationMethod() => PageController.Instance.InitializationMethod();

    public void UpdateMethod() => PageController.Instance.UpdateMethod();

    #endregion
}
