using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PocketPython;
using System.IO;
using System.Threading;

public class PythonReader : MonoBehaviour
{
    [SerializeField] private string path;
    private ConveyorBelt conveyor;
    private VM vm;

    void Start()
    {
        conveyor = FindObjectOfType<ConveyorBelt>();
        vm = new VM();
        StreamReader sr = File.OpenText(".\\Assets\\Python\\playerAPI.py");
        string s = "";
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            s += "\n" + line;
        }
        vm.lazyModules["playerAPI"] = s;


        sr = File.OpenText(path);
        s = "";
        line = "";
        while ((line = sr.ReadLine()) != null)
        {
            s += "\n" + line;
        }
        RunProgramInThread(s);
    }

    public void RunProgramInThread(string code){
        Thread thread = new Thread(RunProgram);
        thread.IsBackground = true;
        thread.Start(code);
    }

    private void RunProgram(object arg){
        string code =  (string) arg;
        vm.Exec(code, "main.py");
        var obj = vm.Eval("opList.get_self()");

        var pyReturn = (List<object>)vm.GetAttr(obj, "text");
        foreach(var ingredient in pyReturn) {
            conveyor.AddToQueue(ingredient.ToString());
        }
    }
}

