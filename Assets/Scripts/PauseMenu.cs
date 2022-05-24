using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject menu2;
    public GameObject menu3;
    public GameObject menu4;
    public GameObject menu5;
    public GameObject menu6;
    public GameObject menu7;
    public GameObject menu8;
    public GameObject menu9;
    public bool gamePaused;

void Start()
{
    gamePaused = false;
}

void Update()
{
    if (Input.GetButtonDown("PauseMenu"))
    {
        PauseOrResume();
    }
}

    // Daniel - 24.05.2022 - pausiere das Spiel bzw. setze das Spiel fort
public void PauseOrResume()
{
    if (gamePaused)
    {
        Resume();
        gamePaused = false;
    }
    else
    {
        Pause();
        gamePaused = true;
    }
}

    // Daniel - 24.05.2022 - Spiel anhalten und Menue anzeigen
public void Pause()
{
    Time.timeScale = 0f;
    pauseMenu.SetActive(true);
}

    // Daniel - 24.05.2022 - Spiel fortsetzen und Menue ausblenden
public void Resume()
{
    Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        menu2.SetActive(false);
        menu3.SetActive(false);
        menu4.SetActive(false);
        menu5.SetActive(false);
        menu6.SetActive(false);
        menu7.SetActive(false);
        menu8.SetActive(false);
        menu9.SetActive(false);
    }
}
