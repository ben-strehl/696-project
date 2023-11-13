using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PocketPython;
using System.IO;
using System.Threading;

public class PythonReader : MonoBehaviour
{
    [SerializeField] private string path;
    private Thread thread;
    private VM vm;
    // Start is called before the first frame update
    void Start()
    {
        vm = new VM();
        /* var mod = vm.NewModule("playerAPI"); */
        /* vm.PyImport("playerAPI"); */
        StreamReader sr = File.OpenText(".\\Assets\\Python\\playerAPI.py");
        string s = "";
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            s += "\n" + line;
        }
        /* vm.Exec(s, "playerAPI.py"); */
        vm.lazyModules["playerAPI"] = s;
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
        
        /* Debug.Log("Read file"); */
        vm.Exec(s, "main.py");
        /* Debug.Log("Ran file"); */
        var obj = vm.Eval("opList.get_self()");

        /* Debug.Log("Object: " + obj); */
        var pyReturn = (List<System.Object>)vm.GetAttr(obj, "text");
        /* if(pyReturn.GetType() == typeof(List<System.Object>)){ */
            /* Debug.Log("String from python: " + ((List<System.Object>)pyReturn)[0]); */
        /* } */
        List<Ingredient> ingredients = new List<Ingredient>();
        foreach(var ingredient in (List<System.Object>)pyReturn)
        {
            ingredients.Add(JsonUtility.FromJson<Ingredient>(ingredient.ToString()));
        }

        Debug.Log("First ingredient: " + ingredients[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

public class TestClass {
    public string test;
}
