using UnityEngine;
using UnityEngine.UI;

public class MSButtonManager : SingletonMono<MSButtonManager>
{
    private Button[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        if (buttons.Length < 1)
        {
            LogWarning($"There is no buttons in {gameObject.name}");
            return;
        }

        for (int i = 0; i < buttons.Length; ++i)
        {
            switch (i)
            {
                case (int)MenuSelectionEnum.Start:
                    buttons[i].onClick.AddListener(UIManager.Instance.Startgame);
                    break;
                case (int)MenuSelectionEnum.Practice:
                    //TODO
                    break;
                case (int)MenuSelectionEnum.Replay:
                    //TODO
                    break;
                case (int)MenuSelectionEnum.Score:
                    buttons[i].onClick.AddListener(UIManager.Instance.ShowScores);
                    break;
                case (int)MenuSelectionEnum.Options:
                    buttons[i].onClick.AddListener(UIManager.Instance.ShowOptionsMenu);
                    break;
                case (int)MenuSelectionEnum.Quit:
                    buttons[i].onClick.AddListener(UIManager.Instance.ExitGame);
                    break;
                default:
                    throw new System.InvalidOperationException();
            }
        }
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Menu Selection Button Manager] : " + msg);
}
