using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameInputPlayerOne : MonoBehaviour {
    public string namePlayerOne;
    public GameObject inputFieldWindow;
    public GameObject inputFieldText;

    public void PressEnter() {
        SetName();
        CloseInputField();
    }

    public void SetName() {
        namePlayerOne = inputFieldText.GetComponent<Text>().text;
    }

    public void CloseInputField() {
        inputFieldWindow.SetActive(false);
    }
}
