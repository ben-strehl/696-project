using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System;

public class TextEditor: MonoBehaviour
{
    public string path;
    private TMP_InputField inputComponent;

    void Start() {
        path = LevelGenerator.GetCurrentPath();

        StreamReader sr = File.OpenText(path);
        string s = "";
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            s += "\n" + line;
        }

        inputComponent = GetComponent<TMP_InputField>();
        inputComponent.text = s.Substring(s.IndexOf('\n') + 1);
    }

    void OnGUI() {
        var current = Event.current;
            if (current.type == EventType.KeyDown || current.type == EventType.KeyUp)
            {
                if (current.isKey && (current.keyCode == KeyCode.Tab || current.character == '\t'))
                {
                    if (current.type == EventType.KeyUp)
                    {
                        inputComponent.text += "  ";
                        inputComponent.caretPosition += 2;
                    }
                    current.Use();
                }
            }
    }
    void Update() {

    }

    public void Highlight(string text) {
        // Debug.Log(text);
    }
}