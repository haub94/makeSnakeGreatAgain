using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInputPlayer1 : MonoBehaviour {
    public string player1Name;
    public GameObject inputField;
    public Button enterButton;
    public GameObject inputWindow;

    void Start() {
        enterButton.onClick.AddListener(Enter);
    }

    public void Enter() {
        SaveName();
        CloseInputField();
    }

    public void SaveName() {
        player1Name = inputField.GetComponent<Text>().text;
    }

    public void CloseInputField() {
        inputWindow.SetActive(false);
    }
}
