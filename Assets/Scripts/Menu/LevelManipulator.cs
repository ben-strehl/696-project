using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelManipulator : MonoBehaviour
{
    public void AllowAllLevels() {
        PlayerPrefs.SetInt("lastLevelBeat", 100);
    }

    public void Reset() {
        PlayerPrefs.SetInt("lastLevelBeat", 0);
    }
}