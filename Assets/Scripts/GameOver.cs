/******************************************************************************
Name:           GameOver
Description:    The script manages the gameover-window and the options to close
                the game or start e new game. Also it resets the all stats that
                be needed to begin a new game.
Author(s):      Daniel Rittrich
Date:           2022-06-20
Version:        V1.0 
TODO:           - lots of stuff
******************************************************************************/

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

    public void Start() { // Daniel - 20.06.2022 - Zugriff auf Variable aus anderem Script
        // myScore = GameObject.Find(" /*GAMEOBJECT*/ ").GetComponent<scoreCalculator>(); 
        // myPauseMenu = GameObject.Find("Pause - Menu - Manager").GetComponent<PauseMenu>();
    }

    void Update() {
        //if (/*GAMEOVER BOOL*/ ) {
        //myPauseMenu.gamePaused = true;
        //myPauseMenu.gameStarted = false;
        //gameOverText.text = "Glueckwunsch. Du hast " + myScore./*HIGHSCORE*/.ToString() + " Punkte erzielt";
        //gameOverWindow.SetActive(true);
        //}
    }

    public void StartNewGame() {
        //SNAKE KOERPER ZURUECK SETZEN
        //SNAKE POSITION ZURUECK SETZEN
        //AKTUELLEN SCORE ZURUECK SETZEN
        //TIMER FUER FUTTER ZURUECK SETZEN
        //FUTTER ZURUECK SETZEN
        //myPauseMenu.gamePaused = false;
        //myPauseMenu.gameStarted = true;
        //gameOverWindow.SetActive(false);
    }
}
