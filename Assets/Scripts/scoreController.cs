/**********************************************************************************************************************
Name:          scoreCalculator
Description:   The script calculates the highscore depending on the lenght of
               the snake.
               read data: PlayerPrefs <-- Textfield <-- highscoreAsInt
Author(s):     Markus Haubold
Date:          2022-05-31
Version:       V1.0 
TODO:          - scorefield genauso wie playername (public)
               - private array, in welches die public textdaten eingepflegt werden
               - spielername aus Snake.cs ankoppeln 
               - run setScorefield() only if the snake.getLength() has changed!
               - add error function (message + gamecontrol)
**********************************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class scoreController : MonoBehaviour
{
    //create object from class snake
    Snake snake;

    //Highscore list output 
    //names
    [SerializeField] List<TextMeshProUGUI> highscoreName = new List<TextMeshProUGUI>();
    //scores
    [SerializeField] List<TextMeshProUGUI> highscoreValue = new List<TextMeshProUGUI>();
    
    //scorefield gamefield
    private TextMeshProUGUI scorefield;
    private int[] highscoreAsInt = new int[6]; //value max highscores in list +1 for actual score (see bubblesort)
    //switch on for debug stuff
    private const bool debugModeOn = true;
    public string snakePlayerName = "spielerName"; //Platzhalter
    public bool gameover;   //PLatzhalter
    public bool test1;

    [SerializeField] string debugSetActualScore;
    [SerializeField] bool deleteAllHighscoreData;

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
        /*
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
*/
        return false;
    }

    //deleteAllHighscoreData
    public bool setDeleteAllHighscoreData(bool value) {
        //if there will be a popup in the future with the final question for the delet -> implement 
        //the logic here!
        deleteAllHighscoreData = value;

        return true;
    }    
    public bool getDeleteAllHighscoreData() {
        
        return deleteAllHighscoreData;
    }

    //textfield highscoreName
    public void setHighscoreName(int index, string value) {
        highscoreName[index].text = value;    
    }
    public string getHighscoreName(int index) {
        return highscoreName[index].text;
    }
    
    //textfield highscoreValue
    public void setHighscoreValue(int index, string value) {
        highscoreValue[index].text = value;    
    }
    public string getHighscoreValue(int index) {
        return highscoreValue[index].text;
    }

    

    // Start is called before the first frame update
    void Start() {
        //reference to snake object (to get the length from it)
        snake = GameObject.Find("Snake").GetComponent<Snake>();
        scorefield = GetComponent<TextMeshProUGUI>();
        setScorefield("0");

        //set keyNames with startvalues if the are not exists
        bool initDataDone = initializePlayerprefKeys();
        //copy playerprefs to the textfields
        bool copyDone = copyPlayerprefsToTextfields();
        //copy score from textfield to integer array
        bool integerDone = setHighscoreAsInt(true, 0, 0);
        
    }



    //shorthand Playerprefs highscoreValue
    private void setPpValue(int index, string value) {
        if (inRangeOfInt(index, 0, 5)) {
            PlayerPrefs.SetString("highscoreValue" + index, value);
        } else {
            log("Error in setPpValue: Index " + index + " out of range!");
        }
    }
    private string getPpValue(int index) {
        string returnValue = "Error in getPpValue: Index " + index + " out of range!";
        if (inRangeOfInt(index, 0, 5)) {
            returnValue = PlayerPrefs.GetString("highscoreValue" + index);
        } else {
            log(returnValue);
        }
    
        return returnValue;
    }
    //shorthand Playerprefs highscoreName
     private void setPpName(int index, string value) {
        if (inRangeOfInt(index, 0, 5)) {
            PlayerPrefs.SetString("highscoreName" + index, value);
        } else {
            log("Error in setPpName: Index " + index + " out of range!");
        }
    }
    private string getPpName(int index) {
        string returnName = "Error in getPpName: Index " + index + " out of range!";
        if (inRangeOfInt(index, 0, 5)) {
            returnName = PlayerPrefs.GetString("highscoreName" + index);
        } else {
            log(returnName);
        }
    
        return returnName;
    }

    
     //initialize highscoredata (set default values)
    private bool initializePlayerprefKeys() {
        //initialize highscorelist
        //set keynames for Playerprefs
        //check only the 1st place because if there was a first game, the score will be the 1st place
        if (getPpName(0) == "") {
            for (byte index = 0; index <= 5; index++) {
                setPpName(index, "Wer wird Platz " + (index + 1) + " belegen?!");
                setPpValue(index, "0");
                if (index == 4) {
                    PlayerPrefs.Save();
                    log("All playerpref-keys are initialized!");
                    
                    return true;
                }
            }
        }
        
        return false;
    }
    
    //copy the playerprefs to the textfields
    private bool copyPlayerprefsToTextfields() {
        for (int index = 0; index <= 4; index++) {
            setHighscoreName(index, getPpName(index));
            setHighscoreValue(index, getPpValue(index));
            if (index == 4) {
                log("All playerprefs are copied to the textfields!");
                //print the data in a log for debugging
                if (debugModeOn) {
                    log("Function copyPlayerPrefsToTextfields says:");
                    for (int debugIndex = 0; debugIndex <= 4; debugIndex++) {
                        log("Textfield" + debugIndex + ": " + getHighscoreName(debugIndex) + " with value: " + 
                            getHighscoreValue(debugIndex));
                    }
                }
            }
        } 
       
       /*
        if (!all && inRangeOfInt(index, 0, 4)) {
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
        */
        return false;
    }

    //delete playerprefs highscore and name (from PlayerPrefs-file AND the highscoreAsInt!)
    private bool deleteHighscoreData(bool all, int index) {
        bool done = false;
        //delete all
        if (all) {
            for (int internalIndex = 0; internalIndex <= 5; internalIndex++) {
                //safe data for log
                string tempName = getPpName(internalIndex);
                string tempValue = getPpValue(internalIndex);
                //delete the PLayerPref name and value
                PlayerPrefs.DeleteKey("highscoreName" + internalIndex);
                PlayerPrefs.DeleteKey("highscoreValue" + internalIndex);
                //print the deleted data if debug mode is on 
                if (debugModeOn) {
                    log("Deleted the highscorename" + tempName + " with the value: " + tempValue + "!");
                }
                    
                if (internalIndex == 4) {
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
            copyPlayerprefsToTextfields();
            
            return true;
        }

        return false;
    }

    //check if the given value (integer) is within a range 
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
        //fill PlayerPref5 with the actual data (needed for bubblesort)
        setPpName(5, snakePlayerName);
        setPpValue(5, getScorefield());
        

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
                    string actualPpValue = getPpValue(index);
                    string actualPpName = getPpName(index);
                    string nextPpValue = getPpValue(index + 1);
                    string nextPpName = getPpName(index + 1);
                    string tempMemoryScore;
                    string tempMemoryName;

                    //convert string to int the compare the values
                    int actualPpValueAsInt = 0;
                    int nextPpValueAsInt = 0;
                    int.TryParse(actualPpValue, out actualPpValueAsInt);
                    int.TryParse(nextPpValue, out nextPpValueAsInt);
                    

                    if (actualPpValueAsInt < nextPpValueAsInt) {
                        tempMemoryScore = nextPpValue;
                        tempMemoryName = nextPpName;
                        //sort PlayerPrefs-score
                        setPpValue((index + 1), actualPpValue);
                        setPpValue(index, tempMemoryScore);
                        //sort PlayerPrefs-name
                        setPpName((index + 1), actualPpName);
                        setPpName(index, tempMemoryName);
                    }
                } 
                //cleanup and copy values
                if (sortCycle == 4) {
                    //overwrite startvalues becaus their values are not ok after bubblesort 
                    //check every place
                    for (int place = 0; place <=4; place++) {
                        string startvalue = getPpName(place);
                        //check every possible number in the startvalue
                        for (int number = 1; number < 5; number++) {
                            if (startvalue.Contains("Wer wird Platz " + number + " belegen?!")) {
                                setPpName(place, "Wer wird Platz " + (place + 1) + " belegen?!");
                                break;
                            }
                        }
                    }

                    //print the sorted data in a log for debugging
                    if (debugModeOn) {
                        log("Function refreshHighscoreList says:");
                        for (int index = 0; index <= 5; index++) {
                            log("Playerpref" + index + ": " + getPpName(index) + " with value: " + getPpValue(index));
                        }
                    }

                    //copy data to playerprefs and playerprefs to the textfields
                    //copyHighscoreToPlayerPrefs(true, 0);
                    copyPlayerprefsToTextfields();

                    return true;
                }
            }

        return false;
    }

    //debug stuff
    //main debug
    private void debugArea() {
        log("DEBUG-MODE ACTIVE!!!");


    }
    //write Playerprefs for debug
    private void debugWritePlayerpref(string keyname, string value) {
        PlayerPrefs.SetString(keyname , value);
    }
    //log shorthand
    private void log(string message) {
        UnityEngine.Debug.Log(message);
    }

    //logs only for debug
    private void debugLog(string message) {
        if (debugModeOn) {
            UnityEngine.Debug.Log(message);
        }
    }


    // Update is called once per frame
    void Update() {
        //TODO: run setScorefield() only if the snake.getLenght() has changed!
        //calculate and update scorefield
        setScorefield(calculate(snake.getLength(), 1, 1, 1).ToString());

        //gamover = refresh highscorelist
        if (gameover) {
            if (debugModeOn) {
                setScorefield(debugSetActualScore);
            }
            bool done = refreshHighscoreList();

            if (done) {
                gameover = false;
            }
        }

        //if the button exists in the UI
        /*if (PLACEHOLDER_BUTTON) {
            setDeleteAllHighscoreData(true);
            PLACEHOLDER_BUTTON = false;
        }*/

        //call: delte all highscore data
        if (getDeleteAllHighscoreData()) {
            if (deleteHighscoreData(true, 0)) {
                setDeleteAllHighscoreData(false);
            }
        }

        //call: debugArea
        if (debugModeOn) {
            debugArea();
       } 
    }
}
