using UnityEngine;
using System.IO;
using TMPro;

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
        //Make tab into 4 spaces
        var current = Event.current;
            if (current.type == EventType.KeyDown || current.type == EventType.KeyUp)
            {
                if (current.isKey && (current.keyCode == KeyCode.Tab || current.character == '\t'))
                {
                    if (current.type == EventType.KeyUp)
                    {
                        inputComponent.text += "    ";
                        inputComponent.caretPosition += 4;
                    }
                    current.Use();
                }
            }
    }

    public void Highlight(string text) {
        // Debug.Log(text);
    }
}