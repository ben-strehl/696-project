using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private int levelNum;
    void Start()
    {
        
    }

    private void LoadLevel() {
        LevelGenerator.SetCurrentLevel(levelNum);
        SceneManager.LoadScene("LevelScene");
    }
}
