using System.Collections;
using UnityEngine;
using System.Linq;
using System;

public class AudioController : SingletonMono<AudioController>, IFlow
{
    private AudioController() { }
    private Hashtable hashJob;          // relationship between audio type and jobs (coroutine or actions)
    private Hashtable hashAudio;        // relationship between audio types (key) and audio tracks (value)
    public AudioTrack[] audioTracks;

    #region public functions

    public void Play(AudioTypeEnum type) => AddJob(new AudioJob(AudioJobActionEnum.START, type));

    public void Stop(AudioTypeEnum type) => AddJob(new AudioJob(AudioJobActionEnum.STOP, type));

    public void Restart(AudioTypeEnum type) => AddJob(new AudioJob(AudioJobActionEnum.RESTART, type));

    #endregion

    #region private functions

    private void Configure()
    {
        hashAudio = new Hashtable();
        hashJob = new Hashtable();
        PopulateAudioTable();
    }

    private void Dispose()
    {
        foreach (DictionaryEntry entry in hashJob)
        {
            IEnumerator job = (IEnumerator)entry.Value;
            if (job != null)
            {
                StopCoroutine(job);
            }
        }
    }

    private void PopulateAudioTable()
    {
        foreach (var (track, audioOBJ) in audioTracks.SelectMany(track => track.audio_objects.Select(audioOBJ => (track, audioOBJ))))
        {
            if (AudioExist(audioOBJ.audio_type))
            {
                LogWarning("You are trying to add an Audio Type that already exist");
                return;
            }
            hashAudio.Add(audioOBJ.audio_type, track);
        }
    }

    private void AddJob(AudioJob job)
    {
        RemoveConflictingJobs(job.audio_type);

        IEnumerator jobRunner = RunAudioJob(job);
        StartCoroutine(jobRunner);
        hashJob.Add(job.audio_type, jobRunner);
    }

    private void RemoveJob(AudioTypeEnum type)
    {
        if (!JobExist(type))
        {
            LogWarning("The Type to remove does not exist");
            return;
        }

        IEnumerator runningJob = (IEnumerator)hashJob[type];
        if (runningJob != null)
        {
            StopCoroutine(runningJob);
        }
        hashJob.Remove(type);
    }

    //HINT Remove conflict is use to compare if the new job to create already exist so -> Is there a Job registered inside the hashJob
    //     that uses the same AudioTrack (which means using the same AudioSource since an Audiotrack aggregate an AudioSource)
    //     SO, We need to loop over every KVP entry inside the HashTable and access the value at Key so we can compare the AudioTrack retrieve
    //     with the value at Key pass as arguments of the function
    private void RemoveConflictingJobs(AudioTypeEnum type)
    {
        if (JobExist(type))
        {
            RemoveJob(type);
        }

        AudioTypeEnum conflictAudio = AudioTypeEnum.NONE;
        foreach (DictionaryEntry item in hashJob)
        {
            AudioTypeEnum itemAudioType = (AudioTypeEnum)item.Key;
            AudioTrack audioTrackInUse = (AudioTrack)hashAudio[itemAudioType];
            AudioTrack audioTrackNeeded = (AudioTrack)hashAudio[type];
            if (audioTrackNeeded.Equals(audioTrackInUse))
            {
                conflictAudio = itemAudioType;
            }
        }
        if (conflictAudio != AudioTypeEnum.NONE) RemoveJob(conflictAudio);
    }

    private IEnumerator RunAudioJob(AudioJob job)
    {
        AudioTrack track = (AudioTrack)hashAudio[job.audio_type];
        track.source.clip = GetAudioClipFromTrack(job.audio_type, track);

        switch (job.job_type)
        {
            case AudioJobActionEnum.START:
                track.source.Play();
                break;
            case AudioJobActionEnum.STOP:
                track.source.Stop();
                break;
            case AudioJobActionEnum.RESTART:
                track.source.Stop();
                track.source.Play();
                break;
            default:
                LogWarning("Invalid Job Enum Type");
                break;
        }

        RemoveJob(job.audio_type);
        yield return null;
    }

    private AudioClip GetAudioClipFromTrack(AudioTypeEnum audio_type, AudioTrack track)
    {
        return track.audio_objects.Where(audioOBJ => audioOBJ.audio_type.Equals(audio_type)).FirstOrDefault().audio_clip;
    }

    private bool AudioExist(AudioTypeEnum type) => hashAudio.ContainsKey(type);

    private bool JobExist(AudioTypeEnum type) => hashAudio.ContainsKey(type);

    private void LogWarning(string msg) => Debug.LogWarning("[Audio Controller] : " + msg);

    #endregion

    #region Unity functions

    public void PreIntilizationMethod() => Configure();

    public void InitializationMethod()
    {
        Play(AudioTypeEnum.ST_01);
    }

    public void UpdateMethod() { }

    #endregion

    private void OnDisable() => Dispose();
}
