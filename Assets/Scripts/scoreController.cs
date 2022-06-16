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
using System.Linq;
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

    [SerializeField] string debugSetActualScore;
    [SerializeField] bool deleteAllHighscoreData;

    // Start is called before the first frame update
    void Start() {
        //reference to snake object
        snake = GameObject.Find("Snake").GetComponent<Snake>();
        scorefield = GetComponent<TextMeshProUGUI>();
        setScorefield("0");

        //set keyNames with startvalues if the are not exists
        bool initDataDone = initializePlayerprefKeys();
        //copy playerprefs to the textfields
        bool copyDone = copyPlayerprefsToTextfields(true, 0);
        
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
        //read playerscore only if the index is within the range of the array (0-4 highscores 5 actualScore)
        if (inRangeOfInt(index, 0, 5)) {
            returnValue = highscoreAsInt[index];
        }

        return  returnValue;
    }
    public bool setHighscoreAsInt(bool all, int index, int value) {
        //convert the textfield string to integer and save it in an array

        //int returnValue = 0;
        if (all) {   //initialize array
            highscoreAsInt[0] = Int32.Parse(highscoreValue0.text);
            highscoreAsInt[1] = Int32.Parse(highscoreValue1.text);
            highscoreAsInt[2] = Int32.Parse(highscoreValue2.text);
            highscoreAsInt[3] = Int32.Parse(highscoreValue3.text);
            highscoreAsInt[4] = Int32.Parse(highscoreValue4.text);
            highscoreAsInt[5] = 0; //needed for bubblesort
            return true;
        }
        //write single index only if the index is within the range of the array (0-5)
        if (!all && inRangeOfInt(index, 0, 5)) {
            highscoreAsInt[index] = value;
            return true;
        }

        return false;
    }
    


    //copy the playerprefs to the textfields
    private bool copyPlayerprefsToTextfields(bool all, byte index) {
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
            log("All playerprefs are copied to the textfields!");

            return true;
        } else {
            switch (index) {
                case 0:
                    highscoreName0.text = PlayerPrefs.GetString("highscoreName0");
                    highscoreValue0.text = PlayerPrefs.GetString("highscoreValue0");
                    break;
                case 1:
                    highscoreName1.text = PlayerPrefs.GetString("highscoreName1");
                    highscoreValue1.text = PlayerPrefs.GetString("highscoreValue1");
                    break;
                case 2:
                    highscoreName2.text = PlayerPrefs.GetString("highscoreName2");
                    highscoreValue3.text = PlayerPrefs.GetString("highscoreValue2");
                    break;
                case 3:
                    highscoreName3.text = PlayerPrefs.GetString("highscoreName3");
                    highscoreValue3.text = PlayerPrefs.GetString("highscoreValue3");
                    break;
                case 4:
                    highscoreName4.text = PlayerPrefs.GetString("highscoreName4");
                    highscoreValue4.text = PlayerPrefs.GetString("highscoreValue4");
                    break;
            }
            log("Playerpref with the index " + index + " was copied to the textfield!");

            return true;
        }

        return false;
    }
    
    //convert the highscoreAsInt to string and copy to the playerprefs 
    private void copyHighscoreToPlayerPrefs(bool all, int index) {
        int actualScore;
        string keyname;
        if (all) {
            for (int internalIndex = 0; internalIndex < 5; internalIndex++) {
                actualScore = getHighscoreAsInt(internalIndex);
                keyname = "highscoreValue" + internalIndex;
                PlayerPrefs.SetString(keyname, actualScore.ToString());
                log("All highscores are copied to the playerprefs!");
            }
        }
        if (!all && inRangeOfInt(index, 0, 4)) {
            actualScore = getHighscoreAsInt(index);
            keyname = "highscoreValue" + index;
            PlayerPrefs.SetString(keyname, actualScore.ToString());
            log("The highscore with the index " + index + " was copied to the playerpref!");
        }
        PlayerPrefs.Save();
    }

    //check if the given value (integer) is within a range 
    private bool inRangeOfInt(int value, int lowBound, int highBound) {
        if ((value >= lowBound) && (value <= highBound)) {
            return true;
        }

        return false;
    }

    //initialize highscoredata (set default values)
    private bool initializePlayerprefKeys() {
        //initialize highscorelist
        //set keynames for Playerprefs
        //check only the 1st place because if there was a first game, the score will be the 1st place
        if (PlayerPrefs.GetString("highscoreName0") == "") {
            for (byte index = 0; index < 5; index++) {
                PlayerPrefs.SetString("highscoreName" + index , "Wer wird Platz " + (index + 1) + " belegen?!");
                PlayerPrefs.SetString("highscoreValue" + index , "0");
                if (index == 4) {
                    log("All playerpref-keys are initialized!");
                    return true;
                }
            }
        }
        
        return false;
    }

    //delete playerprefs highscore and name (from PlayerPrefs-file AND the highscoreAsInt!)
    private bool deleteHighscoreData(bool all, int index) {
        bool done = false;
        bool setHighscoreDone = false;
        //delete all
        if (all) {
            for (int internalIndex = 0; internalIndex < 5; internalIndex++) {
                PlayerPrefs.DeleteKey("highscoreName" + internalIndex);
                PlayerPrefs.DeleteKey("highscoreValue" + internalIndex);
                setHighscoreDone = setHighscoreAsInt(false, internalIndex, 0);
                if ((internalIndex == 4) && setHighscoreDone) {
                    done = true;
                    log("All data are deleted!!");
                }
            }
        }
       
       //delte the name and score additional to the given index
        if (!all && inRangeOfInt(index, 0, 4)) {
            PlayerPrefs.DeleteKey("highscoreName" + index);
            PlayerPrefs.DeleteKey("highscoreValue" + index);
            done = true;
            log("Data with the index " + index + " was deleted!");
        }
       
        //refresh (copy) textfields in the highscore window
        if (done) {
            PlayerPrefs.Save();
            initializePlayerprefKeys();
            copyPlayerprefsToTextfields(true, 0);
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
        int tempMemoryScore;
        bool goToLastPart = false;
        int factor;
        string tempMemoryName;
        //set actual score to int array and name to playerpref 6
        int.TryParse(getScorefield(), out actualScore);
        setHighscoreAsInt(false, 5, actualScore);
        PlayerPrefs.SetString("highscoreName5", snakePlayerName);
        

        //do nothing with the score if it is lower than the one from the 5th place 
        /*if (actualScore < getHighscoreAsInt(4)) {
            //quitt
            return true;
        }*/
    
        //bubblesort: sort the actual score into the highscorelist
        for (byte sortCycle = 0; sortCycle <= 4; sortCycle++)
            {
                for (int index = 0; index <= 4; index++)
                {
                    if (getHighscoreAsInt(index) < getHighscoreAsInt(index + 1))
                    {
                        tempMemoryScore = getHighscoreAsInt(index + 1);
                        tempMemoryName = PlayerPrefs.GetString("highscoreName" + (index + 1));
                        setHighscoreAsInt(false, index + 1, getHighscoreAsInt(index));
                        setHighscoreAsInt(false, index, tempMemoryScore);

                        PlayerPrefs.SetString("highscoreName" + (index + 1), 
                            PlayerPrefs.GetString("highscoreName" + index));
                        PlayerPrefs.SetString("highscoreName" + index, tempMemoryName);
                    }
                } 
                if (sortCycle == 4) {
                    //overwrite the placenumber from the initial-startvalue
                    //"Wer wird PLatz >x< belegen?!"
                    int position = 0;
                    //find the first initial-startvalue (exit when it not exist)
                    for (int index = 0; index <  5; index++) {
                        if (PlayerPrefs.GetString("highscoreName" + index).Contains('1')) {
                            position = index;
                            log("position: " + position);
                            break;
                        }
                        goToLastPart = ((position == 0) && index == 5);
                        log("lastPart: " + goToLastPart);
                    }
                    if (!goToLastPart) {
                        /*switch (position) {
                            case 1: //2nd place
                                factor = 1;
                                break;
                            case 2: //3rd place
                                factor = 2;
                                break;
                            case 3: //4th place
                                factor = 3;
                                break;
                            case 4: //5th place
                                factor = 4;
                                break;
                        }  */ 
                        int zahl = 0;
                        for (int index = position; index <= (5 - position); index++) {
                            int targetInt = zahl + index;
                            char targetChar = Convert.ToChar(targetInt);
                            log("target: " + targetChar);
                            int changeInt = index + 1;
                            char changeChar = Convert.ToChar(changeInt);
                            log("change: " + changeChar);

                            PlayerPrefs.GetString("highscoreName" + index).Replace( targetChar, changeChar);
                        }
                    }
                    
                    
                    
                    copyHighscoreToPlayerPrefs(true, 0);
                    copyPlayerprefsToTextfields(true, 0);

                    return true;
                }
            }

        return false;
    }

    //debug stuff
    //main debug
    private void debugArea() {
        log("DEBUG-MODE ACTIVE!!!");
        
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
    private void debugWritePlayerpref(string keyname, string value) {
        PlayerPrefs.SetString(keyname , value);
    }
    //log shorthand
    private void log(string message) {
        UnityEngine.Debug.Log(message);
    }


    // Update is called once per frame
    void Update()
    {
        //calculate and update scorefield
        setScorefield(calculate(snake.getLenght(), 1, 1, 1).ToString());

        //gamover = refresh highscorelist
        if (gameover) {
            setScorefield(debugSetActualScore);
            bool done = refreshHighscoreList();

            if (done) {
                gameover = false;
            }
        }

        //call: delte all highscore data
        if (deleteAllHighscoreData) {
            if (deleteHighscoreData(true, 0)) {
                deleteAllHighscoreData = false;
            }
        }

        //call: debugArea
        if (debugModeOn) {
            debugArea();
       }   
    }
}
