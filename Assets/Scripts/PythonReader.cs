using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PocketPython;
using System.IO;
using System.Threading;
using System;
using System.Linq.Expressions;
using UnityEngine.Rendering;
using System.Data;

public class PythonReader : MonoBehaviour
{
    private ConveyorBelt conveyor;
    private VM vm;
    private Thread thread;
    private string playerAPI;

    void Start()
    {
        conveyor = FindObjectOfType<ConveyorBelt>();
        vm = new VM();
        thread = new Thread(RunProgram);
        thread.IsBackground = true;

        StreamReader sr = File.OpenText(".\\Assets\\Python\\playerAPI.py");
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            playerAPI += "\n" + line;
        }
        vm.lazyModules["playerAPI"] = playerAPI;
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
        string code =  (string) arg;
        vm.Exec("from playerAPI import *", "main.py");
        vm.Exec(code, "main.py");
        var obj = vm.Eval("opList.get_self()");

        var pyReturn = (List<object>)vm.GetAttr(obj, "text");
        foreach(var ingredient in pyReturn) {
            conveyor.AddToQueue(ingredient.ToString());
        }
    }
}

