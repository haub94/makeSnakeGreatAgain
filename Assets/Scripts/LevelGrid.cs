using System;
using Random=UnityEngine.Random;
using Object = UnityEngine.Object;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class LevelGrid {
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    private Vector2 foodScale = new Vector2(35.0f, 35.0f);  //scale-factor food size
    private const int borderFoodSpawn = 20;
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
        // avoiding food resapawn on the snake (head + body parts) 
        //generate random position for respawn
        do {
            
            foodGridPosition = new Vector2Int(Random.Range(borderFoodSpawn, width), 
            Random.Range(borderFoodSpawn, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);
        
        //foodGameObject settings
        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<Renderer>().sortingOrder = 4;                               //set food at layer 4 (green background = layer 1 to 3)
        foodGameObject.GetComponent<Renderer>().transform.localScale = foodScale;               //scale food
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        //set respawn postion
        foodGameObject.transform.position = new Vector2(foodGridPosition.x, foodGridPosition.y);
        //Haubold: add log to see where the apple is
        UnityEngine.Debug.Log("Food spawn at PosX: " + foodGridPosition.x + " and PosY: " + foodGridPosition.y);

    
    }

    // snake eats
    public bool TrySnakeEatFood(Vector2Int snakeGridPosition) {
        //Haubold: Edited if-sequenz: give an eat-range, otherwise it will be unpossiblr to hit exactly apple
        if ((Math.Abs(snakeGridPosition.x - foodGridPosition.x) < foodRadiusToEat) && 
            (Math.Abs(snakeGridPosition.y - foodGridPosition.y) < foodRadiusToEat))  {
            UnityEngine.Debug.Log("Schlange hat gefressen!!!!!!!");
            Object.Destroy(foodGameObject);
            spawnFood();
            return true;
        } else {
            return false;
        }
    }
}
