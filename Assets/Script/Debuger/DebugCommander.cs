using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCommander : MonoBehaviour , IDebugCommandListener
{
    private Dropdown dropdown;

    public GameObject DebugObject;
    private IDebugCommandRecver debugRecver;


    private GameObject LogPanel;
    private Text LogText;
    private InputField CommandText;

    void Start()
    {
        this.LogPanel = this.transform.Find("LogPanel").gameObject;
        this.LogText = this.LogPanel.transform.Find("CommandLogText").gameObject.GetComponent<Text>();


        GameObject CommandPanel = this.transform.Find("CommandPanel").gameObject;
        this.CommandText = CommandPanel.transform.Find("CommandInputField").gameObject.GetComponent<InputField>();
    }
	
	void Update()
    {
	}

    public void RunCommand()
    {
        /*
        Transform dropDownObject = this.transform.Find("CommandDropdown");
        this.dropdown = dropDownObject.gameObject.GetComponent<Dropdown>();

        this.debugRecver = DebugObject.GetComponent<IDebugCommandRecver>();

        string commandName = this.dropdown.options[this.dropdown.value].text;
        */
        string commandName = this.CommandText.text.ToLower();

        string[] commandWords = commandName.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

        if (commandWords.Length <= 0)
            return;


        this.debugRecver = DebugObject.GetComponent<IDebugCommandRecver>();
        this.debugRecver.RunDebugCommand( commandWords , this);
        //this.CommandText.ActivateInputField();

        this.CommandText.text = "";

    }

    private string CommandWordsToString( string[] cmds)
    {
        string command = "";

        foreach( string word in cmds)
        {
            command += word + " ";
        }

        command = command.Trim();

        return command;
    }

    public void Error(string[] cmds, string message)
    {
        string command = this.CommandWordsToString(cmds);
        this.LogText.text = command + " ERROR " + message + "\n" + this.LogText.text;
    }

    public void Success(string[] cmds, string message)
    {
        string command = this.CommandWordsToString(cmds);
        this.LogText.text = command + " SUCCESS " + message + "\n" + this.LogText.text;
    }

    public void Success(string[] cmds)
    {
        this.Success(cmds, "");
    }

    public void Message(string message)
    {
        this.LogText.text = message + "\n" + this.LogText.text;
    }


    public void ChangeLogWindow()
    {
        if( this.LogPanel.activeSelf )
            this.LogPanel.SetActive(false);
        else
            this.LogPanel.SetActive(true);
    }


    public void CommandListSelected()
    {
        Transform dropDownObject = this.transform.Find("CommandDropdown");
        this.dropdown = dropDownObject.gameObject.GetComponent<Dropdown>();

        this.debugRecver = DebugObject.GetComponent<IDebugCommandRecver>();

        string commandName = this.dropdown.options[this.dropdown.value].text;

        this.CommandText.text = commandName;

    }


    public void Test()
    {
        Input.GetKeyDown(KeyCode.A);
    }


}
