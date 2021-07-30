using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonMono<AudioManager>, IFlow
{
    public AudioMixer mixer;
    private AudioManager() { }
    TextMeshProUGUI main_volumeTxt;
    TextMeshProUGUI se_volumeTxt;
    int main_volume;
    int se_volume;
    float lastTime;

    #region Audio Manager Functions

    public void IncrementMainVolume()
    {
        main_volume = PlayerConfig.IncrementVolume(main_volumeTxt, main_volume);
        SetChanel(Globals.ST_Channel, PercentTo(main_volume));
    }

    public void DecrementMainVolume()
    {
        main_volume = PlayerConfig.DecrementVolume(main_volumeTxt, main_volume);
        SetChanel(Globals.ST_Channel, PercentTo(main_volume));
    }

    public void IncrementSFXVolume()
    {
        se_volume = PlayerConfig.IncrementVolume(se_volumeTxt, se_volume);
        SetChanel(Globals.MenuSFX_Channel, PercentTo(se_volume));
    }

    public void DecrementSFXVolume()
    {
        se_volume = PlayerConfig.DecrementVolume(se_volumeTxt, se_volume);
        SetChanel(Globals.MenuSFX_Channel, PercentTo(se_volume));
    }

    public void DisableMixer(TextMeshProUGUI text)
    {
        if (!text)
        {
            LogWarning("There is no text assigned to the Enable Mixer Event");
            return;
        }
        SetChanel(Globals.Main_Channel, -Globals.channel_lowestvalue);
        text.color = Color.grey;
    }

    public void EnableMixer(TextMeshProUGUI text)
    {
        if (!text)
        {
            LogWarning("There is no text assigned to the Enable Mixer Event");
            return;
        }
        SetChanel(Globals.Main_Channel, Globals.channel_default);
        text.color = Color.grey;
    }

    public void OnSceneLoading()
    {
        //TODO Retrieve the values for the main_volume and se_volume from the XML file upon scene loading / swaping scene
    }

    //TO Avoid clicking a button and returning on the previous panel directly on another button triggering the other SFX
    public void TriggerMouseSFX()
    {
        if (Time.time - lastTime > Globals.nextSFXTime)
        {
            AudioController.Instance.Play(AudioTypeEnum.MenuSFX_01);
            lastTime = Time.time;
        }
    }

    public void TriggerButtonClickSFX() => AudioController.Instance.Play(AudioTypeEnum.MenuSFX_02);

    private float PercentTo(int value) => (value * Globals.channel_lowestvalue / Globals.max_percent) - Globals.channel_lowestvalue;

    #endregion

    #region private functions

    private void LogWarning(string msg) => Debug.LogWarning("[Audio Manager] " + msg);

    #endregion

    #region Unity Functions

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        AudioController.Instance.PreIntilizationMethod();
        SetAudio();
        RetrieveTags();
        SetText();
        lastTime = Time.time;
    }

    public void InitializationMethod()
    {
        AudioController.Instance.InitializationMethod();
        //HINT : Mixer is not accessible in the Awake function when trying to set the audio via function call
        //It has to be done in the start function
        SetChanel(Globals.ST_Channel, PercentTo(main_volume));
        SetChanel(Globals.MenuSFX_Channel, PercentTo(se_volume));
    }

    public void UpdateMethod() { }

    #endregion

    #region Audio Manager Initialization Functions

    private void SetAudio()
    {
        main_volume = Globals.temp_start_percent;           // TEMP : should be loaded from file
        se_volume = Globals.temp_start_percent - 10;        // TEMP
    }

    private void RetrieveTags()
    {
        main_volumeTxt = GameObject.FindGameObjectWithTag(Globals.mainVolumeTag).GetComponent<TextMeshProUGUI>();
        se_volumeTxt = GameObject.FindGameObjectWithTag(Globals.sfxVolumeTag).GetComponent<TextMeshProUGUI>();
    }

    private void InitializeOnStartup(TextMeshProUGUI text, int value) => text.text = value.ToString() + "%";

    private void SetText()
    {
        InitializeOnStartup(main_volumeTxt, main_volume);
        InitializeOnStartup(se_volumeTxt, se_volume);
    }

    private void SetChanel(string channel, float value) => mixer.SetFloat(channel, value);

    #endregion
}
