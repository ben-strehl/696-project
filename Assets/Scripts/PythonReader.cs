using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PocketPython;
using System.IO;
using System.Threading;
using TMPro;

public class PythonReader : MonoBehaviour
{
    private ConveyorBelt conveyor;
    private VM vm;
    private Thread thread;
    private string playerAPI;
    private string errorMessage;
    private TMP_InputField errorDisplay;
    private PlayButton playButton;

    void Start()
    {
        errorMessage = "";
        conveyor = FindObjectOfType<ConveyorBelt>();
        errorDisplay = GameObject.Find("ErrorDisplay").GetComponent<TMP_InputField>();
        errorDisplay.interactable = false;
        playButton = FindObjectOfType<PlayButton>();
        vm = new VM();
        thread = new Thread(RunProgram);
        thread.IsBackground = true;

        //Create the playerAPI module
        StreamReader sr = File.OpenText(".\\Assets\\Python\\playerAPI.py");
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            playerAPI += "\n" + line;
        }
        vm.lazyModules["playerAPI"] = playerAPI;
    }

    //Update the display with any error messages
    void Update() {
        errorDisplay.text = errorMessage;
        if(errorMessage != "") {
            playButton.Stop();
        }
    }

    public void RunProgramInThread(string code){
        thread.Start(code);
    }

    public void Reset() {
        thread.Abort();
        thread = new Thread(RunProgram);
        vm = new VM();
        vm.lazyModules["playerAPI"] = playerAPI;
    }

    private void RunProgram(object arg){
        errorMessage = "";
        string code =  (string) arg;

        vm.Exec("from playerAPI import *", "main.py");

        try {
            vm.Exec(code, "main.py");
        } catch (PyException ex) {
           errorMessage = ex.Message;
        }
        //This grabs the opList object, which contains the list of ingredients the player has specified
        var obj = vm.Eval("opList.get_self()");

        var pyReturn = (List<object>)vm.GetAttr(obj, "text");
        foreach(var ingredient in pyReturn) {
            conveyor.AddToQueue(ingredient.ToString());
        }
    }
}