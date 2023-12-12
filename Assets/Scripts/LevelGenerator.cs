using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelGenerator
{
    private static int currentLevel = 0;
    public static List<Level> levelList = new List<Level>(){
        new Level {
            goals = new List<GoalItem>() { new GoalItem {
                name = "Egg",
                count = 10
            }},
            path = ".\\Assets\\Python\\Level_Templates\\test.py"
        }
    };

    public static int GetCurrentLevel() {
        return currentLevel;
    }

    public static void SetCurrentLevel(int newLevel) {
        currentLevel = Math.Min(newLevel, PlayerPrefs.GetInt("lastLevelBeat") + 1);
    }

    public static void NextLevel() {
        currentLevel = Math.Min(currentLevel + 1, PlayerPrefs.GetInt("lastLevelBeat") + 1);
    }

    public static List<GoalItem> GetCurrentGoals() {
        return levelList[currentLevel].goals;
    }

    public static string GetCurrentPath() {
        return levelList[currentLevel].path;
    }
}

public class Level {
    public List<GoalItem> goals;
    public string path;
}
