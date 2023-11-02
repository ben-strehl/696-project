using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PocketPython;
using System.IO;
using System.Threading;

public class PythonReader : MonoBehaviour
{
    private VM vm;
    // Start is called before the first frame update
    void Start()
    {
        vm = new VM();
        RunProgram();
        /* Thread t = new Thread(new ThreadStart(ReadFile)); */
        /* t.Start(); */
        /* /1* vm.RegisterType(new PyPlayerConnector(), vm.builtins); *1/ */
        /* // RunProgram(); */
        /* vm.Exec("pc = player_connector()", "main.py"); */
        /* vm.Exec("pc.forward('1')", "main.py"); */
    }

    void RunProgram(){
        string path = ".\\Assets\\Python\\pythonInterTest.py";
        

        string s = 
            @"
class MyClass:
  def __init__(self, text):
    self.text = text

  def get_self(self):
    return self

bob = MyClass('Hello from Python!')

             ";
        /* Debug.Log("Read file"); */
        vm.Exec(s, "main.py");
        var obj = vm.Eval("bob.get_self()");

        Debug.Log("Object: " + obj);
        var pyReturn = vm.GetAttr(obj, "text");
        Debug.Log("String from python: " + pyReturn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ReadFile(){
        string path = ".\\Assets\\Python\\pythonInterTest.py";
        StreamReader sr = File.OpenText(path);
        string s = "";
        while ((s += sr.ReadLine()) != null)
        {
            Debug.Log(s);
        }

    }
}
