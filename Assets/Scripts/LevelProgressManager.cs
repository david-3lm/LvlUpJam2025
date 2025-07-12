using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelProgressManager
{
    public static bool IsLevelUnlocked(int levelIndex)
    {
        return PlayerPrefs.GetInt("LevelUnlocked_" + levelIndex, levelIndex == 0 ? 1 : 0) == 1;
    }

    public static void UnlockLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("LevelUnlocked_" + levelIndex, 1);
        PlayerPrefs.Save();
    }
}
