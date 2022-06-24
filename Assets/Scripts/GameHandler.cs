/******************************************************************************
Name:          GameHandler
Description:   
               
Author(s):     
Date:          
Version:       V1.0 
TODO:          - 
               - 
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {
    private Snake snake;
    private LevelGrid levelGrid;

   /* public const int mygamefielWidth = 999;
    public const int mygamefieldHeight = 666;  //test variablen */

    //gamfield width and height with a small border (20) to the canvas-border
    public const int gamefieldWidth = 1080; //changed to public for passing to snake.cs - Emily - 23.06.
    public const int gamefieldHeight = 550; //changed to public for passing to snake.cs - Emily 23.06.

     void Start() {
       Debug.Log("GameHandler.Start");
       levelGrid = new LevelGrid(gamefieldWidth, gamefieldHeight); 
       snake.Setup(levelGrid);
       levelGrid.Setup(snake);

    }


    public void GetFieldWidthHeight()
    {

    }
}
