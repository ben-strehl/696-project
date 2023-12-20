using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Highlighter 
{
    private Regex keywordRx = new Regex(@"(?<!(?<!<color=)#[^\n]*)\b(for|in|if|elif|else|class|and|break|continue|def|True|False|from|not|or|return|while)\b", RegexOptions.Compiled);
    private Regex functionRx = new Regex(@"(?<!(?<!<color=)#[^\n]*)[a-z,A-Z,_]+\(", RegexOptions.Compiled);
    private Regex numRx = new Regex(@"(?<!(?<!<color=)#[^\n]*)\b(?<!#)\d+\b", RegexOptions.Compiled);
    private Regex stringRx = new Regex(@"(?<!(?<!<color=)#[^\n]*)"".*""", RegexOptions.Compiled);
    private Regex commentRx = new Regex(@"(?<!<color=)#[^\n]*", RegexOptions.Compiled);
    private Regex colorRx = new Regex(@"(<color=#\w{6,8}>|</color>)", RegexOptions.Compiled);
    private TMP_InputField textEditor;

    public Highlighter() {
        textEditor = GameObject.Find("Text Editor").GetComponent<TMP_InputField>();
    }

    public (string, string, int) Highlight(string coloredText) {
        StringBuilder coloredSB = new StringBuilder(coloredText, coloredText.Length + 50);

        //Looping this way as opposed to foreach allows us to update the regex matches as we go
        MatchCollection colorCollection = colorRx.Matches(coloredText);
        int count = colorCollection.Count;
        for(int i = 0; i < count; i++) {
            coloredSB.Remove(colorCollection[0].Index, colorCollection[0].Length);
            colorCollection = colorRx.Matches(coloredSB.ToString());
        }

        // if(Input.GetKey(KeyCode.Tab)) {
        //     coloredSB.Insert(textEditor.caretPosition, "    ");
        //     textEditor.caretPosition += 4;
        // }

        //This is our text without the rich text formatting and it should be runnable for pocketpy
        string pythonString = coloredSB.ToString();
        
        MatchCollection stringCollection = stringRx.Matches(coloredSB.ToString());
        count = stringCollection.Count;
        for(int i = 0; i < count; i++){
            coloredSB.Remove(stringCollection[i].Index, stringCollection[i].Length);
            coloredSB.Insert(stringCollection[i].Index, @"<color=#b8bb26ff>" + stringCollection[i].Value + "</color>");
            stringCollection = stringRx.Matches(coloredSB.ToString());
        }

        MatchCollection keywordCollection = keywordRx.Matches(coloredSB.ToString());
        count = keywordCollection.Count;
        for(int i = 0; i < count; i++){
            coloredSB.Remove(keywordCollection[i].Index, keywordCollection[i].Length);
            coloredSB.Insert(keywordCollection[i].Index, @"<color=#fb4934ff>" + keywordCollection[i].Value + "</color>");
            keywordCollection = keywordRx.Matches(coloredSB.ToString());
        }

        //We always use the first match from functionCollection because the color markup breaks the regex
        MatchCollection functionCollection = functionRx.Matches(coloredSB.ToString());
        count = functionCollection.Count;
        for(int i = 0; i < count; i++){
            coloredSB.Remove(functionCollection[0].Index, functionCollection[0].Length);
            coloredSB.Insert(functionCollection[0].Index, @"<color=#fabd2fff>" +
                functionCollection[0].Value.Substring(0, functionCollection[0].Length - 1) + "</color>(");
            functionCollection = functionRx.Matches(coloredSB.ToString());
        }

        MatchCollection numCollection = numRx.Matches(coloredSB.ToString());
        count = numCollection.Count;
        for(int i = 0; i < count; i++){
            coloredSB.Remove(numCollection[i].Index, numCollection[i].Length);
            coloredSB.Insert(numCollection[i].Index, @"<color=#d3869bff>" + numCollection[i].Value + "</color>");
            numCollection = numRx.Matches(coloredSB.ToString());
        }
        MatchCollection commentCollection = commentRx.Matches(coloredSB.ToString());
        count = commentCollection.Count;
        for(int i = 0; i < count; i++){
            coloredSB.Remove(commentCollection[i].Index, commentCollection[i].Length);
            coloredSB.Insert(commentCollection[i].Index, @"<color=#928374ff>" + commentCollection[i].Value + "</color>");
            commentCollection = commentRx.Matches(coloredSB.ToString());
        }



        return (pythonString, coloredSB.ToString(), textEditor.caretPosition);
    }
}
