/******************************************************************************
Name:           PauseMenu
Description:    The script manages the menue-window and all menue-sub-windows.
                Also it manages the option to start the game and pause the 
                game.
Author(s):      Daniel Rittrich
Date:           2022-05-24
Version:        V1.2
TODO:           - 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public Button pauseMenuButton;
    public Button resumeGameButton;
    public Button startSingleplayerButton;
    public Button startMultiplayerButton;
    public GameObject buttonPauseMenu;
    public GameObject buttonAudioOnOff;
    public GameObject pauseMenu;
    public GameObject einstellungsMenu;
    public GameObject anleitungsMenu;
    public GameObject highScoreMenu;
    public GameObject creditsMenu;
    public GameObject startscreenMenu;
    public GameObject nameInputMenu;
    public GameObject pauseMenuButtonEinstellungen;
    public GameObject pauseMenuButtonAnleitung;
    public GameObject pauseMenuButtonHighScores;
    public GameObject pauseMenuButtonCredits;
    public GameObject pauseMenuButtonSpielBeenden;
    public GameObject pauseMenuButtonSpielFortsetzen;
    public GameObject pauseMenuBackgroundLayer;
    public GameObject pauseMenuBanner;
    public GameObject startscreenBGImage;
    public GameObject startscreenTitel;
    public GameObject startscreenButtonEinzelspieler;
    public GameObject startscreenButtonMehrspieler;
    public bool gameStarted;
    public bool gamePaused;

    /*
     * Author: Daniel Rittrich 
     * Description: Set trigger for start and pause. Show menu buttons and windows. Initialize clickevents for buttons.  
     * Parameter: -
     * Return: -
    */
    void Start() {
        gamePaused = true;
        gameStarted = false;
        pauseMenu.SetActive(false);
        startscreenMenu.SetActive(true);
        nameInputMenu.SetActive(true);
        pauseMenuButtonEinstellungen.SetActive(false);
        pauseMenuButtonAnleitung.SetActive(false);
        pauseMenuButtonHighScores.SetActive(false);
        pauseMenuButtonCredits.SetActive(false);
        pauseMenuButtonSpielBeenden.SetActive(false);
        pauseMenuButtonSpielFortsetzen.SetActive(false);
        pauseMenuBackgroundLayer.SetActive(false);
        pauseMenuBanner.SetActive(false);
        startscreenBGImage.SetActive(true);
        startscreenTitel.SetActive(true);
        startscreenButtonEinzelspieler.SetActive(false);
        startscreenButtonMehrspieler.SetActive(false);
        pauseMenuButton.onClick.AddListener(Pause);
        resumeGameButton.onClick.AddListener(Resume);
        startSingleplayerButton.onClick.AddListener(StartGame);
        startMultiplayerButton.onClick.AddListener(StartGame);
    }

    /*
     * Author: Daniel Rittrich 
     * Description: checks for pressed ESC button  
     * Parameter: -
     * Return: -
    */
    void Update() {
        if (gameStarted && Input.GetKeyDown(KeyCode.Escape)) {
            PauseOrResume();
        }
    }

    /*
     * Author: Daniel Rittrich 
     * Description: checks for gamePaused (bool) and pause or resume the game  
     * Parameter: gamePaused (bool)
     * Return: -
    */
    public void PauseOrResume() {
        if (gamePaused) {
            Resume();
        }
        else {
            Pause();
        }
    }

    /*
     * Author: Daniel Rittrich 
     * Description: starts the first game 
     * Parameter: -
     * Return: -
    */
    public void StartGame() {
        gameStarted = true;
        Resume();
    }

    /*
     * Author: Daniel Rittrich 
     * Description: pauses the game and open the pause-menu
     * Parameter: -
     * Return: -
    */
    public void Pause() {
        Time.timeScale = 0f;
        gamePaused = true;
        buttonPauseMenu.SetActive(false);
        buttonAudioOnOff.SetActive(false);
        pauseMenu.SetActive(true);
        pauseMenuButtonSpielFortsetzen.SetActive(true);
        einstellungsMenu.SetActive(false);
        anleitungsMenu.SetActive(false);
        highScoreMenu.SetActive(false);
        creditsMenu.SetActive(false);
        pauseMenuBackgroundLayer.SetActive(true);
        pauseMenuBanner.SetActive(true);
    }

    /*
     * Author: Daniel Rittrich 
     * Description: resume the game and closes the pause-menu 
     * Parameter: -
     * Return: -
    */
    public void Resume() {
        gamePaused = false;
        buttonPauseMenu.SetActive(true);
        buttonAudioOnOff.SetActive(true);
        pauseMenu.SetActive(false);
        einstellungsMenu.SetActive(false);
        anleitungsMenu.SetActive(false);
        highScoreMenu.SetActive(false);
        creditsMenu.SetActive(false);
        startscreenMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    /*
     * Author: Daniel Rittrich 
     * Description: show settings window 
     * Parameter: -
     * Return: -
    */
    public void PressSettingsButton() {
        einstellungsMenu.SetActive(true);
        anleitungsMenu.SetActive(false);
        highScoreMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    /*
     * Author: Daniel Rittrich 
     * Description: show manual window
     * Parameter: -
     * Return: -
    */
    public void PressManualButton() {
        einstellungsMenu.SetActive(false);
        anleitungsMenu.SetActive(true);
        highScoreMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    /*
     * Author: Daniel Rittrich 
     * Description: show highscore window
     * Parameter: -
     * Return: -
    */
    public void PressHighscoreButton() {
        einstellungsMenu.SetActive(false);
        anleitungsMenu.SetActive(false);
        highScoreMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    /*
     * Author: Daniel Rittrich 
     * Description: show credits window
     * Parameter: -
     * Return: -
    */
    public void PressCreditsButton() {
        einstellungsMenu.SetActive(false);
        anleitungsMenu.SetActive(false);
        highScoreMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
}
