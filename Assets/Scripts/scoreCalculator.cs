/*
*Name:          scoreCalculator
*Description:   The script calculates the highscore depending on the lenght of
*               the snake and playtime.
*Author(s):     Markus Haubold
*Date:          2022-05-31
*Version:       V1.0 
*/

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

    // Update is called once per frame
    void Update()
    {
        //hier die berechnung machen
        Debug.Log("act snakelenght: " + snake.getLenght());
        setScorefield(snake.getLenght().ToString());

        //show actual score in gamefield
        

    }
}
