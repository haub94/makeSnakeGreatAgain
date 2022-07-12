/******************************************************************************
Name:           SoundManager
Description:    The script manages to play backgroundmusic and the option to
                pause it by clicking the speaker buttons. The players choice 
                if the music is on or off will be saved for the next gamestart.
Author(s):      Daniel Rittrich
Date:           2022-05-24
Version:        V1.0
TODO:           - 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    [SerializeField] Image soundOnIconGame;
    [SerializeField] Image soundOffIconGame;
    [SerializeField] Image soundOnIconSettings;
    [SerializeField] Image soundOffIconSettings;
    private bool muted = false;

    /*
     * Author: Daniel Rittrich 
     * Description: checks for sound settings and initialize settings for gamestart
     * Parameter: -
     * Return: -
    */
    void Start() {
        if (!PlayerPrefs.HasKey("muted")) {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else {
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    /*
     * Author: Daniel Rittrich 
     * Description: handles the changes for sound-settings (sound on/off)
     * Parameter: -
     * Return: -
     * Code-Quelle: https://www.youtube.com/watch?v=AFcHsKd_aMo (zuletzt aufgerufen am 08.07.2022)
    */
    public void SoundOnOff() {
        if (muted == false) {
            muted = true;
            AudioListener.pause = true;
        }
        else {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonIcon();
    }

    /*
     * Author: Daniel Rittrich 
     * Description: changes the icons for sound on/off in game and in settings 
     * Parameter: -
     * Return: -
    */
    private void UpdateButtonIcon() {
        if (muted == false) {
            soundOnIconGame.enabled = true;
            soundOffIconGame.enabled = false;
            soundOnIconSettings.enabled = true;
            soundOffIconSettings.enabled = false;
        }
        else {
            soundOnIconGame.enabled = false;
            soundOffIconGame.enabled = true;
            soundOnIconSettings.enabled = false;
            soundOffIconSettings.enabled = true;
        }
    }

    /*
     * Author: Daniel Rittrich 
     * Description: load the last setting for sound on/off from playerprefs 
     * Parameter: -
     * Return: -
    */
    private void Load() {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    /*
     * Author: Daniel Rittrich 
     * Description: save the setting for sound on/off in playerprefs
     * Parameter: -
     * Return: -
    */
    private void Save() {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
