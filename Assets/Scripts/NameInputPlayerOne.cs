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

    // Daniel - 20.06.2022 - Buttonclickhandler prueft ob Var fuer Spielernamen leer ist und traegt ggf. neuen Namen ein
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
    }

    public void CloseInputField() {
        inputFieldWindow.SetActive(false);
    }
}
