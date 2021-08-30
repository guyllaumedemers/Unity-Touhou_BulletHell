using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OSButtonEventHandler : SingletonMono<OSButtonEventHandler>
{
    private Button[] buttons;
    private TextMeshProUGUI[] texts;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>(true);
        texts = GameObject.FindGameObjectsWithTag(Globals.toggleMenuComponents).Select(x => x.GetComponent<TextMeshProUGUI>()).ToArray();

        if (buttons.Length < 1 || texts.Length < 4)
        {
            LogWarning($"There is no buttons in {gameObject.name} OR there is missing a toggled text components in the scene");
            return;
        }
    }

    private void Start() => RegisterButtonEvents();

    private void RegisterButtonEvents()
    {
        for (int i = 0; i < buttons.Length; ++i)
        {
            switch (i)
            {
                case (int)OptionSelectionEnum.ST_DOWN:
                    buttons[i].onClick.AddListener(() =>
                    {
                        AudioManager.Instance.DecrementMainVolume();
                    });
                    break;
                case (int)OptionSelectionEnum.ST_UP:
                    buttons[i].onClick.AddListener(() =>
                    {
                        AudioManager.Instance.IncrementMainVolume();
                    });
                    break;
                case (int)OptionSelectionEnum.SE_DOWN:
                    buttons[i].onClick.AddListener(() =>
                    {
                        AudioManager.Instance.DecrementSFXVolume();
                    });
                    break;
                case (int)OptionSelectionEnum.SE_UP:
                    buttons[i].onClick.AddListener(() =>
                    {
                        AudioManager.Instance.IncrementSFXVolume();
                    });
                    break;
                case (int)OptionSelectionEnum.Fullscreen:
                    buttons[i].onClick.AddListener(() =>
                    {
                        UIManager.Instance.FullScreen();
                        CustomDotTween.ToggleTextColor(texts[0], texts[1]);
                    });
                    break;
                case (int)OptionSelectionEnum.Windowed:
                    buttons[i].onClick.AddListener(() =>
                    {
                        UIManager.Instance.Windowed();
                        CustomDotTween.ToggleTextColor(texts[0], texts[1]);
                    });
                    break;
                case (int)OptionSelectionEnum.Mute_OFF:
                    buttons[i].onClick.AddListener(() =>
                    {
                        AudioManager.Instance.EnableMixer();
                        CustomDotTween.ToggleTextColor(texts[2], texts[3]);
                    });
                    break;
                case (int)OptionSelectionEnum.Mute_ON:
                    buttons[i].onClick.AddListener(() =>
                    {
                        AudioManager.Instance.DisableMixer();
                        CustomDotTween.ToggleTextColor(texts[2], texts[3]);
                    });
                    break;
                case (int)OptionSelectionEnum.Reset:
                    buttons[i].onClick.AddListener(() =>
                    {
                        UIManager.Instance.ResetConfig();
                    });
                    break;
                case (int)OptionSelectionEnum.KeyConfig:
                    buttons[i].onClick.AddListener(() =>
                    {
                        UIManager.Instance.ShowKeyConfig();
                    });
                    break;
                case (int)OptionSelectionEnum.Quit:
                    buttons[i].onClick.AddListener(() =>
                    {
                        UIManager.Instance.HideOptionsMenu();
                        foreach (var item in FindObjectsOfType<Button>(true).Where(x => x.GetComponent<BuzzingButtonComponent>()).ToArray()) item.interactable = true;
                    });
                    break;
                default:
                    throw new System.InvalidOperationException();
            }
        }
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Menu Selection Button Manager] : " + msg);
}
