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
    
    private enum GameStatus {  // Create GameStatus to run Update() - Le Xuan
        Continue,
        Stop
    }

    private GameStatus gameStatus; // Store private GameStatus - Le Xuan 
    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;
    private LevelGrid levelGrid;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private int snakeBodySize;
    private List<Vector2Int> snakeMovePositionList;
    private List<snakeBodyPart> snakeBodyPartList;
    PauseMenu myPauseMenu;

    private bool canDoAction; // Booltrigger for stoping Input - Emily
   /* public int mygamefieldWidth;
    public int mygamefieldHeight;
    GameHandler gamehandler;  // my try to get the const value from gamehandler - Emily*/

    public PauseMenu MyPauseMenu { get => myPauseMenu; set => myPauseMenu = value; }
    private int stepDistancePositive = 12;
    private int stepDistanceNegative = -12;
    private float timeToStep = .1f;
    private scoreController scoreControllerScript;
    private const int maxLength = 30; //Haubold: maximal parts of the Snake



    public void Start()
    {
        MyPauseMenu = GameObject.Find("Pause - Menu - Manager").GetComponent<PauseMenu>(); // Daniel - 06.06.2022 - Zugriff auf Variable aus PauseMenu Script
       /* gamehandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

        GameObject GameHandler = GameObject.Find("GameHandler");
        GameHandler gamehandler = GameHandler.GetComponent<GameHandler>(); 
        mygamefieldWidth = gamehandler.gamefieldWidth;
        mygamefieldHeight = gamehandler.gamefieldHeight;
        UnityEngine.Debug.Log("mygamefieldwith " + mygamefieldWidth);
        UnityEngine.Debug.Log("mygamefieldheight " + mygamefieldHeight); */
    }

    /*
     * Author(s): Haubold Markus
     * Description: Get the maximal length (bodyparts) of the snake 
     * Parameter: -
     * Return: The maximal length of the snake as integer 
    */
    public int getMaxLength() {
        int returnValue;
        returnValue = maxLength;

        return returnValue;
    }

    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    private void Awake() {
        canDoAction = true;     //Booltrigger for Input set true - Emily
        gridPosition = new Vector2Int(100, 100);
        gridMoveTimerMax = timeToStep; // Faktor fuer Aktualisierung der Schrittfrequenz ( 1f = 1sec )
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(0, stepDistancePositive); // Daniel - 05.06.2022 - Werte geaendert f�r Movement in groesseren Schritten zu Beginn (0, 1) -> (0, 50)
        snakeMovePositionList = new List<Vector2Int>();
        snakeBodySize = 0;
        snakeBodyPartList = new List<snakeBodyPart>();
        gameStatus = GameStatus.Continue; // gameStatus equal out continue - Le Xuan
        scoreControllerScript = GameObject.Find("Scorefield").GetComponent<scoreController>(); //Haubold:link to script
    }

    private void Update() {
        switch (gameStatus) { // Running Update - Le Xuan
          case GameStatus.Continue:
               HandleInput();
               HandleGridMovement();
               CollisionCheckBoarder();  // Collision with boarder functioin by Emily
               break;
           case GameStatus.Stop:
               break;   
        }                                   
        //UnityEngine.Debug.Log("GridPosition:X=" + gridPosition.x); // x value snake position - Emily
        //UnityEngine.Debug.Log("GridPosition:Y=" + gridPosition.y); // y value snake position - Emily

        //UnityEngine.Debug.Log("Snake at PosX: " + gridPosition.x + " and PosY: " + gridPosition.y);

    }

    // Daniel - 05.06.2022 - Werte geaendert fuer Movement in groesseren Schritten ( 1 -> 50 )           !!!!!! Werte muessen aber noch an das Grid angepasst werden
    private void HandleInput() {
        if (!MyPauseMenu.gamePaused && canDoAction) { // <- if-Befehl sperrt Bewegung wenn im Pausemenu - Daniel - 06.06.2022
                                                            // canDoAction for allowing Input - Emily - 22.06.2022
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (gridMoveDirection.y != stepDistanceNegative)
                {
                    gridMoveDirection.x = 0;
                    gridMoveDirection.y = stepDistancePositive;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (gridMoveDirection.y != stepDistancePositive)
                {
                    gridMoveDirection.x = 0;
                    gridMoveDirection.y = stepDistanceNegative;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (gridMoveDirection.x != stepDistancePositive)
                {
                    gridMoveDirection.x = stepDistanceNegative;
                    gridMoveDirection.y = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (gridMoveDirection.x != stepDistanceNegative)
                {
                    gridMoveDirection.x = stepDistancePositive;
                    gridMoveDirection.y = 0;
                }
            }
        }
    }

    private void HandleGridMovement() {
        if (!MyPauseMenu.gamePaused) { // <- if-Befehl sperrt Bewegung wenn im Pausemenu - Daniel - 06.06.2022
        
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
                    scoreControllerScript.setScorefield(snakeBodySize);
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

                // Snake collides with itself - Le Xuan
                for (int i = 0; i < snakeMovePositionList.Count; i++) { 
                    Vector2Int snakeMovePosition = snakeMovePositionList [i];
                    if (gridPosition == snakeMovePosition)
                    {
                        Gameover ();
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
            Vector2 bodyPartScale = new Vector2(35.0f, 35.0f); //Haubold: scalefactor for bodyparts-sprite
            
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            //Haubold: set sprite to layer 4
            snakeBodyGameObject.GetComponent<Renderer>().sortingOrder = 4;    
            //Haubold: scale sprite of the bodyparts
            snakeBodyGameObject.GetComponent<Renderer>().transform.localScale = bodyPartScale; 

            transform = snakeBodyGameObject.transform;


        }
        public void SetGridPosition(Vector2Int gridPosition) {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }

        
    }


   
    // Snake collision check with boarder  - Emily
    //doesn't work: (0 < gridPosition.x < 1080) && (0 < gridPosition.y < 550)
    public bool CollisionCheckBoarder()
    {
        GetGridPosition();
        if ((gridPosition.x > 0) && (gridPosition.x < 1080) && (gridPosition.y > 0) && (gridPosition.y < 550)) //works
        {
            canDoAction = true; //Trigger for getting KeyDown values - on
            return true;
        }
        else
        {
            Gameover();
            UnityEngine.Debug.Log("gameover");
            return false;
        }
    }

    // let Snake die and game over - Emily
    public void Gameover() {
        canDoAction = false;                                        //Trigger for blocking KeyDown values - off
        GetGridPosition();                                          //do i need it for the Scorecalculating?
        Vector2Int NullgridMoveDirection = new Vector2Int(0, 0);    //inizialise a Vector with (0,0) for setting gridMoveDirection to (0,0) --> no movement
        gridMoveDirection = NullgridMoveDirection;                  //gridMoveDirection = (0,0)
        gridMoveTimer = 0f;                                         //setting speed of steps Null 
       /* <scoreController>().enable = true; // Run ScoreController Script while enabled - unfinished*/
        gameStatus = GameStatus.Stop; // Game stops when Snake bites itself. - Le Xuan


       /* if (score > highscore){                                   //compare scores
            highScore = score;
        }
        */
    
    }

  
}


