using TMPro;
using UnityEngine;

public class HintButton : MonoBehaviour
{
    private GameObject hintPanel;
    private TMP_Text text;
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        hintPanel = GameObject.Find("HintPanel");
        hintPanel.SetActive(false);
    }

    public void ToggleActive() {
        if(hintPanel.activeSelf){
            hintPanel.SetActive(false);
            text.text = "Show Functions";
        } else {
            hintPanel.SetActive(true);
            text.text = "Hide Functions";
        }
    }
}
