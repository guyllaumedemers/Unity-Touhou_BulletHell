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
                    buttons[i].onClick.AddListener(() => { GetComponent<StaircaseDecorator>()?.PlayGraphicAnimation(); });
                    break;
                case (int)MenuSelectionEnum.Practice:
                    //TODO
                    buttons[i].onClick.AddListener(() => { GetComponent<StaircaseDecorator>()?.PlayGraphicAnimation(); });
                    break;
                case (int)MenuSelectionEnum.Replay:
                    //TODO
                    buttons[i].onClick.AddListener(() => { GetComponent<StaircaseDecorator>()?.PlayGraphicAnimation(); });
                    break;
                case (int)MenuSelectionEnum.Score:
                    buttons[i].onClick.AddListener(UIManager.Instance.ShowScores);
                    buttons[i].onClick.AddListener(() => { GetComponent<StaircaseDecorator>()?.PlayGraphicAnimation(); });
                    break;
                case (int)MenuSelectionEnum.Options:
                    buttons[i].onClick.AddListener(UIManager.Instance.ShowOptionsMenu);
                    buttons[i].onClick.AddListener(() => { GetComponent<StaircaseDecorator>()?.PlayGraphicAnimation(); });
                    break;
                case (int)MenuSelectionEnum.Quit:
                    buttons[i].onClick.AddListener(UIManager.Instance.ExitGame);
                    buttons[i].onClick.AddListener(() => { GetComponent<StaircaseDecorator>()?.PlayGraphicAnimation(); });
                    break;
                default:
                    throw new System.InvalidOperationException();
            }
        }
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Menu Selection Button Manager] : " + msg);
}
