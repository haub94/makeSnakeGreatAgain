using System.Diagnostics;
/******************************************************************************
Name:          scoreCalculator
Description:   The script calculates the highscore depending on the lenght of
               the snake and playtime.
Author(s):     Markus Haubold
Date:          2022-05-31
Version:       V1.0 
TODO:          - rename to scoreController
               - 
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreCalculator : MonoBehaviour
{
    Snake snake;
    private TextMeshProUGUI scorefield;
    
    //getter and setter
    //scorefield
    public void setScorefield(string value) {
        scorefield.text = value;
    }
    public string getScorefield() {
        string text = "";

        return text;
    }

    // Start is called before the first frame update
    void Start()
    {
        //reference to snake object
        snake = GameObject.Find("Snake").GetComponent<Snake>();
        scorefield = GetComponent<TextMeshProUGUI>();
        setScorefield("0");
    }

    //calculate the actual score (depends on snakelenght)
    private double calculate(int lenght, float playtime, int bonusFactor1, 
    int bonusFactor2) {
        //function parameters
        double score = 0;
        const double a = 3.0;
        const double k = 0.2;
        const double c = -4;
        const double d = 0;
        int x = lenght;

        if (x == 0) {
            score = 0;
        } else
        {
            //function= (a*e^k*(x-(c)))+d with x=snakelenght
            score = Math.Round(((a * Math.Exp(k*(x - (c)))) + d) * 100); 
            // + (funktion playtime) + (function bF1) + (function bF2)    
        }
        
        return score;
    }

    //Wall of fame aka top five highscores








    // Update is called once per frame
    void Update()
    {
        //calculate and update score
        setScorefield(calculate(snake.getLenght(), 1, 1, 1).ToString());
        
    }
}
