using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button pauseMenuButton;
    public Button resumeGameButton;
    public GameObject buttonPauseMenu;
    public GameObject buttonAudioOnOff;
    public GameObject pauseMenu;
    public GameObject einstellungsMenu;
    public GameObject anleitungsMenu;
    public GameObject highScoreMenu;
    public bool gamePaused;

    void Start()
    {
        gamePaused = false;
        pauseMenuButton.onClick.AddListener(Pause);
        resumeGameButton.onClick.AddListener(Resume);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        }
        else
        {
            Pause();
        }
    }

    // Daniel - 24.05.2022 - 05.06.2022 geupdatet - Spiel anhalten und Menue anzeigen
    public void Pause()
    {
        Time.timeScale = 0f;
        gamePaused = true;
        buttonPauseMenu.SetActive(false);
        buttonAudioOnOff.SetActive(false);
        pauseMenu.SetActive(true);
        einstellungsMenu.SetActive(false);
        anleitungsMenu.SetActive(false);
        highScoreMenu.SetActive(false);
    }

    // Daniel - 24.05.2022 - 05.06.2022 geupdatet - Spiel fortsetzen und Menue ausblenden
    public void Resume()
    {
        gamePaused = false;
        buttonPauseMenu.SetActive(true);
        buttonAudioOnOff.SetActive(true);
        pauseMenu.SetActive(false);
        einstellungsMenu.SetActive(false);
        anleitungsMenu.SetActive(false);
        highScoreMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
