/******************************************************************************
Name:           NameInputPlayerOne
Description:    The script manages the name-input-window at the start of the 
                application.
Author(s):      Daniel Rittrich
Date:           2022-06-20
Version:        V1.1 
TODO:           - 
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameInputPlayerOne : MonoBehaviour {
    public string namePlayerOne = "";
    public GameObject inputFieldWindow;
    public GameObject inputFieldText;
    public TMP_InputField nameInputFieldTMP;
    private scoreController scoreControllerScript; //Haubold: scoreController object

    void Awake() {
        nameInputFieldTMP.characterLimit = 12; //Daniel - 05.07.22 - set the limit of characters for playernameinput
        scoreControllerScript = 
            GameObject.Find("Scorefield").GetComponent<scoreController>(); //Haubold: link to  script
    }

    // Daniel - 20.06.2022 - Buttonclickhandler checks if there is a input in playernamefield and set this value as playername
    public void PressEnter() {
        if (nameInputFieldTMP.text == "") {
        }
        else {
            SetName();
            CloseInputField();
        }
    }

    public void SetName() {
        namePlayerOne = nameInputFieldTMP.text;
        scoreControllerScript.setPlayerName(namePlayerOne); //Haubold: call setter from scoreController for playername
    }

    public void CloseInputField() {
        inputFieldWindow.SetActive(false);
    }
}
