using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter 
{
    private string inputText;
    private int position;
    private int readPosition;
    private char ch;
    private enum Colors {
        Gray, //Comments
        Red, //Keywords
        Yellow, //Functions
        Green, //Strings
        Blue, //Integers
        White //Everything else
    }

    public Highlighter(string input) {
        inputText = input;
        readPosition = 0;
        ReadChar();
    }

    public (string, string) GetText() {
        var pythonString = "";
        var coloredString = "";
        
        while(true) {
            (string, string) nextWords = GetNextWord();
            if(nextWords == ("\0", "\0")) {
                break;
            }
            pythonString += nextWords.Item1;
            coloredString += nextWords.Item2;
        }

        return (pythonString, coloredString);
    }

    private (string, string) GetNextWord() {
        string pythonWord = "";
        string coloredWord = "";
        string temp;

        pythonWord += SkipWhitespace();

        switch(ch) {
            case '#':
                temp = NextLine();
                pythonWord += temp;
                coloredWord += AddColor(Colors.Gray,temp);
                break;
            default:
            break;
        }

        return(pythonWord, coloredWord);
    }

    private void ReadChar() {
        if(readPosition >= inputText.Length) {
            ch = '\0';
        } else {
            ch = inputText[readPosition];
        }
        position = readPosition;
        readPosition += 1;
    }

    private char PeekChar() {
        if(readPosition >= inputText.Length) {
            return '\0';
        } else {
            return inputText[readPosition];
        }
    }

    private string SkipWhitespace() {
        string skipped = "";
        while(ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r') {
            skipped += ch;
            ReadChar();
        }

        return skipped;
    }

    private string NextLine() {
        int currentPosition = position;
        while(ch != '\n') {
            ReadChar();
        }

        return inputText.Substring(currentPosition, position);
    }

    private void SkipColors() {
        string color = "" + ch;

    }

    private string ReadIdentifier() {
        int currentPosition = position;

        while(IsLetter(ch)) {
            ReadChar();
        }

        return inputText.Substring(currentPosition, position);
    }

    private bool IsLetter(char ch) {
        return 'a' <= ch && ch <= 'z' || 'A' <= ch && ch <= 'Z' || ch == '_';
    }

    private bool IsDigit(char ch) {
        return '0' <= ch && ch <= '9';
    }

    private string AddColor(Colors color, string input) {
        switch(color){
            case Colors.Blue:
                return "<color=\"blue\">" + input;
            case Colors.Gray:
                return "<color=\"gray\">" + input;
            case Colors.Green:
                return "<color=\"green\">" + input;
            case Colors.Red:
                return "<color=\"red\">" + input;
            case Colors.Yellow:
                return "<color=\"yellow\">" + input;
            case Colors.White:
                return "<color=\"white\">" + input;
            default:
                return "<color=\"white\">" + input;
        }
    }
}
