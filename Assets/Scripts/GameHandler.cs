/**********************************************************************************************************************
Name:          GameHandler
Description:   
               
Author(s):     Adel Kharbout
Date:          
Version:       V1.0 
TODO:          - 
               - 
**********************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {
    [SerializeField] Snake snake;
    private LevelGrid levelGrid;

    //gamfield width and height with a small border (20) to the canvas-border
    private const int gamefieldWidth = 680;
    private const int gamefieldHeight = 345;

     void Start() {
       Debug.Log("GameHandler.Start");
       levelGrid = new LevelGrid(gamefieldWidth, gamefieldHeight); 
       snake.Setup(levelGrid);
       levelGrid.Setup(snake);

    }

}
