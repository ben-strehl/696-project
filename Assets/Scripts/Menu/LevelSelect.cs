using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private int levelNum;
    private TMP_Text text;
    private Button buttonComp;
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        buttonComp = GetComponent<Button>();

        int lastLevelBeat = PlayerPrefs.GetInt("lastLevelBeat");

        text.text = "Level " + levelNum;
        if(lastLevelBeat >= levelNum) {
            text.color = Color.green;
            buttonComp.interactable = true;
        } else if(lastLevelBeat == levelNum - 1) {
            text.color = Color.black;
            buttonComp.interactable = true;
        } else {
            text.color = Color.black;
            buttonComp.interactable = false;
        }
    }

    void OnGUI() {
        int lastLevelBeat = PlayerPrefs.GetInt("lastLevelBeat");

        text.text = "Level " + levelNum;
        if(lastLevelBeat >= levelNum) {
            text.color = Color.green;
            buttonComp.interactable = true;
        } else if(lastLevelBeat == levelNum - 1) {
            text.color = Color.black;
            buttonComp.interactable = true;
        } else {
            text.color = Color.black;
            buttonComp.interactable = false;
        }
    }

    public void LoadLevel() {
        LevelGenerator.SetCurrentLevel(levelNum);
        SceneManager.LoadScene("LevelScene");
    }

}