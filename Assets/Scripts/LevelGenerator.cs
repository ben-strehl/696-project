using System;
using System.Collections.Generic;
using UnityEngine;

//Everything in this class is static so it persists across scene changes
public class LevelGenerator
{
    private static int currentLevel = 0;
    //Levels are defined as a list of goals and a path to the template file
    private static List<Level> levelList = new List<Level>(){
        new Level {
            goals = new List<GoalItem>() { new GoalItem {
                name = "Egg",
                count = 1
            }},
            path = ".\\Assets\\Python\\Level_Templates\\test.py"
        },
        new Level {
            goals = new List<GoalItem>() { new GoalItem {
                name = "Egg",
                count = 1
            }},
            path = ".\\Assets\\Python\\Level_Templates\\level1.py"
        },
        new Level {
            goals = new List<GoalItem>() { new GoalItem {
                name = "Cake",
                count = 1
            }},
            path = ".\\Assets\\Python\\Level_Templates\\level2.py"
        },
        new Level {
            goals = new List<GoalItem>() { new GoalItem {
                name = "Sugar",
                count = 10
            }},
            path = ".\\Assets\\Python\\Level_Templates\\level3.py"
        },
        new Level {
            goals = new List<GoalItem>() { new GoalItem {
                name = "Chocolate Cake",
                count = 1
            }},
            path = ".\\Assets\\Python\\Level_Templates\\level4.py"
        },
        new Level {
            goals = new List<GoalItem>() { new GoalItem {
                name = "Chocolate Cake",
                count = 1
            }},
            path = ".\\Assets\\Python\\Level_Templates\\level5.py"
        },
        new Level {
            goals = new List<GoalItem>() { new GoalItem {
                name = "Cake",
                count = 10
            }},
            path = ".\\Assets\\Python\\Level_Templates\\level6.py"
        },
        new Level {
            goals = new List<GoalItem>() { new GoalItem {
                name = "Cake",
                count = 5
            },
            new GoalItem {
                name = "Cake (Sprinkles)",
                count = 5
            }},
            path = ".\\Assets\\Python\\Level_Templates\\level7.py"
        },
        new Level {
            goals = new List<GoalItem>() { new GoalItem {
                name = "Cake",
                count = 3
            },
            new GoalItem {
                name = "Cake (Sprinkles)",
                count = 3
            },
            new GoalItem {
                name = "Chocolate Cake",
                count = 3
            },
            new GoalItem {
                name = "Chocolate Cake (Sprinkles)",
                count = 3
            }},
            path = ".\\Assets\\Python\\Level_Templates\\level8.py"
        },
        new Level {
            goals = new List<GoalItem>() { new GoalItem {
                name = "Cake",
                count = 10
            },
            new GoalItem {
                name = "Cake (Sprinkles)",
                count = 10
            }},
            path = ".\\Assets\\Python\\Level_Templates\\level9.py"
        }
    };

    public static int GetCurrentLevel() {
        return currentLevel;
    }

    //Current level cannot be incremented more than one past the last level beaten
    public static void SetCurrentLevel(int newLevel) {
        currentLevel = Math.Min(newLevel, PlayerPrefs.GetInt("lastLevelBeat") + 1);
    }

    public static void NextLevel() {
        currentLevel = Math.Min(currentLevel + 1, PlayerPrefs.GetInt("lastLevelBeat") + 1);
    }

    public static List<GoalItem> GetCurrentGoals() {
        //We must copy items over to avoid overwriting the static list
        var list = new List<GoalItem>();
        levelList[currentLevel].goals.ForEach(x => list.Add(new GoalItem {
            name = x.name,
            count = x.count
        }));
        return list;
    }

    public static string GetCurrentPath() {
        return levelList[currentLevel].path;
    }

    public static int getLevelCount() {
        return levelList.Count;
    }
}

public class Level {
    public List<GoalItem> goals;
    public string path;
}
