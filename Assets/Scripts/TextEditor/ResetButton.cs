using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class ResetButton : MonoBehaviour
{
    private TextEditor editor;
    private TMP_InputField input;
    void Start() {
        editor = FindObjectOfType<TextEditor>();
        input = GameObject.Find("Text Editor").GetComponent<TMP_InputField>();
    }

    //Reset text editor by re-reading template python file
    public void ResetCode() {
        StreamReader sr = File.OpenText(editor.path);
        string s = "";
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            s += "\n" + line;
        }

        input.text = s[(s.IndexOf('\n') + 1)..];
    }
}
