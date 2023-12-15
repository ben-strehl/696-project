using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManipulator : MonoBehaviour
{
    public void AllowAllLevels() {
        PlayerPrefs.SetInt("lastLevelBeat", 100);
    }

    // Locks all levels
    public void Reset() {
        PlayerPrefs.SetInt("lastLevelBeat", 0);
    }

    public void Menu() {
        SceneManager.LoadScene("MainMenu");
    }
}