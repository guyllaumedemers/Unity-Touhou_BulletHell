using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerConfig
{
    public static int IncrementVolume(TextMeshProUGUI volumeTxt, int value)
    {
        if (++value >= 100) value = 100;
        volumeTxt.text = value.ToString() + "%";
        return value;
    }

    public static int DecrementVolume(TextMeshProUGUI volumeTxt, int value)
    {
        if (--value <= 0) value = 0;
        volumeTxt.text = value.ToString() + "%";
        return value;
    }

    public static bool SetWindow(Button clicked)
    {
        //TODO brainstorm about this
        return false;
    }

    public static bool Mute(Button clicked)
    {
        //TODO brainstorm about this
        return false;
    }

    public static void Reset()
    {

    }
}
