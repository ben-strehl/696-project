using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void LoadLevelSelect() {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Quit() {
        Application.Quit();
    }
}
