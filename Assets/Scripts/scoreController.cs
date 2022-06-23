/**********************************************************************************************************************
Name:          scoreCalculator
Description:   The script calculates the highscore depending on the lenght of
               the snake.
               read data: PlayerPrefs <-- Textfield <-- highscoreAsInt
Author(s):     Markus Haubold
Date:          2022-06-20
Version:       V1.0 
TODO:          - spielername aus Snake.cs ankoppeln 
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
    Snake snake;    //create object from class snake

    [SerializeField] List<TextMeshProUGUI> highscoreName = 
        new List<TextMeshProUGUI>();    //list of textfield for the names in the highscore window                                    
    [SerializeField] List<TextMeshProUGUI> highscoreValue = 
        new List<TextMeshProUGUI>();    //list of textfield for the scores in the highscore window
    private TextMeshProUGUI scorefield; //scorefield in the corner from the playfield (shows actual highscore) 
    private const bool debugModeOn = true;  //switch on for debug stuff
    public string snakePlayerName = "spielerName"; //name of the actual player
    [SerializeField] bool runRefreshHighscoreList;   //trigger: true if the game is over (set from a button in the gameover popup)
    [SerializeField] string debugSetActualScore;    //set an score for debugging
    [SerializeField] bool deleteAllHighscoreData;   //trigger: delete all data from the highscorelist (for ever)
    [SerializeField] bool secondCheckDeleteData = false;
    private List<string> messages = new List<string>();
    //setter and getter
    //scorefield
    public void setScorefield(string value) {
        scorefield.text = value;
    }
    public string getScorefield() {
        string returnValue = scorefield.text;

        return returnValue;
    }

    //deleteAllHighscoreData
    public bool setDeleteAllHighscoreData(bool value) {
        //if there will be a popup in the future with the final question for the delet -> implement 
        //the logic here!
        deleteAllHighscoreData = value;

        return true;
    }    
    public bool getDeleteAllHighscoreData() {
        bool returnValue;
        returnValue = deleteAllHighscoreData;

        return returnValue;
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

    //second check delete all data
    public void setSecondCheckDeleData(bool value) {
        secondCheckDeleteData = value;
    }
    public bool getSecondCheckDeleteData() {
        bool returnValue;
        returnValue = secondCheckDeleteData;

        return returnValue;
    }

    //start the alogrithm to sort the actual score into the highscorelist
    public void setRunRefreshHighscoreList(bool value) {
        runRefreshHighscoreList = value;
    }
    public bool getRunRefreshHighscoreList() {
        bool returnValue;
        returnValue = runRefreshHighscoreList;

        return returnValue;
    }

    public void setMessage() {
       //maybe later it will be possible to change the language -> implement logic here!
       
        messages.Add("Bist du sicher, dass alle Highscoredaten gelöscht werden sollen?");
        messages.Add("Sorry...leider konnte dein Highscore nicht gespeichert werden!"); 
        messages.Add("Du hast es leider nicht unter die Top5 geschafft!"); 
        
    }
    public string getMessage(int index) {
        string returnValue;
        returnValue = messages[index];

        return returnValue;
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
        
        
        setMessage();
    }

    //shorthands Playerprefs highscoreValue
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
    
    //shorthands Playerprefs highscoreName
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

     //initialize playerpref-keys with the default values
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

    //convert a string to an integer
    private int stringToInt(string value) {
        int returnValue;

        try {
            returnValue = int.Parse(value);
        } catch (FormatException e) {
            log("Error in function stringToInt: " + e.Message);
            returnValue = -99; //workaround till the error-handle-function is implemented
        }
        //ToDo: if the error-handle-function is implemented, call it! 

        return returnValue;
    }

    //calculate the actual score (depends on snakelenght)
    private double calculate(int lenght) {
        //function parameters
        double score = 0;
        const double a = 3.0;
        const double k = 0.2;
        const double c = -4;
        int x = lenght;

        if (x == 0) {
            score = 0;
        } else {
            //function= (a*e^k*(x-(c)))+d with x=snakelenght
            score = Math.Round(((a * Math.Exp(k*(x - (c))))) * 100);     
        }
        
        return score;
    }

    //refresh / upddate highscore window
    public bool refreshHighscoreList() {
        const int memoryIndexPp = 5;
         
        //do nothing with the score if it is lower than another one ore equal
        if (stringToInt(getScorefield()) <= stringToInt(getPpValue(4))) {
            debugLog("Score " + getPpValue(memoryIndexPp) + " to low for highscorelist");
            userInformation(getMessage(4));

            return true;
        } else {
             //write the actual player-data in the PlayPrefs because they will needed for bubblesort
            setPpName(memoryIndexPp, snakePlayerName);
            setPpValue(memoryIndexPp, getScorefield());
        } 


        //bubblesort: sort the actual score into the highscorelist
        for (byte sortCycle = 0; sortCycle <= 4; sortCycle++)
            {
                for (int index = 0; index <= 4; index++)
                {
                    //write data from playerprefs with the actual index to variables
                    string actualPpValue = getPpValue(index);
                    string actualPpName = getPpName(index);
                    string nextPpValue = getPpValue(index + 1);
                    string nextPpName = getPpName(index + 1);
                    string tempMemoryScore;
                    string tempMemoryName;

                    //convert string to integer to compare the values
                    int actualPpValueAsInt = stringToInt(actualPpValue);    //return -99 if the parse failed
                    int nextPpValueAsInt = stringToInt(nextPpValue);        //-||-
                    
                    if ((actualPpValueAsInt == -99) || (nextPpValueAsInt == -99)) {
                        log("Error in function refreshHighscoreList: converting string to integer not successful!");
                        userInformation(getMessage(2));
                        //the score will not be saved

                        return true;
                    }
                    //sort the actual and the next value
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
                        debugLog("Function refreshHighscoreList says:");
                        for (int index = 0; index <= 5; index++) {
                            debugLog("Playerpref" + index + ": " + getPpName(index) + " with value: " + getPpValue(index));
                        }
                    }
                    //copy playerprefs to the textfields
                    copyPlayerprefsToTextfields();

                    return true;
                }
            }

        return false;
    }

    //PREPARTION FOR A COMMUNICATION WITH THE USER (UI NEEDS THEREFOR A SCRIPTLINKED TEXTFELD!)
    private void userInformation(string message) {
        //TODO: generate an textfield at the UI and link it!
        debugLog(message);
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
        
        setScorefield(calculate(snake.getLength()).ToString());

        //gamover = refresh highscorelist
        if (getRunRefreshHighscoreList()) {
            if (debugModeOn) {
                setScorefield(debugSetActualScore);
            }
            //wait for finish
            if (refreshHighscoreList()) {
                setRunRefreshHighscoreList(false);
            }
        }

        /* PREPARATION FOR A ACTION TO DELETE ALL THE HIGHSCORE-DATA
            needs a doublecheck mechanism at the UI 
            set with a first button the setDeleteAllHighscoreData(true) and
            confirm it with a second one via setSecondCheckDeletData(true)*/
        //delete all highscore data
        if (getDeleteAllHighscoreData()) {
            //preparation for a second check if the user really want to delete the data
            //but acually it is not planned -> user should know what he do
            if (!getSecondCheckDeleteData()) {
                userInformation(getMessage(0));
            } else {
                //delete all data
                deleteHighscoreData(true, 0);
                log("All highscore data are deleted!");
                //reset variables
                setDeleteAllHighscoreData(false);
                setSecondCheckDeleData(false);
            }
        }

        //call: debugArea
        if (debugModeOn) {
            debugArea();
       } 
    }
}
