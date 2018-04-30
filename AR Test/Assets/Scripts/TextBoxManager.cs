using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    public TextAsset textFile;
    public string[] textLines;

    public GameObject textBox;
    public Text text;

    public int currentLine, endLine;

    public bool isActive = false;

    private void Start()
    {
        DisableTextBox();

        // Make sure that we have a text file loaded to avoid unncessary errors
        if (textFile != null)
        {
            // Read through the text file spliting the file into a new element in the array at each new line.
            textLines = textFile.text.Split('\n');
        }

        // Make sure we don't just finish as soon as the file is opened.
        if (endLine == 0)
        {
            endLine = textLines.Length - 1;
        }

        // Decide if the textbox should be active or not
        if (isActive)
            EnableTextBox();
        else
            DisableTextBox();
    }

    private void Update()
    {

        if (!isActive)
            return;

        text.text = textLines[currentLine];

        if (Input.GetMouseButtonDown(0))
        {
            currentLine++;
        }

        if (currentLine > endLine)
        {
            DisableTextBox();
        }
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
    }

    public void LoadTextFile(TextAsset text)
    {
        if (text != null)
        {
            textLines = new string[1];
            textLines = text.text.Split('\n');
        }
    }
}
