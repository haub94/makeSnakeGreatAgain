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
    public TMP_InputField nameInputFieldTMP;
    public GameObject pauseMenu;
    public GameObject pauseMenuButtonEinstellungen;
    public GameObject pauseMenuButtonAnleitung;
    public GameObject pauseMenuButtonHighScores;
    public GameObject pauseMenuButtonCredits;
    public GameObject pauseMenuButtonSpielBeenden;
    public GameObject pauseMenuButtonSpielFortsetzen;
    public GameObject pauseMenuBackgroundLayer;
    public GameObject pauseMenuBanner;
    public GameObject startscreenMenu;
    public GameObject startscreenBGImage;
    public GameObject startscreenTitel;
    public GameObject startscreenButtonEinzelspieler;
    public GameObject startscreenButtonMehrspieler;
    private scoreController scoreControllerScript; //Haubold: scoreController object

    /*
     * Author: Daniel Rittrich 
     * Description: set the limit of characters for playernameinput  
     * Parameter: -
     * Return: -
    */
    void Awake() {
        nameInputFieldTMP.characterLimit = 12;
        scoreControllerScript = 
            GameObject.Find("Scorefield").GetComponent<scoreController>(); //Haubold: link to  script
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
        scoreControllerScript.setPlayerName(namePlayerOne); //Haubold: call setter from scoreController for playername
    }

    /*
     * Author: Daniel Rittrich 
     * Description: close the inputfield-window and shows the menu
     * Parameter: -
     * Return: -
    */
    public void CloseInputField() {
        inputFieldWindow.SetActive(false);
        pauseMenu.SetActive(true);
        pauseMenuButtonEinstellungen.SetActive(true);
        pauseMenuButtonAnleitung.SetActive(true);
        pauseMenuButtonHighScores.SetActive(true);
        pauseMenuButtonCredits.SetActive(true);
        pauseMenuButtonSpielBeenden.SetActive(true);
        pauseMenuButtonSpielFortsetzen.SetActive(false);
        pauseMenuBackgroundLayer.SetActive(false);
        pauseMenuBanner.SetActive(false);
        startscreenMenu.SetActive(true);
        startscreenBGImage.SetActive(true);
        startscreenTitel.SetActive(true);
        startscreenButtonEinzelspieler.SetActive(true);
        startscreenButtonMehrspieler.SetActive(true);
    }
}
