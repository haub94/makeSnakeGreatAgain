using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;
    private LevelGrid levelGrid;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    [SerializeField] int lenght; //Markus: number of bodyparts for scoreCalculation()
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

    private void Awake()
    {
        gridPosition = new Vector2Int(100, 100);
        gridMoveTimerMax = .5f; // Faktor fuer Aktualisierung der Schrittfrequenz ( 1f = 1sec )
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(0, 50); // Daniel - 05.06.2022 - Werte geaendert für Movement in groesseren Schritten zu Beginn (0, 1) -> (0, 50)
    }

    private void Update()
    {
        HandleInput();
        HandleGridMovement();
    }

    // Daniel - 05.06.2022 - Werte geaendert fuer Movement in groesseren Schritten ( 1 -> 50 )           !!!!!! Werte muessen aber noch an das Grid angepasst werden
    private void HandleInput()
    {
        if (!myPauseMenu.gamePaused) // <- if-Befehl sperrt Bewegung wenn im Pausemenu - Daniel - 06.06.2022
        {
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

    private void HandleGridMovement()
    {
        if (!myPauseMenu.gamePaused) // <- if-Befehl sperrt Bewegung wenn im Pausemenu - Daniel - 06.06.2022
        {
            gridMoveTimer += Time.deltaTime;
            if (gridMoveTimer >= gridMoveTimerMax)
            {
                gridMoveTimer -= gridMoveTimerMax;
                gridPosition += gridMoveDirection;
            }
            // snake head directions
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) - 90);

            levelGrid.SnakeMoved(gridPosition);
        }
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }
}


