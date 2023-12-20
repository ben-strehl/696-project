using UnityEngine;
using System.IO;
using TMPro;

public class TextEditor: MonoBehaviour
{
    public string path;
    public string pythonString;
    private TMP_InputField inputComponent;
    private Highlighter highlighter;
    private int caretPos;
    private bool newCaretPos;

    void Start() {
        path = LevelGenerator.GetCurrentPath();

        StreamReader sr = File.OpenText(path);
        string s = "";
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            s += "\n" + line;
        }

        highlighter = new Highlighter();

        inputComponent = GetComponent<TMP_InputField>();
        inputComponent.text = s.Substring(s.IndexOf('\n') + 1);

        // (pythonString, inputComponent.text) = highlighter.HighlightInitial(s.Substring(s.IndexOf('\n') + 1));
        // Debug.Log(inputComponent.text);
    }

    void Update() {
        if(newCaretPos) {
            inputComponent.caretPosition = caretPos;
            newCaretPos = false;
        }
    }

    public void Highlight(string text) {
        string temp;
        (pythonString, temp, caretPos) = highlighter.Highlight(text);
        inputComponent.SetTextWithoutNotify(temp);
        newCaretPos = true;
    }
}