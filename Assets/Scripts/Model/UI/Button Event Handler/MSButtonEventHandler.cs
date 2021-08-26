using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MSButtonEventHandler : SingletonMono<MSButtonEventHandler>
{
    private Button[] buttons;
    private RectTransform[] rects;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();

        if (buttons.Length < 1)
        {
            LogWarning($"There is no child buttons in {gameObject.name}");
            return;
        }
        rects = buttons.Select(x => x.GetComponent<RectTransform>()).ToArray();
        if (rects.Length < 1)
        {
            LogWarning($"There is no rect in {gameObject.name}");
            return;
        }

        for (int i = 0; i < buttons.Length; ++i)
        {
            switch (i)
            {
                case (int)MenuSelectionEnum.Start:
                    buttons[i].onClick.AddListener(() =>
                    {
                        UIManager.Instance.Startgame();
                        StaircaseAnimation();
                        DisableAllbuttons();
                        AudioManager.Instance.TriggerButtonClickSFX();
                    });
                    break;
                case (int)MenuSelectionEnum.Practice:
                    //TODO
                    buttons[i].onClick.AddListener(() =>
                    {
                        StaircaseAnimation();
                        DisableAllbuttons();
                        AudioManager.Instance.TriggerButtonClickSFX();
                    });
                    break;
                case (int)MenuSelectionEnum.Replay:
                    //TODO
                    buttons[i].onClick.AddListener(() =>
                    {
                        StaircaseAnimation();
                        DisableAllbuttons();
                        AudioManager.Instance.TriggerButtonClickSFX();
                    });
                    break;
                case (int)MenuSelectionEnum.Score:
                    buttons[i].onClick.AddListener(() =>
                    {
                        UIManager.Instance.ShowScores();
                        StaircaseAnimation();
                        DisableAllbuttons();
                        AudioManager.Instance.TriggerButtonClickSFX();
                    });
                    break;
                case (int)MenuSelectionEnum.Options:
                    buttons[i].onClick.AddListener(() =>
                    {
                        UIManager.Instance.ShowOptionsMenu();
                        StaircaseAnimation();
                        DisableAllbuttons();
                        AudioManager.Instance.TriggerButtonClickSFX();
                    });
                    break;
                case (int)MenuSelectionEnum.Quit:
                    buttons[i].onClick.AddListener(() =>
                    {
                        UIManager.Instance.ExitGame();
                        StaircaseAnimation();
                        DisableAllbuttons();
                        AudioManager.Instance.TriggerButtonClickSFX();
                    });
                    break;
                default:
                    throw new System.InvalidOperationException();
            }
        }
    }

    private void OnEnable()
    {
        if (buttons.Length < 1)
        {
            return;
        }
        foreach (var item in rects) item.anchoredPosition = new Vector2(0.0f, item.anchoredPosition.y);
        foreach (var item in buttons) item.interactable = true;
    }

    private void StaircaseAnimation()
    {
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name.Equals("StaircaseAnimation")).FirstOrDefault().Name);
        StartCoroutine(CustomDotTween.StaircaseAnimation(rects, Globals.staircaseTime, delegate (RectTransform r, float time)
        {
            StartCoroutine(CustomDotTween.SlideAnimation(r, r.anchoredPosition.x - Globals.sliding_offset, time));
        }));
    }

    private void DisableAllbuttons()
    {
        if (buttons.Length < 1)
        {
            LogWarning("There is no button component here " + gameObject.name);
            return;
        }
        foreach (var item in buttons) item.interactable = false;
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Menu Selection Button Manager] : " + msg);
}
