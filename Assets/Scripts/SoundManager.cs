using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] Image soundOnIconGame;
    [SerializeField] Image soundOffIconGame;
    [SerializeField] Image soundOnIconSettings;
    [SerializeField] Image soundOffIconSettings;
    private bool muted = false;

    void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }

        UpdateButtonIcon();
        AudioListener.pause = muted;

    }

    void Update()
    {

    }

    // Daniel - 24.05.2022 - Hintergrundmusik An- bzw. Ausschalten
    public void SoundOnOff()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }

        Save();
        UpdateButtonIcon();
    }

    // Daniel - 24.05.2022 - ButtonIcon ändern An->Aus bzw. Aus->An
    private void UpdateButtonIcon()
    {
        if (muted == false)
        {
            soundOnIconGame.enabled = true;
            soundOffIconGame.enabled = false;
            soundOnIconSettings.enabled = true;
            soundOffIconSettings.enabled = false;
        }
        else
        {
            soundOnIconGame.enabled = false;
            soundOffIconGame.enabled = true;
            soundOnIconSettings.enabled = false;
            soundOffIconSettings.enabled = true;
        }
    }

    // Daniel - 24.05.2022 - Soundeinstellungen aus den Spielereinstellungen laden
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    // Daniel - 24.05.2022 - Soundeinstellungen in den Spielereinstellungen speichern
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
