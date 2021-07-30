using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonMono<AudioManager>, IFlow
{
    public AudioMixer mixer;
    private AudioManager() { }
    private TextMeshProUGUI main_volumeTxt;
    private TextMeshProUGUI se_volumeTxt;
    private float main_volume;
    private float se_volume;
    private float lastTime;

    #region Audio Manager Functions

    public void IncrementMainVolume()
    {
        main_volume = IncrementVolume(main_volumeTxt, (int)main_volume);
        SetChanel(Globals.ST_Channel, PercentTo(main_volume));
    }

    public void DecrementMainVolume()
    {
        main_volume = DecrementVolume(main_volumeTxt, (int)main_volume);
        SetChanel(Globals.ST_Channel, PercentTo(main_volume));
    }

    public void IncrementSFXVolume()
    {
        se_volume = IncrementVolume(se_volumeTxt, (int)se_volume);
        SetChanel(Globals.MenuSFX_Channel, PercentTo(se_volume));
    }

    public void DecrementSFXVolume()
    {
        se_volume = DecrementVolume(se_volumeTxt, (int)se_volume);
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

    public void SaveAudio() => PlayerConfig.SetPlayerPref(new float[] { main_volume, se_volume }, new string[] { Globals.Main_Channel, Globals.SE_Channel });

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

    #endregion

    #region private functions

    //TODO Find a way to make this more parametrize -> currently cannot pass 2 arrays since the values updated wont translate to the class variable of main_volume and se_volume
    //since they would be pass as copy and not by reference AND also since params cannot handle ref params... which sucks
    private void LoadPlayerPref()
    {
        main_volume = PlayerPrefs.GetFloat(Globals.Main_Channel);
        se_volume = PlayerPrefs.GetFloat(Globals.SE_Channel);
    }

    #region UI Management for Audio - HANDLING EXCEPTION - SHOULD BE DECOUPLED FROM THE AUDIO MANAGEMENT

    private float PercentTo(float value) => (value * Globals.channel_lowestvalue / Globals.max_percent) - Globals.channel_lowestvalue;

    private void RetrieveTags()
    {
        main_volumeTxt = GameObject.FindGameObjectWithTag(Globals.mainVolumeTag).GetComponent<TextMeshProUGUI>();
        se_volumeTxt = GameObject.FindGameObjectWithTag(Globals.sfxVolumeTag).GetComponent<TextMeshProUGUI>();
    }


    private void SetTextToAudioComponentsValues(float[] values, params TextMeshProUGUI[] text)
    {
        if (values.Length != text.Length)
        {
            LogWarning("The number of entries in the value array doesnt match the entries in the key array");
            return;
        }

        for (int i = 0; i < values.Length; ++i)
        {
            InitializeOnStartup(text[i], values[i]);
        }
    }
    private void InitializeOnStartup(TextMeshProUGUI text, float value) => text.text = value.ToString() + "%";

    public int IncrementVolume(TextMeshProUGUI volumeTxt, int value)
    {
        if (++value >= 100) value = 100;
        volumeTxt.text = value.ToString() + "%";
        return value;
    }

    public int DecrementVolume(TextMeshProUGUI volumeTxt, int value)
    {
        if (--value <= 0) value = 0;
        volumeTxt.text = value.ToString() + "%";
        return value;
    }

    #endregion

    private void SetAudioComponentsValues(float[] values, string[] channels)
    {
        if (values.Length != channels.Length)
        {
            LogWarning("The number of entries in the value array doesnt match the entries in the key array");
            return;
        }

        for (int i = 0; i < values.Length; ++i)
        {
            SetChanel(channels[i], values[i]);
        }
    }

    private void SetChanel(string channel, float value) => mixer.SetFloat(channel, value);

    private void LogWarning(string msg) => Debug.LogWarning("[Audio Manager] " + msg);

    #endregion

    #region Unity Functions

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        AudioController.Instance.PreIntilizationMethod();
        LoadPlayerPref();
        RetrieveTags();
        SetTextToAudioComponentsValues(new float[] { main_volume, se_volume }, new TextMeshProUGUI[] { main_volumeTxt, se_volumeTxt });
        lastTime = Time.time;
    }

    public void InitializationMethod()
    {
        AudioController.Instance.InitializationMethod();
        SetAudioComponentsValues(new float[] { PercentTo(main_volume), PercentTo(se_volume) }, new string[] { Globals.ST_Channel, Globals.MenuSFX_Channel });
    }

    public void UpdateMethod() { }

    #endregion
}
