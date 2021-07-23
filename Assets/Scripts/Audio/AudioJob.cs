
public class AudioJob
{
    public AudioJobActionEnum actionType;
    public AudioTypeEnum audioType;

    public AudioJob(AudioJobActionEnum action, AudioTypeEnum type)
    {
        actionType = action;
        audioType = type;
    }
}
