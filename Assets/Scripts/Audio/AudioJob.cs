
public class AudioJob
{
    public AudioJobActionEnum job_type;
    public AudioTypeEnum audio_type;

    public AudioJob(AudioJobActionEnum action, AudioTypeEnum type)
    {
        job_type = action;
        audio_type = type;
    }
}
