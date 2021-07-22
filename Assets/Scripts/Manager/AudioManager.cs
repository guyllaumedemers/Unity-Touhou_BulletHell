using TMPro;
using UnityEngine;

public class AudioManager : SingletonMonoPersistent<AudioManager>, IFlow
{
    private AudioManager() { }
    TextMeshProUGUI main_volumeTxt;
    TextMeshProUGUI se_volumeTxt;
    int main_volume;
    int se_volume;

    /*********************ACTIONS**************************/

    public void IncrementMainVolume()
    {
        main_volume = PlayerConfig.IncrementVolume(main_volumeTxt, main_volume);
    }

    public void DecrementMainVolume()
    {
        main_volume = PlayerConfig.DecrementVolume(main_volumeTxt, main_volume);
    }

    public void IncrementSFXVolume()
    {
        se_volume = PlayerConfig.IncrementVolume(se_volumeTxt, se_volume);
    }

    public void DecrementSFXVolume()
    {
        se_volume = PlayerConfig.DecrementVolume(se_volumeTxt, se_volume);
    }

    public void OnSceneLoading()
    {
        //TODO Retrieve the values for the main_volume and se_volume from the XML file upon scene loading / swaping scene
    }

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        main_volume = 100;
        se_volume = 100;
        main_volumeTxt = GameObject.FindGameObjectWithTag(Globals.mainVolumeTag).GetComponent<TextMeshProUGUI>();
        se_volumeTxt = GameObject.FindGameObjectWithTag(Globals.sfxVolumeTag).GetComponent<TextMeshProUGUI>();
    }

    public void InitializationMethod() { }

    public void UpdateMethod() { }
}
