/*
*Name:          scoreCalculator
*Description:   The script calculates the highscore depending on the lenght of
*               the snake and playtime.
*Author(s):     Markus
*Date:          2022-05-31
*Version:       V1.0 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreCalculator : MonoBehaviour
{
    //create snake from class Snake
    Snake snake;
  
    // Start is called before the first frame update
    void Start()
    {
        //reference to snake object
        snake = GameObject.Find("Snake").GetComponent<Snake> ();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(snake.lenght);
    }
}
