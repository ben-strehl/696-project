using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    private CanvasGroup cGroup;
    private GameObject nextLevelButton;
    void Start()
    {
        cGroup = GetComponent<CanvasGroup>();
        cGroup.alpha = 0f;
        cGroup.interactable = false;

        nextLevelButton = GameObject.Find("NextLevelButton");
    }

    public void MakeActive() {
        cGroup.alpha = 1f;
        cGroup.interactable = true;

        //Don't show the "Next Level" button if this is the last button
        if(LevelGenerator.GetCurrentLevel() + 1 >= LevelGenerator.getLevelCount()) {
            nextLevelButton.SetActive(false);
        }
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel() {
        LevelGenerator.NextLevel();
        SceneManager.LoadScene("LevelScene");
    }
}
