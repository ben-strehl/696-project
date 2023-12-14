using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private delegate void StopEvent();
    private PythonReader reader;
    private TMP_InputField input;
    private Button buttonComp;
    private TMP_Text text;
    private StopEvent stop;
    void Start()
    {
        reader = FindObjectOfType<PythonReader>();
        input = GameObject.Find("Text Editor").GetComponent<TMP_InputField>();
        buttonComp = GetComponent<Button>();
        text = GetComponentInChildren<TMP_Text>();
        buttonComp.onClick.AddListener(Play);

        stop += FindObjectOfType<Robot>().Reset;
        stop += FindObjectOfType<ConveyorBelt>().Reset;
        stop += FindObjectOfType<MixingTable>().Reset;
        stop += FindObjectOfType<DecoratingTable>().Reset;
        stop += FindObjectOfType<DeliveryTruck>().Reset;
        stop += FindObjectOfType<PythonReader>().Reset;
        stop += FindObjectOfType<SpeedupButton>().Reset;
    }

    //Start baking and toggle to stop button
    public void Play() {
        reader.RunProgramInThread(input.text);
        buttonComp.onClick.RemoveListener(Play);
        buttonComp.onClick.AddListener(Stop);
        text.text = "Stop";
    }

    //Reset objects and toggle to play button
    public void Stop() {
        stop();
        buttonComp.onClick.RemoveListener(Stop);
        buttonComp.onClick.AddListener(Play);
        text.text = "Play";
    }

    public void Reset() {
        buttonComp.onClick.RemoveListener(Stop);
        buttonComp.onClick.AddListener(Play);
        text.text = "Play";
    }
}
