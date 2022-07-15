/**********************************************************************************************************************
Name:           
Description:    
               
Author(s):      Adel Kharbout
                Haubold Markus (only the interaction with the scoreController 
                object)
Date:          
Version:        V1.0 
TODO:           - 
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class Snake : MonoBehaviour {

    public enum GameStatus {  //Tran Le Xuan: Create GameStatus to run Update()
        Continue,
        Stop
    }

    public GameStatus gameStatus; //Tran Le Xuan: Store private GameStatus 
    public Vector2Int gridMoveDirection;
    public Vector2Int gridPosition;
    private LevelGrid levelGrid;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    public int snakeBodySize;
    public List<Vector2Int> snakeMovePositionList;
    public List<snakeBodyPart> snakeBodyPartList;
    public bool isGameOver;
    PauseMenu myPauseMenu;
    /*GameHandler mygamehandler;  //Emily: please refer Improving Note: CollisionCheckBoarder()*/

    public PauseMenu MyPauseMenu { get => myPauseMenu; set => myPauseMenu = value; }
    /*public GameHandler MyGameHandler { get => mygamehandler; set => mygamehandler = value; } //Emily - please refer Improving Note: at CollisionCheckBoarder()*/
    private int stepDistancePositive = 12;
    private int stepDistanceNegative = -12;
    private float timePerStep = .1f;
    private scoreController scoreControllerScript; //Haubold: scoreController object
    private const int maxLength = 90; //Haubold: max parts of the snake
    private bool checkIfIsFirstStart = true;


    public void Start() {
        MyPauseMenu = GameObject.Find("Pause - Menu - Manager").GetComponent<PauseMenu>(); // Daniel - 06.06.2022 - Zugriff auf Variable aus PauseMenu Script
        /* gamehandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();    //Emily: link GameHandler.cs for passing gamefield-scale
         UnityEngine.Debug.Log("mygamefieldwith " + mygamefieldWidth);                  //Emily: please refer improving Note: CollisionCheckBoarder()
         UnityEngine.Debug.Log("mygamefieldheight " + mygamefieldHeight); */
    }


    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    public void Awake() {
        isGameOver = false;
        if (checkIfIsFirstStart == true) {
            checkIfIsFirstStart = false;
            snakeMovePositionList = new List<Vector2Int>();
            snakeBodyPartList = new List<snakeBodyPart>();
        }
        gridMoveDirection = new Vector2Int(0, stepDistancePositive);
        gridPosition = new Vector2Int(100, 100);
        gridMoveTimerMax = timePerStep; // Faktor fuer Aktualisierung der Schrittfrequenz ( 1f = 1sec )
        gridMoveTimer = gridMoveTimerMax;
        snakeBodySize = 0;
        snakeMovePositionList.Clear();
        snakeBodyPartList.Clear();
        gameStatus = GameStatus.Continue; //Tran Le Xuan: gameStatus equal out continue
        scoreControllerScript = 
            GameObject.Find("Scorefield").GetComponent<scoreController>(); //Haubold: link to scoreController script
        scoreControllerScript.setRefreshDone(false);    //Haubold: reset the done variable from the refreshing
        scoreControllerScript.setRunRefreshHighscoreList(false);    //Haubold: reset the run variable from refreshing
    }

    private void Update() {
        switch (gameStatus) { //Tran Le Xuan: Add switch case for GameStatus
            case GameStatus.Continue:
                HandleInput();
                HandleGridMovement();
                CollisionCheckBoarder();  //Emily: call CollisionCheckBoarder function every frame
                break;
            case GameStatus.Stop:
                break;
        }
    }

    // Daniel - 05.06.2022 - Werte geaendert fuer Movement in groesseren Schritten ( 1 -> 50 )           !!!!!! Werte muessen aber noch an das Grid angepasst werden
    private void HandleInput() {
        if (!MyPauseMenu.gamePaused) { // <- if-Befehl sperrt Bewegung wenn im Pausemenu - Daniel - 06.06.2022
                                                      
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                if (gridMoveDirection.y != stepDistanceNegative) {
                    gridMoveDirection.x = 0;
                    gridMoveDirection.y = stepDistancePositive;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                if (gridMoveDirection.y != stepDistancePositive) {
                    gridMoveDirection.x = 0;
                    gridMoveDirection.y = stepDistanceNegative;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (gridMoveDirection.x != stepDistancePositive) {
                    gridMoveDirection.x = stepDistanceNegative;
                    gridMoveDirection.y = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                if (gridMoveDirection.x != stepDistanceNegative) {
                    gridMoveDirection.x = stepDistancePositive;
                    gridMoveDirection.y = 0;
                }
            }
        }
    }

    private void HandleGridMovement() {
        if (!MyPauseMenu.gamePaused) { // <- if-Befehl sperrt Bewegung wenn im Pausemenu - Daniel - 06.06.2022

            gridMoveTimer += Time.deltaTime;
            if (gridMoveTimer >= gridMoveTimerMax) {
                gridMoveTimer -= gridMoveTimerMax;
                snakeMovePositionList.Insert(0, gridPosition);
                gridPosition += gridMoveDirection;

                // snake ate food = body grows
                bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);
                if (snakeAteFood) {
                    snakeBodySize++;
                    scoreControllerScript.setScorefield(snakeBodySize, maxLength); //Haubold: write length to 
                                                                                   //scoreController (calc act score)
                    CreateSnakeBody();
                }

                if (snakeMovePositionList.Count >= snakeBodySize + 1) {
                    snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
                }
                /*for (int i = 0; i < snakeMovePositionList.Count; i++) { 
                    Vector2Int snakeMovePosition = snakeMovePositionList[i];
                    World_Sprite worldSprite = World_Sprite.Create(new Vector3(snakeMovePosition.x, snakeMovePosition.y), Vector3.one * .5f, Color.white);
                    FunctionTimer.Create(worldSprite.DestroySelf, gridMoveTimerMax);
                }*/

                //Tran Le Xuan: Snake collides with itself
                for (int i = 0; i < snakeMovePositionList.Count; i++) {
                    Vector2Int snakeMovePosition = snakeMovePositionList[i];
                    if (gridPosition == snakeMovePosition) {
                        Gameover();
                    }
                }

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
        for (int i = 0; i < snakeBodyPartList.Count; i++) {
            snakeBodyPartList[i].SetGridPosition(snakeMovePositionList[i]);
        }
    }
    private float GetAngleFromVector(Vector2Int dir) {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
    public Vector2Int GetGridPosition() {
        return gridPosition;
    }
    //return full list of positions (head + body)
    public List<Vector2Int> GetFullSnakeGridPositionList() {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        gridPositionList.AddRange(snakeMovePositionList);
        return gridPositionList;
    }
    public class snakeBodyPart {
        private Vector2Int gridPosition;
        private Transform transform;
        public snakeBodyPart(int bodyIndex) {
            Vector2 bodyPartScale = new Vector2(35.0f, 35.0f); //Haubold: scalefactor for bodyparts-sprite

            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            //Haubold: set sprite to layer 4
            snakeBodyGameObject.GetComponent<Renderer>().sortingOrder = 4;
            //Haubold: scale sprite of the bodyparts
            snakeBodyGameObject.GetComponent<Renderer>().transform.localScale = bodyPartScale;
            //Daniel - set a tag to every bodypart
            snakeBodyGameObject.tag = "SnakeBodyPart";

            transform = snakeBodyGameObject.transform;


        }
        public void SetGridPosition(Vector2Int gridPosition) {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }


    }



   /* Author: Emily Herzner
    * Description: Check if Sneak-Head collides with boarder, if so calls GameOver()
    * Parameter <gridPosition.x>: current coordinate x of Snake-Head
    * Parameter <gridPosition.y>: current coordinate y of Snake-Head 
    * Return: true (nothing changes) or false (Gameover();)
    * Version: 1.0
   */
    public bool CollisionCheckBoarder() {
        GetGridPosition();
        if ((gridPosition.x > 0) && (gridPosition.x < 705) && (gridPosition.y > 0) && (gridPosition.y < 400)) {
            return true;
        }
        else {
            Gameover();
            return false;
        }
    }
    /* Note for improving CollisionCheckBoarder(): 
     * if ((gridPosition.x > 0) && (gridPosition.x < MyGameHandler.gamefieldWidth) && 
     * gridPosition.y > 0) && (gridPosition.y < MyGameHandler.gamefieldHeight)) {}
     * --> would be more functional, Problem: getting right value
     * --> GameHandler.cs was edited, current Values are unuseable for this function
    */
    /*
     * Author: Daniel Rittrich (Verion 2)
     *         (Emily: support version 1 revised by Daniel) 
     * Description: set isGameOver (bool) to true  
     * Parameter: -
     * Return: -
     * Version: 1.1
    */
    public void Gameover() {
       isGameOver = true;

        //Haubold: call the refresh-function from scoreController
        if (!scoreControllerScript.getRefreshDone()) {
            scoreControllerScript.setRunRefreshHighscoreList(true); 
            UnityEngine.Debug.Log("refresh calles from gameover");
        } 
        if (scoreControllerScript.getRefreshDone()) {
            scoreControllerScript.setRunRefreshHighscoreList(false); 
        }
    }
}


