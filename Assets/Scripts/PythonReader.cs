using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PocketPython;
using System.IO;
using System.Threading;

public class PythonReader : MonoBehaviour
{
    [SerializeField] private string path;
    [SerializeField] private GameObject conveyerObject;
    private ConveyorBelt conveyor;
    private Thread thread;
    private VM vm;

    void Start()
    {
        conveyor = conveyerObject.GetComponent<ConveyorBelt>();
        vm = new VM();
        StreamReader sr = File.OpenText(".\\Assets\\Python\\playerAPI.py");
        string s = "";
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            s += "\n" + line;
        }
        vm.lazyModules["playerAPI"] = s;

        RunProgramInThread();
    }

    void RunProgramInThread(){
        ThreadStart ts = new ThreadStart(RunProgram);
        thread = new Thread(ts);
        thread.IsBackground = true;
        thread.Start();
    }

    void RunProgram(){
        StreamReader sr = File.OpenText(path);
        string s = "";
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            s += "\n" + line;
        }
        
        vm.Exec(s, "main.py");
        var obj = vm.Eval("opList.get_self()");

        var pyReturn = (List<System.Object>)vm.GetAttr(obj, "text");
        /* pyReturn.ForEach(ingredient => conveyor.AddToQueue(ingredient.ToString())); */
        foreach(var ingredient in pyReturn) {
            conveyor.AddToQueue(ingredient.ToString());
            Thread.Sleep(1000);
        }
    }
}

