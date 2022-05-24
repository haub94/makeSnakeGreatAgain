using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public bool gamePaused;

void Start()
{
    gamePaused = false;
}

void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
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
}
}
