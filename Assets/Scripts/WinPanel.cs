using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    private CanvasGroup cGroup;
    void Start()
    {
        cGroup = GetComponent<CanvasGroup>();
        cGroup.alpha = 0f;
        cGroup.interactable = false;
    }

    public void MakeActive() {
        cGroup.alpha = 1f;
        cGroup.interactable = true;
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel() {
        LevelGenerator.NextLevel();
        SceneManager.LoadScene("LevelScene");
    }
}
