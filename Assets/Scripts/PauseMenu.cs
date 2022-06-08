using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour /////////////////////////////////////////////////////////////////    pruefen ob bereits ein Spielername vergeben wurde und ein Inputfenster einblenden + evtl. var erstellen um startscreen frei zu geben
{
    public Button pauseMenuButton;
    public Button resumeGameButton;
    public Button startSingleplayerButton;
    public Button startMultiplayerButton;
    public GameObject buttonPauseMenu;
    public GameObject buttonAudioOnOff;
    public GameObject pauseMenu;
    public GameObject einstellungsMenu;
    public GameObject anleitungsMenu;
    public GameObject highScoreMenu;
    public GameObject creditsMenu;
    public GameObject startscreenMenu;
    public bool gameStarted;
    public bool gamePaused;

    void Start()
    {
        gamePaused = true;
        gameStarted = false;
        // Daniel - 24.05.2022 - 08.06.2022 geupdatet - ClickEvent fuer IngameButtons. Diese starten folgende Funktionen
        pauseMenuButton.onClick.AddListener(Pause);
        resumeGameButton.onClick.AddListener(Resume);
        startSingleplayerButton.onClick.AddListener(StartGame);
        startMultiplayerButton.onClick.AddListener(StartGame);
    }

    void Update()
    {
        if (gameStarted && Input.GetKeyDown(KeyCode.Escape))
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
    
    // Daniel - 08.06.2022 - schaltet Variable fuer den Spielstart auf true und schlie�t Menue
    public void StartGame()
    {
        gameStarted = true;
        Resume();
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
        creditsMenu.SetActive(false);
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
        creditsMenu.SetActive(false);
        startscreenMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
