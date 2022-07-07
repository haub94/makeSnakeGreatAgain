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

    /*
     * Author: Daniel Rittrich 
     * Description: set the limit of characters for playernameinput  
     * Parameter: -
     * Return: -
    */
    void Awake() {
        nameInputFieldTMP.characterLimit = 12;
    }

    /*
     * Author: Daniel Rittrich 
     * Description: checks if there is a input in playernamefield  
     * Parameter: text (string) from inputfield
     * Return: -
    */
    public void PressEnter() {
        if (nameInputFieldTMP.text == "") {
        }
        else {
            SetName();
            CloseInputField();
        }
    }

    /*
     * Author: Daniel Rittrich 
     * Description: set value (string) as playername
     * Parameter: text (string) from inputfield
     * Return: -
    */
    public void SetName() {
        namePlayerOne = nameInputFieldTMP.text;
    }

    /*
     * Author: Daniel Rittrich 
     * Description: close the inputfield-window
     * Parameter: -
     * Return: -
    */
    public void CloseInputField() {
        inputFieldWindow.SetActive(false);
    }
}
