/******************************************************************************
Name:           GameOver
Description:    The script manages the gameover-window and the options to close
                the game or start e new game. Also it resets the all stats that
                be needed to begin a new game.
Author(s):      Daniel Rittrich
Date:           2022-06-20
Version:        V1.0 
TODO:           - check if functions works well
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour {

    public GameObject gameOverWindow;
    public TextMeshProUGUI gameOverText;
    scoreController myScore;
    PauseMenu myPauseMenu;
    Snake mySnake;
    NameInputPlayerOne myPlayerName;

    public scoreController MyScore { get => myScore; set => myScore = value; }
    public PauseMenu MyPauseMenu { get => myPauseMenu; set => myPauseMenu = value; }
    public Snake MySnake { get => mySnake; set => mySnake = value; }
    public NameInputPlayerOne MyPlayerName { get => myPlayerName; set => myPlayerName = value; }

    public void Start() {
        // Daniel - 20.06.2022 - Zugriff auf Variablen aus anderem Script
        MyScore = GameObject.Find("Text (TMP)").GetComponent<scoreController>();
        MyPauseMenu = GameObject.Find("Pause - Menu - Manager").GetComponent<PauseMenu>();
        MySnake = GameObject.Find("Snake").GetComponent<Snake>();
        MyPlayerName = GameObject.Find("Name Input Manager - Startscreen").GetComponent<NameInputPlayerOne>();
    }

    void Update() {
        if (MySnake.isGameOver) {
            GameOverStopGame();
        }
    }

    public void GameOverStopGame() {
        Time.timeScale = 0f;
        MyPauseMenu.gamePaused = true;
        MyPauseMenu.gameStarted = false;
        gameOverText.text = "Glückwunsch " + MyPlayerName.namePlayerOne + " !!!\nDu hast " + MyScore.getScorefield() + " Punkte erzielt";
        gameOverWindow.SetActive(true);
    }

    public void StartNewGame() {
        MySnake.snakeBodySize = 0;                        //SNAKE KOERPER ZURUECK SETZEN
        MySnake.snakeBodyPartList.Clear();                //SNAKE BODYPARTLIST ZURUECK SETZEN ???
        MySnake.snakeMovePositionList.Clear();            //SNAKE MOVEPOSITIONLIST ZURUECK SETZEN ???
        MySnake.gridPosition = new Vector2Int(100, 100);    //SNAKE POSITION ZURUECK SETZEN ???
        MySnake.gridMoveDirection = new Vector2Int(0, 50);    //SNAKE MOVEDIRECTION ZURUECK SETZEN ???
        MyScore.setScorefield("0");                       //SCORE ZURUECK SETZEN ???
        //TIMER FUER FUTTER ZURUECK SETZEN ??? Oder ist das nicht noetig
        //FUTTER DAS HERUM LIEGT LOESCHEN ??? Oder ist das nicht noetig
        gameOverWindow.SetActive(false);
        MyPauseMenu.gamePaused = false;
        MyPauseMenu.gameStarted = true;
        MySnake.isGameOver = false;
        Time.timeScale = 1f;
    }

}
