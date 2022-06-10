/******************************************************************************
Name:          LevelGrid
Description:   
               
Author(s):     
Date:          
Version:       V1.0 
TODO:          - implement postionController(): check every frame: collision snake<->snake && snake<-->wall
               - destroy food after eat it
               - camelCase-notation!!!
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class LevelGrid
{
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    private Vector2 foodScale = new Vector2(30.0f, 30.0f);  //scale-factor food size
    private const int borderFoodSpawn = 20;
    private Snake snake;
    private int width;
    private int height;

    private bool testModeSpawnOn = true;  //true = spawn apple in rectangle 200x200

    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;

        spawnFood();    //spawn first and after eating
        //FunctionPeriodic.Create(SpawnFood, 0.001f); OLD but do not delete!
    }

    public void Setup(Snake snake)
    {
        this.snake = snake;
    }

    private void spawnFood()
    {
        if (testModeSpawnOn == true) {
            foodGridPosition = new Vector2Int(Random.Range(borderFoodSpawn, 500), 
            Random.Range(borderFoodSpawn, 500));
        } else {
            foodGridPosition = new Vector2Int(Random.Range(borderFoodSpawn, width), 
            Random.Range(borderFoodSpawn, height));
        }
        
        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<Renderer>().sortingOrder = 4;                               //set food at layer 4 (green background = layer 1 to 3)
        foodGameObject.GetComponent<Renderer>().transform.localScale = foodScale;               //scale food
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObject.transform.position = new Vector2(foodGridPosition.x, foodGridPosition.y);


    // avoiding food respawn on the snake    
        /*OLD
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetGridPosition() == foodGridPosition);

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    */
    }

    public void SnakeMoved(Vector2Int snakeGridPosition)
    {
        Debug.Log("snakeMoved executed!!!!!");
        if ((snakeGridPosition.x == foodGridPosition.x) && (snakeGridPosition.y == foodGridPosition.y)) //not tested!
        {
            Debug.Log("snake auf food!!!!!");
            Object.Destroy(foodGameObject);
            spawnFood();
        }


    }
}
