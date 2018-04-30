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
        // If the textbox isn't currently active, bail out of the update sequence
        if (!isActive)
            return;

        // Update the UI text to display the current line of the text file
        text.text = textLines[currentLine];

        // Move to the next line if the mouse is clicked or phone screen is tapped
        if (Input.GetMouseButtonDown(0))
        {
            currentLine++;
        }

        // If we have reached the end of the file disable the text box
        if (currentLine > endLine)
        {
            DisableTextBox();
        }
    }

    // Functions to enable and disable textbox
    public void EnableTextBox()
    {
        textBox.SetActive(true);
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
    }

    // Function to load in new files depending on what object is currently being shown
    // Was unable to get this to work with Vuforia, works with normal Unity3D.  
    public void LoadTextFile(TextAsset text)
    {
        if (text != null)
        {
            textLines = new string[1];
            textLines = text.text.Split('\n');
        }
    }
}
