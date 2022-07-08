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

    /*
     * Author: Daniel Rittrich 
     * Description: this function is a connection to other scripts 
     * Parameter: -
     * Return: -
    */
    public void Start() {
        // Daniel - 20.06.2022 - Zugriff auf Variablen aus anderem Script
        MyScore = GameObject.Find("Scorefield").GetComponent<scoreController>();
        MyPauseMenu = GameObject.Find("Pause - Menu - Manager").GetComponent<PauseMenu>();
        MySnake = GameObject.Find("Snake").GetComponent<Snake>();
        MyPlayerName = GameObject.Find("Name Input Manager - Startscreen").GetComponent<NameInputPlayerOne>();
    }

    /*
     * Author: Daniel Rittrich 
     * Description: checks if gameover-bool where setted to true
     * Parameter: isGameOver (bool) from Snake.cs
     * Return: -
    */
    void Update() {
        if (MySnake.isGameOver) {
            GameOverStopGame();
        }
    }

    /*
     * Author: Daniel Rittrich 
     * Description: Stops the game. Read the actual score. Show gameover-window.
     * Parameter: score (string) from getScorefield() function from scoreController.cs
     * Return: -
    */
    public void GameOverStopGame() {
        Time.timeScale = 0f;
        MyPauseMenu.gamePaused = true;
        MyPauseMenu.gameStarted = false;
        MyPauseMenu.buttonPauseMenu.SetActive(false);
        if (MyScore.getScorefield() == "0") {
            gameOverText.text = "Ups! " + MyPlayerName.namePlayerOne + "\nDu hast " + MyScore.getScorefield() + " Punkte erzielt";
        }
        else {
            gameOverText.text = "Wow " + MyPlayerName.namePlayerOne + " !!!\nDu hast " + MyScore.getScorefield() + " Punkte erzielt";
        }
        gameOverWindow.SetActive(true);
    }

    /*
     * Author: Daniel Rittrich 
     * Description: deletes old visible snake-bodyparts of dead snake
     * Parameter: -
     * Return: -
    */
    public void DestroyOldBodyParts() {
        GameObject[] bodyPartsToDestroy = GameObject.FindGameObjectsWithTag("SnakeBodyPart");
        for (int i = 0; i < bodyPartsToDestroy.Length; i++) {
            GameObject.Destroy(bodyPartsToDestroy[i]);
        }
    }

    /*
     * Author: Daniel Rittrich 
     * Description: Sets all settings to default and starts a new game. 
     * Parameter: -
     * Return: -
    */
    public void StartNewGame() {
        MySnake.isGameOver = false;
        DestroyOldBodyParts();
        MyScore.setScorefield(0, 0);                        
        MyScore.setRunRefreshHighscoreList(true);          
        gameOverWindow.SetActive(false);
        MyPauseMenu.buttonPauseMenu.SetActive(true);
        MyPauseMenu.gamePaused = false;
        MyPauseMenu.gameStarted = true;
        MySnake.Awake(); // set settings to default
        Time.timeScale = 1f;


        // For debugging a list
        /*
        string result = "List contents before: ";
        foreach (var item in MySnake.snakeBodyPartList) {
            result += item.ToString() + ", ";
        }
        Debug.Log(result);

        string result2 = "List contents after: ";
        foreach (var item in MySnake.snakeBodyPartList) {
            result2 += item.ToString() + ", ";
        }
        Debug.Log(result2);
        */
    }

}
