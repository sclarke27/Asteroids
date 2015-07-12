using UnityEngine;
using System.Collections;

public class PausePanel : PanelBaseClass
{

    private OptionsPanel optionsPanel;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        
    
    }

    public void ResumeGame()
    {
        optionsPanel = GameObject.FindObjectOfType<OptionsPanel>();
        if (optionsPanel == null)
        {
            gameData.PauseGame(false);
        }
    }

    public void MainMenu()
    {
        optionsPanel = GameObject.FindObjectOfType<OptionsPanel>();
        if (optionsPanel == null)
        {
            levelManager.MainMenu();
        }
    }

    public void ShowOptions()
    {
        optionsPanel = GameObject.FindObjectOfType<OptionsPanel>();
        if (optionsPanel == null)
        {
            optionsPanel.ShowOptionsPanel(true);
        }
    }

}
