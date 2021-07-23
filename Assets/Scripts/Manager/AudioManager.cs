using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonMonoPersistent<AudioManager>, IFlow
{
    public AudioMixer mixer;
    private AudioManager() { }
    TextMeshProUGUI main_volumeTxt;
    TextMeshProUGUI se_volumeTxt;
    int main_volume;
    int se_volume;

    /*********************ACTIONS**************************/

    public void IncrementMainVolume()
    {
        main_volume = PlayerConfig.IncrementVolume(main_volumeTxt, main_volume);
        SetChanel(Globals.music_channel, PercentTo(main_volume));
    }

    public void DecrementMainVolume()
    {
        main_volume = PlayerConfig.DecrementVolume(main_volumeTxt, main_volume);
        SetChanel(Globals.music_channel, PercentTo(main_volume));
    }

    public void IncrementSFXVolume()
    {
        se_volume = PlayerConfig.IncrementVolume(se_volumeTxt, se_volume);
        SetChanel(Globals.sfx_channel, PercentTo(se_volume));
    }

    public void DecrementSFXVolume()
    {
        se_volume = PlayerConfig.DecrementVolume(se_volumeTxt, se_volume);
        SetChanel(Globals.sfx_channel, PercentTo(se_volume));
    }

    public void OnSceneLoading()
    {
        //TODO Retrieve the values for the main_volume and se_volume from the XML file upon scene loading / swaping scene
    }

    private float PercentTo(int value) => (value * Globals.channel_lowestvalue / Globals.percent) - Globals.channel_lowestvalue;

    private void SetChanel(string channel, float value) => mixer.SetFloat(channel, value);

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        main_volume = Globals.percent;      // TEMP : should be loaded from file
        se_volume = Globals.percent;        // TEMP
        main_volumeTxt = GameObject.FindGameObjectWithTag(Globals.mainVolumeTag).GetComponent<TextMeshProUGUI>();
        se_volumeTxt = GameObject.FindGameObjectWithTag(Globals.sfxVolumeTag).GetComponent<TextMeshProUGUI>();
    }

    public void InitializationMethod() { }

    public void UpdateMethod() { }
}
