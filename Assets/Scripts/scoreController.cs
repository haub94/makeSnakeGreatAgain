/******************************************************************************
Name:          scoreCalculator
Description:   The script calculates the highscore depending on the lenght of
               the snake and playtime.
Author(s):     Markus Haubold
Date:          2022-05-31
Version:       V1.0 
TODO:          - scorefield genauso wie playername (public)
               - private array, in welches die public textdaten eingepflegt werden
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreController : MonoBehaviour
{
    Snake snake;

    //Highscore list output 
    //names
    public TextMeshProUGUI playername1;
    public TextMeshProUGUI playername2;
    public TextMeshProUGUI playername3;
    public TextMeshProUGUI playername4;
    public TextMeshProUGUI playername5;
    //scores
    public TextMeshProUGUI playerscore1;
    public TextMeshProUGUI playerscore2;
    public TextMeshProUGUI playerscore3;
    public TextMeshProUGUI playerscore4;
    public TextMeshProUGUI playerscore5;
    
    //scorefield gamefield
    private TextMeshProUGUI scorefield;
    private int[] playerscoreAsInt = new int[6];
    //switch on for debug stuff
    private const bool debugModeOn = true;


    // Start is called before the first frame update
    void Start() {
        //reference to snake object
        snake = GameObject.Find("Snake").GetComponent<Snake>();
        scorefield = GetComponent<TextMeshProUGUI>();
        setScorefield("0");

        //initialize highscorelist
        //the very first run of the game => set keynames for Playerprefs
        if (PlayerPrefs.GetString("playername1") == "") {
            String keynameName;
            String keynameScore;
            for (int index = 1; index < 6; index++) {
                keynameName = "playername" + index;
                keynameScore = "playerscore" + index;
                PlayerPrefs.SetString(keynameName , "Wer wird Platz " + index + " belegen?!");
                PlayerPrefs.SetString(keynameScore , "123");
            }
        }
        //read values from playerprefs
        playername1.text = PlayerPrefs.GetString("playername1");
        playerscore1.text = PlayerPrefs.GetString("playerscore1");
        playername2.text = PlayerPrefs.GetString("playername2");
        playerscore2.text = PlayerPrefs.GetString("playerscore2");
        playername3.text = PlayerPrefs.GetString("playername3");
        playerscore3.text = PlayerPrefs.GetString("playerscore3");
        playername4.text = PlayerPrefs.GetString("playername4");
        playerscore4.text = PlayerPrefs.GetString("playerscore4");
        playername5.text = PlayerPrefs.GetString("playername5");
        playerscore5.text = PlayerPrefs.GetString("playerscore5");

        //initilize array from setPlayerscoreAsInt
        
    }

    //getter and setter
    //scorefield
    public string getScorefield() {
        string text = "";

        return text;
    }
    public void setScorefield(string value) {
        scorefield.text = value;
    }
    //playerscoreAsInt
    public int getPlayerscoreAsInt(int index) {
        int returnValue = 0;
        returnValue = playerscoreAsInt[index];
        
        return  returnValue;
    }
    public void setPlayerscoreAsInt(int index, int value, bool initialize) {
        UnityEngine.Debug.Log("converter" + initialize);
        //int returnValue = 0;
        if (initialize) {   //initialize array
            playerscoreAsInt[1] = Int32.Parse(playerscore1.text);
            /*playerscoreAsInt[2] = Int32.Parse(playerscore2.text);
            playerscoreAsInt[3] = Int32.Parse(playerscore3.text);
            playerscoreAsInt[4] = Int32.Parse(playerscore4.text);
            playerscoreAsInt[5] = Int32.Parse(playerscore5.text);*/
            

            //debug
            if (debugModeOn) {
                UnityEngine.Debug.Log("INIT DONE: " + 
                playerscoreAsInt[1] + " | " +
                playerscoreAsInt[2] + " | " +
                playerscoreAsInt[3] + " | " +
                playerscoreAsInt[4] + " | " +
                playerscoreAsInt[5]);
                initialize = false;
            }
        } else {
            playerscoreAsInt[index] = value;
        }
    }
    
    
    //calculate the actual score (depends on snakelenght)
    private double calculate(int lenght, float playtime, int bonusFactor1, int bonusFactor2) {
        //function parameters
        double score = 0;
        const double a = 3.0;
        const double k = 0.2;
        const double c = -4;
        const double d = 0;
        int x = lenght;

        if (x == 0) {
            score = 0;
        } else
        {
            //function= (a*e^k*(x-(c)))+d with x=snakelenght
            score = Math.Round(((a * Math.Exp(k*(x - (c)))) + d) * 100); 
            // + (funktion playtime) + (function bF1) + (function bF2)    
        }
        
        return score;
    }

    //wall of fame aka top five highscores
    public bool refreshHighscoreList() {
        
        


        return true;
    }

    //write Playerprefs for debug
    private void debugWritePlayerpref(String keyname, String value) {
        PlayerPrefs.SetString(keyname , value);
    }

    // Update is called once per frame
    void Update()
    {
        //calculate and update scorefield
        setScorefield(calculate(snake.getLenght(), 1, 1, 1).ToString());
        
        debugWritePlayerpref("playername1", "debugPlayername");
        debugWritePlayerpref("playerscore1", "123456");

        setPlayerscoreAsInt(0, 0, true);
        UnityEngine.Debug.Log("playerscore: " + getPlayerscoreAsInt(1));

    }
}
