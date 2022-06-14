/******************************************************************************
Name:           
Description:    
               
Author(s):      Adel Kharbout
Date:          
Version:        V1.0 
TODO:           - english language
                - camelCase-notation
                - bracket formation (readme codestyle!)

******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class Snake : MonoBehaviour {
    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;
    private LevelGrid levelGrid;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private int snakeBodySize;
    private List<Vector2Int> snakeMovePositionList;
    private List<snakeBodyPart> snakeBodyPartList;
    PauseMenu myPauseMenu;

    public void Start()
    {
        myPauseMenu = GameObject.Find("Pause - Menu - Manager").GetComponent<PauseMenu>(); // Daniel - 06.06.2022 - Zugriff auf Variable aus PauseMenu Script
    }

    //getter and setter
    //lenght
    public void setLenght(int value)
    {  //funktioniert noch nicht 
        lenght = value;

    }
    public int getLenght()
    {
        int returnLenght = lenght;

        return returnLenght;
    }

    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    private void Awake() {
        gridPosition = new Vector2Int(100, 100);
        gridMoveTimerMax = .5f; // Faktor fuer Aktualisierung der Schrittfrequenz ( 1f = 1sec )
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(0, 50); // Daniel - 05.06.2022 - Werte geaendert f�r Movement in groesseren Schritten zu Beginn (0, 1) -> (0, 50)
        snakeMovePositionList = new List<Vector2Int>();
        snakeBodySize = 0;
        snakeBodyPartList = new List<snakeBodyPart>();
    }

    private void Update() {
        HandleInput();
        HandleGridMovement();
    }

    // Daniel - 05.06.2022 - Werte geaendert fuer Movement in groesseren Schritten ( 1 -> 50 )           !!!!!! Werte muessen aber noch an das Grid angepasst werden
    private void HandleInput() {
        if (!myPauseMenu.gamePaused) { // <- if-Befehl sperrt Bewegung wenn im Pausemenu - Daniel - 06.06.2022
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (gridMoveDirection.y != -50)
                {
                    gridMoveDirection.x = 0;
                    gridMoveDirection.y = 50;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (gridMoveDirection.y != 50)
                {
                    gridMoveDirection.x = 0;
                    gridMoveDirection.y = -50;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (gridMoveDirection.x != 50)
                {
                    gridMoveDirection.x = -50;
                    gridMoveDirection.y = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (gridMoveDirection.x != -50)
                {
                    gridMoveDirection.x = 50;
                    gridMoveDirection.y = 0;
                }
            }
        }
    }

    private void HandleGridMovement() {
        if (!myPauseMenu.gamePaused) { // <- if-Befehl sperrt Bewegung wenn im Pausemenu - Daniel - 06.06.2022
        
            gridMoveTimer += Time.deltaTime;
            if (gridMoveTimer >= gridMoveTimerMax)
            {
                gridMoveTimer -= gridMoveTimerMax;
                snakeMovePositionList.Insert(0, gridPosition);
                gridPosition += gridMoveDirection;

                // snake ate food = body grows
                bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);
                if (snakeAteFood) {
                    snakeBodySize++;
                    CreateSnakeBody();
                }

                if (snakeMovePositionList.Count >= snakeBodySize +1) {
                    snakeMovePositionList.RemoveAt(snakeMovePositionList.Count -1);
                }
                /*for (int i = 0; i < snakeMovePositionList.Count; i++) { 
                    Vector2Int snakeMovePosition = snakeMovePositionList[i];
                    World_Sprite worldSprite = World_Sprite.Create(new Vector3(snakeMovePosition.x, snakeMovePosition.y), Vector3.one * .5f, Color.white);
                    FunctionTimer.Create(worldSprite.DestroySelf, gridMoveTimerMax);
                }*/
            
            // snake position on Start
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            // snake head optimization 
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) - 90);

            
            UpdateSnakeBodyParts();

            }
        }
    }
    /*private void CreateSnakeBody() {
        GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
        snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite;
        snakeBodyTransformList.Add(snakeBodyGameObject.transform);
        snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -snakeBodyTransformList.Count;
    }*/
    private void CreateSnakeBody() {
        snakeBodyPartList.Add(new snakeBodyPart(snakeBodyPartList.Count));
    }
    private void UpdateSnakeBodyParts() {
        // snake body parts follow the posititon of the snake head
            for (int i = 0; i <snakeBodyPartList.Count; i++) {
                snakeBodyPartList[i].SetGridPosition(snakeMovePositionList[i]);
            }
    }
    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
    public Vector2Int GetGridPosition() {
        return gridPosition;
    }
    //return full list of positions (head + body)
    public List<Vector2Int> GetFullSnakeGridPositionList() {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() {gridPosition};
        gridPositionList.AddRange(snakeMovePositionList);
        return gridPositionList;
    }
    private class snakeBodyPart{
        private Vector2Int gridPosition;
        private Transform transform;
        public snakeBodyPart(int bodyIndex) {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = snakeBodyGameObject.transform;
        }
        public void SetGridPosition(Vector2Int gridPosition) {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }
    }
}


