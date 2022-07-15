/**********************************************************************************************************************
Name:           BackgroundMusic
Description:    The script checks if a audio file is embedded 
Author(s):      Daniel Rittrich
Date:           2022-06-03
Version:        V1.0
TODO:           - 
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {
    private static BackgroundMusic backgroundMusic;

    /*
     * Author: Daniel Rittrich 
     * Description: checks if there exists a audio file
     * Parameter: -
     * Return: -
     * Version: 1.0
     * Code-Quelle: https://www.youtube.com/watch?v=AFcHsKd_aMo (zuletzt aufgerufen am 08.07.2022)
    */
    void Awake() {
        if (backgroundMusic == null) {
            backgroundMusic = this;
            DontDestroyOnLoad(backgroundMusic);
        }
        else {
            Destroy(backgroundMusic);
        }
    }
}
