using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameData : MonoBehaviour
{

    public static GameData instance;
    public int levelUnitPixelWidth = 16;
    public float defaultSFXVolume = 1f;
    public float defaultMusicVolume = 0.14f;
    public float defaultDifficultyLevel = 1f;
    public float defaultPaddleSpeed = 0.3f;
    public int defaultPlayerLives = 5;
    public GameAnalytics gameAnalytics;
    public AudioSource collect1UpSound;
    public PlayerShip playerShip;

    private float ballStartingVelocity = 7.0f;
    private bool useAI = false;
    private bool useGA = false;
    private float difficultyLevel = 2f;
    private int playerScore = 0;
    private int playerLives = 0;
    private float playerPaddleSpeed;
    private bool gamePaused;
    private float currMusicVolume;
    private float currSFXVolume;
    private int totalHighScores = 25;
    private ArrayList highScoreList = new ArrayList();
    private bool playerReady = false;
    private bool hasSeenInstructions = true;
    public Vector2 minScreenBounds;
    public Vector2 maxScreenBounds;


    private bool leftPaddledown = false;
    private bool rightPaddledown = false;
    private bool upPaddledown = false;

    private PlayerShip playerOne;
    private LevelManager levelManager;

    public enum playerPrefTypes
    {
        musicVolume,
        sfxVolume,
        useAI,
        useGA,
        difficultyLevel,
        paddleSpeed
    };

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            levelManager = GameObject.FindObjectOfType<LevelManager>();
            SetMusicVolume(PlayerPrefs.GetFloat(playerPrefTypes.musicVolume.ToString(), defaultMusicVolume));
            SetSFXVolume(PlayerPrefs.GetFloat(playerPrefTypes.sfxVolume.ToString(), defaultSFXVolume));
            SetAIEnabled((PlayerPrefs.GetFloat(playerPrefTypes.useAI.ToString(), (!useAI) ? 0 : 1) == 0) ? false : true);
            SetGAEnabled((PlayerPrefs.GetFloat(playerPrefTypes.useGA.ToString(), (!useGA) ? 0 : 1) == 0) ? false : true);
            SetDifficulty(PlayerPrefs.GetFloat(playerPrefTypes.difficultyLevel.ToString(), defaultDifficultyLevel));
            SetPlayerPaddleSpeed(PlayerPrefs.GetFloat(playerPrefTypes.paddleSpeed.ToString(), defaultPaddleSpeed));
            LoadHighScores();
            ResetPlayerLives();

            Camera mainCam = GameObject.FindObjectOfType<Camera>();
            Vector2 extents = new Vector2(mainCam.orthographicSize * Screen.width / Screen.height, mainCam.orthographicSize);
            minScreenBounds = (Vector2)mainCam.transform.position - extents;
            maxScreenBounds = (Vector2)mainCam.transform.position + extents;

        }
        else if (this != instance)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        Debug.Log("minX: " + minScreenBounds.x + " minY:" + minScreenBounds.y);
        Debug.Log("maxX: " + maxScreenBounds.x + " maxY:" + maxScreenBounds.y);
    }

    void Update()
    {
        Camera mainCam = GameObject.FindObjectOfType<Camera>();
        Vector2 extents = new Vector2(mainCam.orthographicSize * Screen.width / Screen.height, mainCam.orthographicSize);
        minScreenBounds = (Vector2)mainCam.transform.position - extents;
        maxScreenBounds = (Vector2)mainCam.transform.position + extents;

        if (playerOne == null)
        {
            playerOne = GameObject.FindObjectOfType<PlayerShip>();
        }
    }

    public Vector2 GetMinScreenBounds()
    {
        return minScreenBounds;
    }

    public Vector2 GetMaxScreenBounds()
    {
        return maxScreenBounds;
    }

    public void SetPlayerReady(bool isReady)
    {
        if (playerOne != null)
        {
            Destroy(playerOne.gameObject);
        }
        
        playerReady = isReady;
        if (playerReady)
        {
            playerOne = Instantiate(playerShip, new Vector2(0f, 0f), Quaternion.identity) as PlayerShip;
            Cursor.visible = false;
        }
    }

    public bool IsPlayerReady()
    {
        return playerReady;
    }

    public bool PlayerViewedInstructions()
    {
        return hasSeenInstructions;
    }

    public void SetInstructionsViewed()
    {
        hasSeenInstructions = true;
    }

    public void ResetPlayerScore()
    {
        playerScore = 0;
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public void DeleteHighScores()
    {
        for (int i = 1; i <= totalHighScores; i++)
        {
            PlayerPrefs.DeleteKey("highscore" + i);
        }
        highScoreList = new ArrayList();
        LoadHighScores();
    }

    public void LoadHighScores()
    {
        string defaultStr = "******,0";
        for (int i = 1; i <= totalHighScores; i++)
        {
            string currentString = PlayerPrefs.GetString("highscore" + i);
            if (currentString.Length > 1)
            {
                highScoreList.Add(currentString);
            }
            else
            {
                highScoreList.Add(defaultStr);
            }
        }
    }

    public int GetPlayerScoreRank()
    {
        int index = 0;
        foreach (string score in highScoreList)
        {
            string[] scoredata = score.Split(',');
            if (playerScore > int.Parse(scoredata[1]))
            {
                return index;
            }
            index = index + 1;
        }

        return totalHighScores + 1;
    }

    public void SavePlayerScore(string playerName)
    {
        int index = 0;
        foreach (string score in highScoreList)
        {
            string[] scoredata = score.Split(',');
            if (playerScore > int.Parse(scoredata[1]))
            {
                highScoreList.Insert(index, playerName + "," + playerScore);
                SaveHighScores();
                return;
            }
            index = index + 1;
        }

    }

    public ArrayList GetHighScores()
    {
        return highScoreList;
    }

    public void SaveHighScores()
    {
        int index = 1;
        foreach (string score in highScoreList)
        {
            if (score.IndexOf(',') > 1)
            {
                PlayerPrefs.SetString("highscore" + index, score);
            }
            index = index + 1;
        }

    }

    public void AddPlayerScore(int scoreValue)
    {
        playerScore = playerScore + scoreValue;
    }

    public bool IsGamePaused()
    {
        return gamePaused;
    }

    public void UnpauseGame()
    {
        PauseGame(false);
    }

    public void PauseGame(bool doPause)
    {
        gamePaused = doPause;
        if (gamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        if (gameAnalytics != null)
        {
            gameAnalytics.LogEvent(GameAnalytics.gaEventCategories.GameEvent, "gamePaused", "Game Paused");
        }

    }

    public bool GetAIEnabled()
    {
        return useAI;
    }

    public void SetAIEnabled(bool enableAI)
    {
        PlayerPrefs.SetFloat(playerPrefTypes.useAI.ToString(), (enableAI) ? 1 : 0);
        useAI = enableAI;

        if (gameAnalytics != null)
        {
            gameAnalytics.LogEvent(GameAnalytics.gaEventCategories.UIEvent, "aiEnabled", "AI Paddle Toggled", ((useAI) ? 1 : 0));
        }

    }

    public float GetMusicVolume()
    {
        return currMusicVolume;
    }

    public void SetMusicVolume(float newVolume)
    {
        PlayerPrefs.SetFloat(playerPrefTypes.musicVolume.ToString(), newVolume);
        currMusicVolume = newVolume;
        if (gameAnalytics != null)
        {
            gameAnalytics.LogEvent(GameAnalytics.gaEventCategories.UIEvent, "musicVolume", "Set Music volume", (System.Convert.ToInt64(currMusicVolume)));
        }
    }

    public float GetSFXVolume()
    {
        return currSFXVolume;
    }

    public void SetSFXVolume(float newVolume)
    {
        PlayerPrefs.SetFloat(playerPrefTypes.sfxVolume.ToString(), newVolume);
        currSFXVolume = newVolume;
        if (gameAnalytics != null)
        {
            gameAnalytics.LogEvent(GameAnalytics.gaEventCategories.UIEvent, "sfxVolume", "Set SFX volume", (System.Convert.ToInt64(currSFXVolume)));
        }

    }

    public float GetDifficultyLevel()
    {
        return difficultyLevel;
    }

    public void SetDifficulty(float newDifficulty)
    {
        PlayerPrefs.SetFloat(playerPrefTypes.difficultyLevel.ToString(), newDifficulty);
        difficultyLevel = newDifficulty;
        if (gameAnalytics != null)
        {
            gameAnalytics.LogEvent(GameAnalytics.gaEventCategories.UIEvent, "setDifficulty", "Set Difficulty", (System.Convert.ToInt64(difficultyLevel)));
        }
    }

    public float GetDefaultPaddleSpeed()
    {
        return defaultPaddleSpeed;
    }

    public int GetPlayerDefaultLives()
    {
        return defaultPlayerLives;
    }

    public int GetPlayerRemainingLives()
    {
        return playerLives;
    }

    private void SetPlayerRemainingLives(int lifeCount)
    {
        playerLives = lifeCount;
    }

    public void ResetPlayerLives()
    {
        playerLives = defaultPlayerLives;
    }

    public void LoseOneLife()
    {
        SetPlayerRemainingLives(playerLives - 1);
        if (gameAnalytics != null)
        {
            gameAnalytics.LogEvent(GameAnalytics.gaEventCategories.GameEvent, "playerDied", "Player Died", GetPlayerRemainingLives());
        }
        if (playerLives <= 0)
        {
            levelManager.LoadLevel("EndScreen");
        }
    }

    public void GainOneLife()
    {
        collect1UpSound.volume = GetSFXVolume();
        collect1UpSound.Play();
        SetPlayerRemainingLives(playerLives + 1);
        if (gameAnalytics != null)
        {
            gameAnalytics.LogEvent(GameAnalytics.gaEventCategories.GameEvent, "playerOneUp", "Player 1-up", GetPlayerRemainingLives());
        }
    }

    public float GetPlayerPaddleSpeed()
    {
        return playerPaddleSpeed;
    }

    public float GetPlayerBallStartSpeed()
    {
        return ballStartingVelocity;
    }

    public void SetPlayerPaddleSpeed(float newSpeed)
    {
        playerPaddleSpeed = newSpeed;
        if (gameAnalytics != null)
        {
            gameAnalytics.LogEvent(GameAnalytics.gaEventCategories.UIEvent, "setPaddleSpeed", "Set Paddle Speed", (System.Convert.ToInt64(GetPlayerPaddleSpeed())));
        }

    }

    public bool IsLeftPaddledown()
    {
        return leftPaddledown;
    }

    public bool IsRightPaddledown()
    {
        return rightPaddledown;
    }

    public bool IsUpPaddledown()
    {
        return upPaddledown;
    }

    public void SetPaddle(string name, bool isdown)
    {
        switch (name)
        {
            case "left":
                leftPaddledown = isdown;
                break;
            case "right":
                rightPaddledown = isdown;
                break;
            case "up":
                upPaddledown = isdown;
                break;

        }
    }

    public void FirePlayerProjectile()
    {
        if (playerOne != null)
        {
            playerOne.FireProjectile();
        }
    }


    public bool GetGAEnabled()
    {
        return gameAnalytics.IsAnalyticsEnabled();
    }

    public void SetGAEnabled(bool enableGA)
    {
        PlayerPrefs.SetFloat(playerPrefTypes.useGA.ToString(), (enableGA) ? 1 : 0);
        gameAnalytics.EnableAnalytics(enableGA);

        if (gameAnalytics != null)
        {
            gameAnalytics.LogEvent(GameAnalytics.gaEventCategories.UIEvent, "gaEnabled", "Google Analytics Toggled", ((useAI) ? 1 : 0));
        }

    }

}
