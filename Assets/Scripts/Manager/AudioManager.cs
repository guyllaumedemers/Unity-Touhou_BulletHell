using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonMonoPersistent<AudioManager>, IFlow
{
    public AudioMixer mixer;
    AudioClip[] music_clips;
    AudioClip[] sfx_clips;
    AudioClip mouse_clip;
    AudioClip buttonclick_clip;
    AudioSource[] sources;
    private AudioManager() { }
    TextMeshProUGUI main_volumeTxt;
    TextMeshProUGUI se_volumeTxt;
    int main_volume;
    int se_volume;
    float lastTime;

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

    private float PercentTo(int value) => (value * Globals.channel_lowestvalue / Globals.max_percent) - Globals.channel_lowestvalue;

    private void SetChanel(string channel, float value) => mixer.SetFloat(channel, value);

    private void SetAudioSource(AudioSource src, AudioClip clip, AudioMixerGroup output, params bool[] state)
    {
        src.clip = clip;
        src.loop = state[0];
        src.outputAudioMixerGroup = output;
        if (state[1]) src.Play();
    }

    private AudioMixerGroup GetAudioMixerGroupChannel(string name)
    {
        return mixer.FindMatchingGroups(string.Empty).Where(x => x.name.Equals(name)).FirstOrDefault();
    }

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        SetAudio();
        RetrieveTags();
        SetText();
        lastTime = Time.time;
    }

    public void InitializationMethod()
    {
        //HINT : Mixer is not accessible in the Awake function when trying to set the audio via function call
        //It has to be done in the start function
        SetChanel(Globals.music_channel, PercentTo(main_volume));
        SetChanel(Globals.sfx_channel, PercentTo(se_volume));
    }

    public void UpdateMethod() { }

    /**************************************************/

    private void SetAudio()
    {
        main_volume = Globals.temp_start_percent;           // TEMP : should be loaded from file
        se_volume = Globals.temp_start_percent - 20;        // TEMP
        music_clips = Utilities.FindResources<AudioClip>(Globals.music_clips_path);
        sfx_clips = Utilities.FindResources<AudioClip>(Globals.sfx_clips_path);
        mouse_clip = sfx_clips[0];
        buttonclick_clip = sfx_clips[1];
        sources = GetComponents<AudioSource>();
        SetAudioSource(sources[0], music_clips[0], GetAudioMixerGroupChannel(Globals.music_channel), true, true);
    }

    private void SetText()
    {
        InitializeOnStartup(main_volumeTxt, main_volume);
        InitializeOnStartup(se_volumeTxt, se_volume);
    }

    //TO Avoid clicking a button and returning on the previous panel directly on another button triggering the other SFX
    public void TriggerMouseSFX()
    {
        if (Time.time - lastTime > 0.5f)
        {
            sources[1].PlayOneShot(mouse_clip);
            lastTime = Time.time;
        }
    }

    public void TriggerButtonClickSFX() => sources[2].PlayOneShot(buttonclick_clip);

    private void RetrieveTags()
    {
        main_volumeTxt = GameObject.FindGameObjectWithTag(Globals.mainVolumeTag).GetComponent<TextMeshProUGUI>();
        se_volumeTxt = GameObject.FindGameObjectWithTag(Globals.sfxVolumeTag).GetComponent<TextMeshProUGUI>();
    }

    private void InitializeOnStartup(TextMeshProUGUI text, int value) => text.text = value.ToString() + "%";
}
