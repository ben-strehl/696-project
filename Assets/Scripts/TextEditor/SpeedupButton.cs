using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UI;

public class SpeedupButton : MonoBehaviour
{
    public float speedUpFactor;
    private TMP_Text text;
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        speedUpFactor = 1f;
    }

    public void ToggleSpeedup() {
        if(text.text == "Speed Up") {
            speedUpFactor = 2f;
            text.text = "Slow Down";
        } else {
            speedUpFactor = 1f;
            text.text = "Speed Up";
        }
    }

    public void Reset() {
        text.text = "Speed Up";
    }

}
