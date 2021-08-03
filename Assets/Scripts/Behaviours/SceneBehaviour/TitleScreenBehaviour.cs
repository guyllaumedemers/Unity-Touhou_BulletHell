using UnityEngine;
using UnityEngine.UI;

public class TitleScreenBehaviour : AbsSceneHandler
{
    private Button startButton;

    private void Awake()
    {
        SetStartButton(ref startButton);
        Load(ref alphagroup);
        AudioManager.Instance.PreInitializeTitleScreen();
    }

    private void Start()
    {
        Play();
        AudioManager.Instance.InitializeTitleScreen();
    }

    #region private functions

    private void SetStartButton(ref Button start)
    {
        start = FindObjectOfType<Button>();
        if (!start)
        {
            LogWarning("There is no button in the scene");
            return;
        }
        start.onClick.AddListener(EntryPoint.Instance.TriggerNextScene);
    }

    private void LogWarning(string msg) => Debug.Log("[Title Screen Behaviour] : " + msg);

    #endregion
}
