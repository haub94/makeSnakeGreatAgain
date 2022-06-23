/**********************************************************************************************************************
Name:           LevelGrid
Description:   
               
Author(s):      Adel Kharbout
Date:          
Version:       V1.0 
TODO:          - implement postionController(): check every frame: collision snake<->snake && snake<-->wall
               - destroy food after eat it
               - camelCase-notation
               - getter and setter
**********************************************************************************************************************/
using System;
using Random=UnityEngine.Random;
using Object = UnityEngine.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class LevelGrid {
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    
    private Snake snake;
    private int width;
    private int height;
    private const int foodRadiusToEat = 15;

    public LevelGrid(int width, int height) {
        this.width = width;
        this.height = height;
    }

    public void Setup(Snake snake) {
        this.snake = snake;
        spawnFood();
    }
    
    
    public void spawnFood() {
        Vector2 foodScale = new Vector2(35.0f, 35.0f);  //Haubold: factor to scale the food-sprite up
        const int borderFoodSpawn = 20; //Haubold: spawndistance to the border (without you will only see a half apple)

        // avoiding food resapawn on the snake (head + body parts) 
        //generate random position for respawn
        do {
            //Haubold: change random-x-position from 0 to borderFoodSpawn
            foodGridPosition = new Vector2Int(Random.Range(borderFoodSpawn, width), 
            Random.Range(borderFoodSpawn, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);
        
        //foodGameObject settings
        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        //Haubold: set apple-sprite to layer 4
        foodGameObject.GetComponent<Renderer>().sortingOrder = 4;                               //set food at layer 4 (green background = layer 1 to 3)
        //Haubold: scale the food-sprite
        foodGameObject.GetComponent<Renderer>().transform.localScale = foodScale;               //scale food
        
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        //set respawn postion
        foodGameObject.transform.position = new Vector2(foodGridPosition.x, foodGridPosition.y);
        //Haubold: add log to see the coordinates of the apple
        UnityEngine.Debug.Log("Food spawn at PosX: " + foodGridPosition.x + " and PosY: " + foodGridPosition.y);

    
    }

    // snake eats
    public bool TrySnakeEatFood(Vector2Int snakeGridPosition) {
        //Haubold: Change if-sequenz: set eat-range, otherwise it will be nearly unpossible to hit the apple exactly
        if ((Math.Abs(snakeGridPosition.x - foodGridPosition.x) < foodRadiusToEat) && 
            (Math.Abs(snakeGridPosition.y - foodGridPosition.y) < foodRadiusToEat))  {
            Object.Destroy(foodGameObject);
            spawnFood();
            return true;
        } else {
            return false;
        }
    }
}
