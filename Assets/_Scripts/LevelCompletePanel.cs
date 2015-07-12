using UnityEngine;
using System.Collections;

public class LevelCompletePanel : PanelBaseClass
{

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    public void NextLevel()
    {
        levelManager.LoadNextLevel();
    }

    public void RetryLevel()
    {
        levelManager.RestartLevel();
    }

    public void QuitGame()
    {
        levelManager.QuitRequest();
    }
}
