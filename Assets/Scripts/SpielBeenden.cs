/******************************************************************************
Name:           SpielBeenden
Description:    The script closes the application.
Author(s):      Daniel Rittrich
Date:           2022-05-24
Version:        V1.0
TODO:           - 
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielBeenden : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // Daniel - 24.05.2022 - Spiel beenden
    public void QuitGame()
    {
        Application.Quit();
    }
}
