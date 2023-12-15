using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintButton : MonoBehaviour
{
    private GameObject hintPanel;
    void Start()
    {
        hintPanel = GameObject.Find("HintPanel");
        hintPanel.SetActive(false);
    }

    public void ToggleActive() {
        if(hintPanel.activeSelf){
            hintPanel.SetActive(false);
        } else {
            hintPanel.SetActive(true);
        }
    }
}
