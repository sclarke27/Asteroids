using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHUD : MonoBehaviour
{

    public GameAnalytics gameAnalytics;
    public string scoreText = "Score: ";
    public Text scoreTextField;
    public string livesText = "Lives: ";
    public Text livesTextField;
    public string pausedText = "Game\nPaused";
    public Text pausedTextField;
    public Text pausedTextFieldShadow;
    public Button restartButton;
    public Button quitButton;
    public Slider difficultySlider;
    public bool isMenuScreen;
    public bool isStartMenu = false;
    public bool isEndScreen = false;
    public Texture2D defaultCursor;
    public bool isFirstLevel = false;

    public GameObject pausePanel;
    public GameObject readyPanel;
    public GameObject levelCompletePanel;
    public GameObject instructionsPanel;
    public GameObject highScorePanel;
    public GameObject nameInputPanel;
    public GameObject mobileOverlayPanel;
    public Text highScoreNames;
    public Text highScoreScores;
    public InputField playerNameInput;
    public Button paddleLeftUIButton;
    public Button paddleRightUIButton;

    private GameData gameData;
    //private LevelManager levelManager;

    private string nextLevel;
    //private Ball ball;



    void Awake()
    {
        gameData = GameObject.FindObjectOfType<GameData>();
        //levelManager = GameObject.FindObjectOfType<LevelManager>();

        if (!isMenuScreen && (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
        {
            mobileOverlayPanel.SetActive(true);
        }
        else if (!isMenuScreen)
        {
            mobileOverlayPanel.SetActive(false);
        }

    }

    // Use this for initialization
    void Start()
    {
        if (!isMenuScreen)
        {
            scoreTextField.text = "";
            livesTextField.text = "";
            pausedTextField.text = "";
            pausedTextFieldShadow.text = "";
            pausePanel.SetActive(false);
            readyPanel.SetActive(false);
            instructionsPanel.SetActive(false);
            //ball = GameObject.FindObjectOfType<Ball>();
            gameAnalytics.LogScreen(Application.loadedLevelName + "Game Screen");

        }
        else
        {
            if (isStartMenu)
            {
                gameAnalytics.LogScreen("Start Screen");
                SetupHighScoresPanel();
                difficultySlider.value = gameData.GetDifficultyLevel();
            }
            if (isEndScreen)
            {
                gameAnalytics.LogScreen("End Screen");
                SetupHighScoresPanel();
                scoreTextField.text = gameData.GetPlayerScore().ToString();
                //if player got high score, show name dialog instead of loading next level
                if (gameData.GetPlayerScoreRank() < 26)
                {
                    //gameData.SavePlayerScore();
                    ToggleHighScoreNameDialog(true, "");
                    return;
                }

            }
        }

    }

    public void HandlePaddleUIButtons(string buttonEvent)
    {
        switch (buttonEvent)
        {
            case "leftUp":
                gameData.SetPaddle("left", false);
                break;
            case "leftDown":
                gameData.SetPaddle("left", true);
                break;
            case "rightUp":
                gameData.SetPaddle("right", false);
                break;
            case "rightDown":
                gameData.SetPaddle("right", true);
                break;
            case "thrustUp":
                gameData.SetPaddle("up", false);
                break;
            case "thrustDown":
                gameData.SetPaddle("up", true);
                break;
            case "fire":
                gameData.FirePlayerProjectile();
                break;
        }
    }



    private void SetupHighScoresPanel()
    {
        ArrayList highScores = gameData.GetHighScores();
        int scoreCount = (highScores.Count > 0) ? int.Parse(highScores[0].ToString().Split(',')[1]) : 0;
        if (scoreCount == 0)
        {
            if (instructionsPanel != null)
            {
                instructionsPanel.SetActive(true);
            }
            highScorePanel.SetActive(false);
        }
        else
        {
            if (instructionsPanel != null)
            {
                instructionsPanel.SetActive(false);
            }
            highScorePanel.SetActive(true);
            highScoreNames.text = "";
            highScoreScores.text = "";
            foreach (string score in highScores)
            {
                string[] scoreData = score.Split(',');
                highScoreNames.text += scoreData[0] + "\n";
                highScoreScores.text += scoreData[1] + "\n";
            }
        }
    }

    public void ClearHighScores()
    {
        gameData.DeleteHighScores();
        if (isStartMenu)
        {

            SetupHighScoresPanel();
        }
    }

    public void ToggleHighScoreNameDialog(bool showDialog, string levelName)
    {
        if (levelName != "")
        {
            nextLevel = levelName;
        }
        if (showDialog)
        {
            Screen.showCursor = true;
        }
        else
        {
            Screen.showCursor = false;
        }
        nameInputPanel.SetActive(showDialog);
    }

    public void SavePlayerHighScore()
    {
        if (playerNameInput.text != "")
        {
            Screen.showCursor = true;
            gameData.SavePlayerScore(playerNameInput.text);
            ToggleHighScoreNameDialog(false, "");
            if (nextLevel != "" && nextLevel != null)
            {
                Application.LoadLevel(nextLevel);
            }
        }
        if (isEndScreen)
        {
            SetupHighScoresPanel();
        }
        if (gameAnalytics != null)
        {
            gameAnalytics.LogEvent(GameAnalytics.gaEventCategories.GameEvent, "newHighScore", "New High Score", System.Convert.ToInt64(gameData.GetPlayerScore()));
        }
    }

    public void CancelPlayerHighScore()
    {
        ToggleHighScoreNameDialog(false, "");
        if (!isEndScreen)
        {
            Application.LoadLevel(nextLevel);
        }
    }

    public void HandlePlayerReady()
    {
        Screen.showCursor = false;
        gameData.SetInstructionsViewed();
        gameData.SetPlayerReady(true);
        //ball.LaunchBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMenuScreen)
        {
            //populate HUD with default values
            int playerScore = gameData.GetPlayerScore();
            scoreTextField.text = ((playerScore > 9999) ? playerScore.ToString() : (scoreText + playerScore));
            livesTextField.text = livesText + gameData.GetPlayerRemainingLives();

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || gameData.IsLeftPaddledown())
            {
                gameData.SetPaddle("left", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || !gameData.IsLeftPaddledown())
            {
                gameData.SetPaddle("left", false);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || gameData.IsRightPaddledown())
            {
                gameData.SetPaddle("right", true);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || !gameData.IsRightPaddledown())
            {
                gameData.SetPaddle("right", false);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                gameData.SetPaddle("up", true);
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
            {
                gameData.SetPaddle("up", false);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                gameData.FirePlayerProjectile();
            }

            if (!gameData.IsPlayerReady())
            {

                //wait for player to hit space bar
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    HandlePlayerReady();
                }

                //if player has not seen the instructions, show them else show ready panel
                if (isFirstLevel && !gameData.PlayerViewedInstructions())
                {
                    instructionsPanel.SetActive(true);
                    readyPanel.SetActive(false);

                }
                else
                {
                    instructionsPanel.SetActive(false);
                    readyPanel.SetActive(true);
                }

            }
            else
            {
                //listen for pause keys
                if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
                {
                    gameData.PauseGame(!gameData.IsGamePaused());
                    if (gameData.IsGamePaused())
                    {
                        Screen.showCursor = true;
                    }
                    else
                    {
                        Screen.showCursor = false;
                    }
                }



                //if game is paused, show the paused panel else dont
                if (gameData.IsGamePaused())
                {
                    pausePanel.SetActive(true);
                    pausedTextField.text = pausedText;
                    pausedTextFieldShadow.text = pausedText;
                }
                else
                {
                    pausePanel.SetActive(false);
                    pausedTextField.text = "";
                    pausedTextFieldShadow.text = "";

                }

                //hide other panels
                readyPanel.SetActive(false);
                instructionsPanel.SetActive(false);
            }
        }
        else
        {
            //we are in a menu screen
            Screen.showCursor = true;
        }
    }

    public void ShowLevelComplete()
    {
        Screen.showCursor = true;
        levelCompletePanel.SetActive(true);
    }
}
