using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsPanel : PanelBaseClass
{

    public static OptionsPanel optionsPanel;

    // player pref UI Fields
    public Slider musicVolSlider;
    public Slider sfxVolSlider;
    public Toggle useAIToggle;
    public Toggle useGAToggle;
    public GameAnalytics gameAnalytics;
    private bool panelActive = false;

    void Awake()
    {
        
        if (optionsPanel == null)
        {
            DontDestroyOnLoad(gameObject);
            optionsPanel = this;
        }
        else if (this != optionsPanel)
        {
            Destroy(gameObject);
        }
    }

    new void Start()
    {
        base.Start();
        PopulateDefaultValues();
    }


    new void Update()
    {
        base.Update();
    }

    void PopulateDefaultValues()
    {
        if (gameData != null)
        {
            musicVolSlider.value = gameData.GetMusicVolume();
            sfxVolSlider.value = gameData.GetSFXVolume();
            useAIToggle.isOn = gameData.GetAIEnabled();
            useGAToggle.isOn = gameData.GetGAEnabled();
        }
    }

    public void SaveSelectedOptions()
    {
        gameData.SetMusicVolume(musicVolSlider.value);
        gameData.SetSFXVolume(sfxVolSlider.value);
        gameData.SetAIEnabled(useAIToggle.isOn);
        gameData.SetGAEnabled(useGAToggle.isOn);
        PopulateDefaultValues();
        ShowOptionsPanel(false);
    }

    public void ShowOptionsPanel(bool showPanel)
    {
        panelActive = showPanel;
        if (panelActive)
        {
            gameAnalytics.LogScreen("Options Panel - Show");
        }
        else
        {
            gameAnalytics.LogScreen("Options Panel - Hide");
        }
        PopulateDefaultValues();
        gameObject.SetActive(panelActive);
    }


}
