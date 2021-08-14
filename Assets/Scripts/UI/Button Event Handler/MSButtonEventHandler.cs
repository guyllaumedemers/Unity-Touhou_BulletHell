using UnityEngine;
using UnityEngine.UI;

public class MSButtonEventHandler : SingletonMono<MSButtonEventHandler>
{
    private Button[] buttons;
    private StaircaseDecorator animationInstance;

    private void Awake()
    {
        animationInstance = GetComponent<StaircaseDecorator>();
        buttons = GetComponentsInChildren<Button>();
        if (buttons.Length < 1)
        {
            LogWarning($"There is no child buttons in {gameObject.name}");
            return;
        }
        else if (!animationInstance)
        {
            LogWarning($"Staircase Decorator Script is missing in {gameObject.name}");
            return;
        }

        for (int i = 0; i < buttons.Length; ++i)
        {
            switch (i)
            {
                case (int)MenuSelectionEnum.Start:
                    buttons[i].onClick.AddListener(UIManager.Instance.Startgame);
                    buttons[i].onClick.AddListener(() => { animationInstance.PlayGraphicAnimation(); });
                    break;
                case (int)MenuSelectionEnum.Practice:
                    //TODO
                    buttons[i].onClick.AddListener(() => { animationInstance.PlayGraphicAnimation(); });
                    break;
                case (int)MenuSelectionEnum.Replay:
                    //TODO
                    buttons[i].onClick.AddListener(() => { animationInstance.PlayGraphicAnimation(); });
                    break;
                case (int)MenuSelectionEnum.Score:
                    buttons[i].onClick.AddListener(UIManager.Instance.ShowScores);
                    buttons[i].onClick.AddListener(() => { animationInstance.PlayGraphicAnimation(); });
                    break;
                case (int)MenuSelectionEnum.Options:
                    buttons[i].onClick.AddListener(UIManager.Instance.ShowOptionsMenu);
                    buttons[i].onClick.AddListener(() => { animationInstance.PlayGraphicAnimation(); });
                    break;
                case (int)MenuSelectionEnum.Quit:
                    buttons[i].onClick.AddListener(UIManager.Instance.ExitGame);
                    buttons[i].onClick.AddListener(() => { animationInstance.PlayGraphicAnimation(); });
                    break;
                default:
                    throw new System.InvalidOperationException();
            }
        }
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Menu Selection Button Manager] : " + msg);
}
