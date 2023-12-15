using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PocketPython;
using System.IO;

public class PythonReader : MonoBehaviour
{
    private VM vm;
    // Start is called before the first frame update
    void Start()
    {
        vm = new VM();

        vm.RegisterType(new PyPlayerConnector(), vm.builtins);
        // RunProgram();
        vm.Exec("pc = player_connector()", "main.py");
        vm.Exec("pc.forward('1')", "main.py");
    }

    void RunProgram(){
        string path = ".\\Assets\\Python\\pythonInterTest.py";
        
        StreamReader sr = File.OpenText(path);
        string s = "";
        while ((s += sr.ReadLine()) != null)
        {}

        vm.Exec(s, "main.py");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}