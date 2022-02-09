using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool testMode = false;
    private float startGameTime;

    private readonly float standardGameTime = 60f;
    private readonly float testGameTime = 5f;
    private readonly int maxNumberofPrefabs = 7;
    
    public int GameScore { get; set; }
    public float GameCountdownTimeInSecs { get; set; }

    public GameObject player;
    public GameState currentGameState;
    public AudioSource audioSource;
    public UIManager uiManager;
    public NutManager nutManager;
    public PlayerManager playerManager;


    void Start()
    {
        
        nutManager.Initialise(maxNumberofPrefabs);

        GameScore = 0;
        Time.timeScale = 0f;
        startGameTime = standardGameTime;
        if (testMode)
        {
            PlayerPrefs.SetInt("HighScore", 0);
            startGameTime = testGameTime;
        }
      
        currentGameState = GameState.Ended;
        GameCountdownTimeInSecs = startGameTime;
        uiManager.UpdateScoreAndTimeText(GameCountdownTimeInSecs, GameScore);
    }


    void Update()
    {

        if (currentGameState != GameState.Started)
        {
            return;
        }
        if (GameCountdownTimeInSecs < 0)
        {
            EndGame();
            currentGameState = GameState.Ended;
            return;
        }


        uiManager.UpdateScoreAndTimeText(GameCountdownTimeInSecs, GameScore);
        playerManager.MovePlayer(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        if (nutManager.CheckForNutEaten())
        {
            EatingIncreasing();
        }

        nutManager.UpdateNutsList();

        GameCountdownTimeInSecs -= Time.deltaTime;
    }

    private void EatingIncreasing()
    {
        GameScore += 10;
        playerManager.MovementSpeedAdjustment();
        playerManager.IncreasePouch();
    }





    public void EndGame()
    {
        audioSource.Stop();
        Time.timeScale = 0f;
        uiManager.ActivatePanel(Panels.Menu);
        int highScore = PlayerPrefs.GetInt("HighScore");
        string messageText = "Your Highscore remains at" + highScore;
        if ( highScore < GameScore)
        {
            highScore = GameScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            messageText = "Your new Highscore is " + highScore;
        }
        uiManager.SetText(Panels.HighScore, messageText);
    }


    public void StartGame()
    {
        audioSource.Play();
        GameCountdownTimeInSecs = startGameTime;
        GameScore = 0;
        playerManager.ResetPlayerManager();
        Time.timeScale = 1f;
        uiManager.ActivatePanel(Panels.HighScore);
        currentGameState = GameState.Started;
    }

}

public enum GameState
{
    Ended, Started
}