/**********************************************************************************************************************
Name:          scoreCalculator
Description:   The script calculates the highscore depending on the lenght of
               the snake and playtime.
               read data: PlayerPrefs <-- Textfield <-- playerScoreAsInt
Author(s):     Markus Haubold
Date:          2022-05-31
Version:       V1.0 
TODO:          - scorefield genauso wie playername (public)
               - private array, in welches die public textdaten eingepflegt werden
               - spielername aus Snake.cs ankoppeln 
**********************************************************************************************************************/

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
    public TextMeshProUGUI highscoreName0;
    public TextMeshProUGUI highscoreName1;
    public TextMeshProUGUI highscoreName2;
    public TextMeshProUGUI highscoreName3;
    public TextMeshProUGUI highscoreName4;
    //scores
    public TextMeshProUGUI highscoreValue0;
    public TextMeshProUGUI highscoreValue1;
    public TextMeshProUGUI highscoreValue2;
    public TextMeshProUGUI highscoreValue3;
    public TextMeshProUGUI highscoreValue4;
    
    //scorefield gamefield
    private TextMeshProUGUI scorefield;
    private int[] highscoreAsInt = new int[6]; //value max highscores in list +1 for actual score (see bubblesort)
    //switch on for debug stuff
    private const bool debugModeOn = false;
    public string snakePlayerName = "horstiHorst94"; //Platzhalter
    public bool gameover;   //PLatzhalter

    public string testScore;

    // Start is called before the first frame update
    void Start() {
        //reference to snake object
        snake = GameObject.Find("Snake").GetComponent<Snake>();
        scorefield = GetComponent<TextMeshProUGUI>();
        setScorefield("0");

        //initialize highscorelist
        //the very first run of the game => set keynames for Playerprefs
        //check only the 1st place because if there was a first game, the score will be the 1st place
        if (PlayerPrefs.GetString("highscoreName1") == "") {
            for (byte index = 1; index < 6; index++) {
                PlayerPrefs.SetString("highscoreName" + index , "Wer wird Platz " + index + " belegen?!");
                PlayerPrefs.SetString("highscoreValue" + index , "123");
            }
        }
        //values from playerprefs --> playername/score.text --> playername/scoreAsInt
        copyPlayerprefsToTextfields(true, 0);
        setHighscoreAsInt(true, 0, 0);

        //initilize array from setPlayerscoreAsInt
        
    }

    //getter and setter
    //scorefield
    public string getScorefield() {
        string text = scorefield.text;

        return text;
    }
    public void setScorefield(string value) {
        scorefield.text = value;
    }
    //playerscoreAsInt
    public int getHighscoreAsInt(int index) {
        int returnValue = 0;
        //read playerscore only if the index is within the range of the array (0-5)
        if (inRangeOfInt(index, 0, 5)) {
            returnValue = highscoreAsInt[index];
        }

        return  returnValue;
    }
    public void setHighscoreAsInt(bool all, int index, int value) {
        //int returnValue = 0;
        if (all) {   //initialize array
            highscoreAsInt[0] = Int32.Parse(highscoreValue0.text);
            highscoreAsInt[1] = Int32.Parse(highscoreValue1.text);
            highscoreAsInt[2] = Int32.Parse(highscoreValue2.text);
            highscoreAsInt[3] = Int32.Parse(highscoreValue3.text);
            highscoreAsInt[4] = Int32.Parse(highscoreValue4.text);
            highscoreAsInt[5] = 0; //needed for bubblesort
        }
        //write single index only if the index is within the range of the array (0-5)
        if (!all && inRangeOfInt(index, 0, 5)) {
            highscoreAsInt[index] = value;
            log("setHighscore: ", index, value);
        }
    }
    
    
    private void copyPlayerprefsToTextfields(bool all, byte index) {
        if (all) {
            highscoreName0.text = PlayerPrefs.GetString("highscoreName0");
            highscoreValue0.text = PlayerPrefs.GetString("highscoreValue0");
            highscoreName1.text = PlayerPrefs.GetString("highscoreName1");
            highscoreValue1.text = PlayerPrefs.GetString("highscoreValue1");
            highscoreName2.text = PlayerPrefs.GetString("highscoreName2");
            highscoreValue2.text = PlayerPrefs.GetString("highscoreValue2");
            highscoreName3.text = PlayerPrefs.GetString("highscoreName3");
            highscoreValue3.text = PlayerPrefs.GetString("highscoreValue3");
            highscoreName4.text = PlayerPrefs.GetString("highscoreName4");
            highscoreValue4.text = PlayerPrefs.GetString("highscoreValue4");
        } else {
            switch (index) {
                case 1:
                    highscoreName0.text = PlayerPrefs.GetString("highscoreName0");
                    highscoreValue0.text = PlayerPrefs.GetString("highscoreValue0");
                    break;
                case 2:
                    highscoreName1.text = PlayerPrefs.GetString("highscoreName0");
                    highscoreValue1.text = PlayerPrefs.GetString("highscoreValue0");
                    break;
                case 3:
                    highscoreName2.text = PlayerPrefs.GetString("highscoreName0");
                    highscoreValue3.text = PlayerPrefs.GetString("highscoreValue0");
                    break;
                case 4:
                    highscoreName3.text = PlayerPrefs.GetString("highscoreName0");
                    highscoreValue3.text = PlayerPrefs.GetString("highscoreValue0");
                    break;
                case 5:
                    highscoreName4.text = PlayerPrefs.GetString("highscoreName0");
                    highscoreValue4.text = PlayerPrefs.GetString("highscoreValue0");
                    break;
            }
        }
    }

    private void copyHighscoreToPlayerPrefs(bool all, int index) {
        int actualScore;
        string keyname;
        if (all) {
            for (int internalIndex = 0; internalIndex < 5; internalIndex++) {
                actualScore = getHighscoreAsInt(internalIndex);
                keyname = "highscoreValue" + internalIndex;
                PlayerPrefs.SetString(keyname, actualScore.ToString());
            }
        }
        if (!all && inRangeOfInt(index, 0, 4)) {
            actualScore = getHighscoreAsInt(index);
            keyname = "highscoreValue" + index;
            PlayerPrefs.SetString(keyname, actualScore.ToString());
        }
        PlayerPrefs.Save();
    }

    //check if the given value is within a range 
    private bool inRangeOfInt(int value, int lowBound, int highBound) {
        if ((value >= lowBound) && (value <= highBound)) {
            return true;
        }

        return false;
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
        } else {
            //function= (a*e^k*(x-(c)))+d with x=snakelenght
            score = Math.Round(((a * Math.Exp(k*(x - (c)))) + d) * 100); 
            // + (funktion playtime) + (function bF1) + (function bF2)    
        }
        
        return score;
    }

    //refresh / upddate highscore window
    public bool refreshHighscoreList() {
        int actualScore;
        int tempMemory;
        int.TryParse(getScorefield(), out actualScore);
        setHighscoreAsInt(false, 5, actualScore);

        //do nothing with the score if it is lower than the one from the 5th place 
        /*if (actualScore < getHighscoreAsInt(4)) {
            //quitt
            return true;
        }*/
        

        foreach(int aa in highscoreAsInt) {
            UnityEngine.Debug.Log("vorher: " + aa);
        }

        //bubblesort: sort the actual score into the highscorelist
        for (byte sortCycle = 0; sortCycle <= 4; sortCycle++)
            {
                for (int index = 0; index <= 4; index++)
                {
                    if (getHighscoreAsInt(index) < getHighscoreAsInt(index + 1))
                    {
                        tempMemory = getHighscoreAsInt(index + 1);
                        setHighscoreAsInt(false, index + 1, getHighscoreAsInt(index));
                        setHighscoreAsInt(false, index, tempMemory);
                    }
                } 
                if (sortCycle == 4) {
                    copyHighscoreToPlayerPrefs(true, 0);
                    copyPlayerprefsToTextfields(true, 0);
                    
                    foreach(int bb in highscoreAsInt) {
                        UnityEngine.Debug.Log("nachher: " + bb);
                    }

                    return true;
                }
            }

        return false;
    }

    //debug stuff
    //main debug
    private void debugArea() {
        log("DEBUG-MODE ACTIVE!!!", 0, 0);
        
        //write highscores in int array
        setHighscoreAsInt(false, 0, 100);
        setHighscoreAsInt(false, 1, 300);
        setHighscoreAsInt(false, 2, 500);
        setHighscoreAsInt(false, 3, 200);
        setHighscoreAsInt(false, 4, 400);
        setHighscoreAsInt(false, 5, 800); //act score

        foreach(int aa in highscoreAsInt) {
            UnityEngine.Debug.Log("vorher: " + aa);
        }

        bool temp = refreshHighscoreList();

        foreach(int bb in highscoreAsInt) {
            UnityEngine.Debug.Log("nachher: " + bb);
        }

    }
    //write Playerprefs for debug
    private void debugWritePlayerpref(String keyname, String value) {
        PlayerPrefs.SetString(keyname , value);
    }
    //log shorthand
    private void log(String message, int intValue1, int intValue2) {
        UnityEngine.Debug.Log(message + " " + intValue1.ToString() + " " + intValue2.ToString());
    }


    // Update is called once per frame
    void Update()
    {
        //calculate and update scorefield
        setScorefield(calculate(snake.getLenght(), 1, 1, 1).ToString());

        //gamover = refresh highscorelist
        if (gameover) {
            setScorefield(testScore);
            bool done = refreshHighscoreList();

            if (done) {
                gameover = false;
            }
        }



        if (debugModeOn) {
            debugArea();
       }   

    }
}
