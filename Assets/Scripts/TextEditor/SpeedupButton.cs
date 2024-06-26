using TMPro;
using UnityEngine;

public class SpeedupButton : MonoBehaviour
{
    public float speedUpFactor;
    private TMP_Text text;
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        speedUpFactor = 1f;
    }

    //Can speed up by a factor of 2
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
        speedUpFactor = 1f;
    }

}
