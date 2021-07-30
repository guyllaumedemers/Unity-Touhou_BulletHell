using TMPro;
using UnityEngine;

public static class PlayerConfig
{
    public static void SetPlayerPref(float[] values, params string[] playerPrefEntries)
    {
        if (values.Length != playerPrefEntries.Length)
        {
            LogWarning("The number of entries in the value array doesnt match the entries in the key array");
            return;
        }

        for (int i = 0; i < values.Length; ++i)
        {
            PlayerPrefs.SetFloat(playerPrefEntries[i], values[i]);
        }
    }

    private static void LogWarning(string msg) => Debug.LogWarning("[Audio Manager] " + msg);
}
