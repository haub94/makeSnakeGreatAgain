using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour {
    public string myScore = "";
    public GameObject gameOverWindow;
    public TextMeshProUGUI gameOverText;
    // ERZIELTEN HIGHSCORE HINZUFUEGEN
    // GAMEOVER BOOL NOCH HINZUFUEGEN

    public void Start() { // NOCH ANPASSEN UND SCRIPT FUER SCORE EINBINDEN
                          // myScore = GameObject.Find("Pause - Menu - Manager").GetComponent<PauseMenu>(); // Daniel - 20.06.2022 - Zugriff auf Variable aus anderem Script
        //myPauseMenu = GameObject.Find("Pause - Menu - Manager").GetComponent<PauseMenu>();

    }

    void Update() {
        //if (/*GAMEOVER*/ ) {
        //myPauseMenu.gamePaused = true;
        //myPauseMenu.gameStarted = false;
        //gameOverText.text = "Glückwunsch. Du hast " + /*HIGHSCORE*/ " Punkte erzielt";
        //gameOverWindow.SetActive(true);
        //}
    }

    public void StartNewGame() {
        //gameOverWindow.SetActive(false);
        //myPauseMenu.gamePaused = false;
        //myPauseMenu.gameStarted = true;
    }
}
