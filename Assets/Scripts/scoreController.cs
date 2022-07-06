/**********************************************************************************************************************
Name:          scoreCalculator
Description:   The script calculates the highscore depending on the lenght of
               the snake.
               read data: PlayerPrefs <-- Textfield <-- highscoreAsInt
Author(s):     Markus Haubold
Date:          2022-07-01
Version:       V1.0 
TODO:          - future: add config file to switch the debug mode (not in the script)
               - complete the user interaction
**********************************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class scoreController : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> highscoreName = 
        new List<TextMeshProUGUI>();    //list of textfield for the names in the highscore window                                    
    [SerializeField] List<TextMeshProUGUI> highscoreValue = 
        new List<TextMeshProUGUI>();    //list of textfield for the scores in the highscore window
    private TextMeshProUGUI scorefield; //scorefield in the corner from the playfield (shows actual highscore) 
    private const bool debugModeOn = false;  //switch on for debug stuff //TODO: it would be better to switch it with an
                                             //config file but currently there is no time for the implementation)
    [SerializeField] string playerName; //name of the current player
    [SerializeField] bool runRefreshHighscoreList;   //trigger: true if the game is over (set from a button in the 
                                                     //gameover popup)
    [SerializeField] bool deleteAllHighscoreData;   //trigger: delete all data from the highscorelist (for ever)
    [SerializeField] bool secondCheckDeleteData = false;
    private List<string> messages = new List<string>();
    [SerializeField] bool refreshDone = false;
    
    //setter and getter
    /*
     *Author(s): Haubold Markus;
     *Description: Set the actual playername
     *Parameter value: The value for the playername as string
     *Return: -
    */
    public void setPlayerName(string value) {
        playerName = value;
    }
    
    /*
     *Author(s): Haubold Markus;
     *Description: Get the actual playername
     *Parameter: -
     *Return: The actual playername as string
    */
    public string getPlayerName() {
        string returnValue;
        returnValue = playerName;

        return returnValue;
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Set the state of the refreshing
     *Parameter value: state as bool  (true == done)
     *Return: -
    */
    public void setRefreshDone(bool value) {
        refreshDone = value;
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Get the current state of the refreshing from the highscorelist
     *Parameter: -
     *Return: The current state of the refreshing  as bool (true == done)
    */
    public bool getRefreshDone() {
        bool returnValue;
        returnValue = refreshDone;

        return returnValue; 
    }
    
    /*
     *Author(s): Haubold Markus;
     *Description: Set the current playerscore as string (it is displayed in the Textfield at the playfield)
     *Parameter value: The value for the Scorefield as integer
     *Return: -
    */
    public void setScorefield(int value, int maxLength) {
        log("snakelänge: " + value);
        //set scorefield 0 if the snake has no bodyparts
        if (value <= 0) {
            scorefield.text = "0";
        }
        //if snake has bodyparts from 1 up to maxLength: calculate the score
        if (inRangeOfInt(value, 1, maxLength)) {
            scorefield.text = calculate(value).ToString();
        }
    }
    
    /*
     *Author(s): Haubold Markus;
     *Description: Get the current playerscore as string
     *Parameter: -
     *Return: The actual playerscore as string
    */
    public string getScorefield() {
        string returnValue = scorefield.text;

        return returnValue;
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Set/unset the variable deleteAllHighscoreData to delete all highscore data
     *Parameter value: The state of the variable deleteAllHighscoreData as bool
     *Return: -
    */
    public void setDeleteAllHighscoreData(bool value) {
        //if there will be a popup in the future with the final question for the delet -> implement 
        //the logic here!
        deleteAllHighscoreData = value;

    }    
    
    /*
     *Author(s): Haubold Markus;
     *Description: Get the status of the variable deleteAllHighscoreData to delete all data 
     *Parameter : -
     *Return: The state of the variable deleteAllHighscoreData as bool
    */
    public bool getDeleteAllHighscoreData() {
        bool returnValue;
        returnValue = deleteAllHighscoreData;

        return returnValue;
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Set a name to an position at the highscoreName-list 
     *Parameter index: Set the list-position as integer
     *Parameter value: Set the playername in the list as string
     *Return: -
    */
    public void setHighscoreName(int index, string value) {
        highscoreName[index].text = value;    
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Get a name from the highscoreName list
     *Parameter index: Set the list-position as integer
     *Return: The name from the list-position as string
    */
    public string getHighscoreName(int index) {
        
        return highscoreName[index].text;
    }
    
    /*
     *Author(s): Haubold Markus;
     *Description: Set a score to an position at the highscoreValue-list 
     *Parameter index: Set the list-position as integer
     *Parameter value: Set the playerscore in the list as string
     *Return: -
    */
    public void setHighscoreValue(int index, string value) {
        highscoreValue[index].text = value;    
    }
    
    /*
     *Author(s): Haubold Markus;
     *Description: Get a score from the highscoreValue list
     *Parameter index: Set the list-position as integer
     *Return: The score from the list-postion as string
    */
    public string getHighscoreValue(int index) {
       
        return highscoreValue[index].text;
    }

     /*
     *Author(s): Haubold Markus;
     *Description: Set/unset the variable secondCheckDeleteData (confirm deletion)
     *Parameter value: Set/unset variable secondCheckDeleteData as bool
     *Return: -
    */
    public void setSecondCheckDeleData(bool value) {
        secondCheckDeleteData = value;
    }
    
    /*
     *Author(s): Haubold Markus;
     *Description: Get the state of the variable secondCheckDeleteData 
     *Parameter : -
     *Return: The state of the variable secondCheckDeleteData as bool
    */
    public bool getSecondCheckDeleteData() {
        bool returnValue;
        returnValue = secondCheckDeleteData;

        return returnValue;
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Set/unset the state of the variable runRefreshHighscoreList
     *Parameter value: Set/unset the state of the variable runRefreshHighscoreList as bool
     *Return: -
    */
    public void setRunRefreshHighscoreList(bool value) {
        runRefreshHighscoreList = value;
    }
    
    /*
     *Author(s): Haubold Markus;
     *Description: Get the state of the variable runRefreshHighscoreList
     *Parameter : -
     *Return: The state of the variable runRefreshHighscoreList as bool
    */
    public bool getRunRefreshHighscoreList() {
        bool returnValue;
        returnValue = runRefreshHighscoreList;

        return returnValue;
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Add messagetexts to the list messages // CURRENTLY NOT USED - ITS A PREPERATION FOR THE FUTURE
     *Parameter : -
     *Return: -
    */
    public void setMessage() {
       //later it will maybe possible to change the language -> implement logic here!
       
        messages.Add("Bist du sicher, dass alle Highscoredaten gelöscht werden sollen?");
        messages.Add("Sorry...leider konnte dein Highscore nicht gespeichert werden!"); 
        messages.Add("Du hast es leider nicht unter die Top 5 geschafft!");
    }
    
    /*
     *Author(s): Haubold Markus;
     *Description: Get an messsage from the message-list // CURRENTLY NOT USED - ITS A PREPERATION FOR THE FUTURE
     *Parameter index: Position in the message-list (message selector)
     *Return: The message as string
    */
    public string getMessage(int index) {
        string returnValue;
        returnValue = messages[index];

        return returnValue;
    }



    //startup
    /*
     *Author(s): Haubold Markus;
     *Description: Is called with the first run of the script and is used to initialize the variables,lists and objects
     *Parameter : - 
     *Return: -
    */
    void Start() {
        //get the gameobject from the textfield
        scorefield = GetComponent<TextMeshProUGUI>();

        //set keyNames with startvalues if the are not exists
        bool initDataDone = initializePlayerprefKeys();
        //copy playerprefs to the textfields
        bool copyDone = copyPlayerprefsToTextfields();
        //set scorefield to 0 
        setScorefield(0, 0);
        
        setMessage();
    }



    //declaration of the function
    /*
     *Author(s): Haubold Markus;
     *Description: Shorthand to set the highscoreValue to the PlayerPrefs
     *Parameter index: Set the number from the highscoreValue-key as integer (e.g. highscoreValue2)
     *Parameter value: Set the highscoreValue as string
     *Return: -
    */
    private void setPpValue(int index, string value) {
        if (inRangeOfInt(index, 0, 5)) {
            PlayerPrefs.SetString("highscoreValue" + index, value);
        } else {
            log("Error in setPpValue: Index " + index + " out of range!");
        }
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Shorthand to get the highscoreValue from the PlayerPrefs
     *Parameter index: Set the number from the highscoreValue-key as integer (e.g. highscoreValue2)
     *Return: The highscoreValue as string
    */
    private string getPpValue(int index) {
        string returnValue = "Error in getPpValue: Index " + index + " out of range!";
        if (inRangeOfInt(index, 0, 5)) {
            returnValue = PlayerPrefs.GetString("highscoreValue" + index);
        } else {
            log(returnValue);
        }
    
        return returnValue;
    }
    
    /*
     *Author(s): Haubold Markus;
     *Description: Shorthand to set the highscoreName to the PlayerPrefs
     *Parameter index: Set the number from the highscoreName-key as integer (e.g. highscoreName2)
     *Parameter value: Set the highscoreName as string
     *Return: -
    */
    private void setPpName(int index, string value) {
        if (inRangeOfInt(index, 0, 5)) {
            PlayerPrefs.SetString("highscoreName" + index, value);
        } else {
            log("Error in setPpName: Index " + index + " out of range!");
        }
    }
   
   /*
     *Author(s): Haubold Markus;
     *Description: Shorthand to get the highscoreName from the PlayerPrefs
     *Parameter index: Set the number from the highscoreName-key as integer (e.g. highscoreValue2)
     *Return: The highscoreName as string
    */
    private string getPpName(int index) {
        string returnName = "Error in getPpName: Index " + index + " out of range!";
        if (inRangeOfInt(index, 0, 5)) {
            returnName = PlayerPrefs.GetString("highscoreName" + index);
        } else {
            log(returnName);
        }
    
        return returnName;
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Initialize the PlayerPrefs: set the keys with the default values
     *Parameter : - 
     *Return: The state true if the PlayerPrefs where initialized and with false if not as bool
    */
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
    
    /*
     *Author(s): Haubold Markus;
     *Description: Copy the values highscoreName and highscoreValue from the PlayrePrefs to the eponymous lists
     *Parameter : -  
     *Return: The state true if the copy was successfull or with false if not as bool
    */
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
       
        return false;
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Delete all highscoreName and highscoreValue from the PlayerPrefs
     *Parameter : -
     *Return: The state true if the deletion was successfull or with false if not as bool
    */
    private bool deleteHighscoreData() {
        bool done = false;
        
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
       
        //refresh (copy) textfields in the highscore window
        if (done) {
            PlayerPrefs.Save();
            initializePlayerprefKeys();
            copyPlayerprefsToTextfields();
            
            return true;
        }

        return false;
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Tests whether a given value is within the given bounds
     *Parameter value: The value to test for the bounds 
     *Parameter lowBound: Allowed minimum value for the value
     *Parameter highBound: Allowed maximum value for the value 
     *Return: The state true if the value is within the bounds and false if not as bool
    */
    private bool inRangeOfInt(int value, int lowBound, int highBound) {
        if ((value >= lowBound) && (value <= highBound)) {
            return true;
        }

        return false;
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Convert a number from the type string to an number from the type integer 
     *Parameter value: The number from the type string which to convert
     *Return: The converted number as an integer 
    */
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

    /*
     *Author(s): Haubold Markus;
     *Description: Calculates an rounded an factorized exponential function with the given parameter x
     *Parameter x: The variable value for the calculation
     *Return: The result from the calculation as an double 
    */
    private double calculate(int x) {
        //function parameters
        double score = 0;
        const double a = 0.1;
        const double k = 0.2;
        const double c = - 1;

        if (x == 0) {
            score = 0;
        } else {
            //function= (a*e^k*(x-(c)))+d with x=snakelenght
            score = Math.Round(((a * Math.Exp(k*(x - (c))))) * 100);     
        }
        
        return score;
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Sorts highscoreName and highscoreValue (from PlayerPrefs) in descending order and writes it to the
     *             eponymous lists (=refreshing the list of highscores at the highscore window)
     *Parameter: - 
     *Return: The status true if the refreshing was successful and and false if not as bool
    */
    private bool refreshHighscoreList() {
        const int memoryIndexPp = 5;
        //do nothing with the score if it is lower than another one ore equal
        if (stringToInt(getScorefield()) <= stringToInt(getPpValue(4))) {
            debugLog("Score " + getPpValue(memoryIndexPp) + " to low for highscorelist");
            userInformation(getMessage(2));
            

            return true;
        } else {
             //write the actual playerdata at the PlayPref position 5 because they will needed for bubblesort
            setPpName(memoryIndexPp, getPlayerName());
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
                        userInformation(getMessage(1));
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

    /*
     *Author(s): Haubold Markus;
     *Description: Currently a preparation for an communication with the user (show messages)
     *Parameter message: The to shown message as string 
     *Return: -
    */
    private void userInformation(string message) {
        //TODO: generate an textfield at the UI and link it here!
        debugLog(message);
    }

    //debug stuff
    /*
     *Author(s): Haubold Markus;
     *Description: Debug area to test some functions / codelines rapid (only called if debugModeOn true)
     *Parameter: -
     *Return: -
    */
    private void debugArea() {
        log("DEBUG-MODE ACTIVE!!!");
        

    }
    
    /*
     *Author(s): Haubold Markus;
     *Description: Write a PlayerPref with the given key and value
     *Parameter keyname: The keyname for the PlayerPref as string
     *Parameter value: The value for the given keyname as string
     *Return: -
    */
    private void debugWritePlayerpref(string keyname, string value) {
        PlayerPrefs.SetString(keyname , value);
    }
    
    /*
     *Author(s): Haubold Markus;
     *Description: Shorthand for the UnityEngine.Debug.Log()
     *Parameter message: The message to log
     *Return: -
    */
    private void log(string message) {
        UnityEngine.Debug.Log(message);
    }

    /*
     *Author(s): Haubold Markus;
     *Description: Logs which only shown when debugModeOn is true
     *Parameter message: The message to log
     *Return: -
    */
    private void debugLog(string message) {
        if (debugModeOn) {
            UnityEngine.Debug.Log(message);
        }
    }



    //main
    /*
     *Author(s): Haubold Markus;
     *Description: Update is called every frame and handles the call from the functions wich are used to calculate and 
     *             handle the highscorelist 
     *Parameter: -
     *Return: -
    */
    void Update() {
        //gamover = refresh highscorelist
        if (getRunRefreshHighscoreList() && !getRefreshDone()) {
            //wait for done
            if (refreshHighscoreList()) {
                setRefreshDone(true);
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
                deleteHighscoreData();
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



