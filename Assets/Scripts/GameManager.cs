using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool testMode = true;

    public Rigidbody2D rb;
    private readonly float startMovementSpeed = 20f;
    private float startGameTime;
    private float startAdjustment = 0.7f;
    private float movementSpeed;
    private Vector2 playerMovement;

    //private readonly float MIN_X = -5.75f;
    private readonly float MAX_X = 5.75f;
    //private readonly float MIN_Y = -4.2f;
    private readonly float MAX_Y = 4.2f;

    private readonly float standardGameTime = 60f;
    private readonly float testGameTime = 5f;


    private Vector3 centerOfBounds;
    private Vector3 cournBounds;

    public Bounds gameBounds;




    public GameObject prefabNut;
    private List<GameObject> nuts;
    private readonly int maxNumberofPrefabs = 7;
    private float adjustment;
    private float gameTime;
    public int score { get; set; }
    public GameObject player;
    public GameState gameState;

    public UIManager uiManager;
    public NutManager nutManager;
    public PlayerManager playerManager;
    private float nextNutDelay = 30f;

    private void Awake()
    {
        
    }


    void Start()
    {
        
        nuts = new List<GameObject>();
        for (int i = 0; i < maxNumberofPrefabs; i++)
        {
            nutManager.GenerateNut();
        }

        centerOfBounds = Vector3.zero;
        cournBounds = new Vector3(MAX_X * 2f, MAX_Y * 2f, 0f);
        gameBounds = new Bounds(centerOfBounds, cournBounds);

        adjustment = startAdjustment;

        score = 0;
        Time.timeScale = 0f;
        startGameTime = standardGameTime;
        movementSpeed = startMovementSpeed;
        if (testMode)
        {
            PlayerPrefs.SetInt("HighScore", 0);
            startGameTime = testGameTime;
        }
      
        gameState = GameState.Ended;
        gameTime = startGameTime;
        uiManager.UpdateScoreAndTimeText(gameTime, score);
    }



    private Vector3 GetNewPos()
    {
        Vector3 newPos = new Vector3(Random.Range(gameBounds.min.x + 1, gameBounds.max.x + 1 - 1), Random.Range(gameBounds.min.y + 1, gameBounds.max.y - 1), 0);
        bool found = false;
        while (found)
        {
            if (Vector3.SqrMagnitude(newPos - player.transform.position) < 1f)
            {
                continue;
            }
            foreach (GameObject nut in nuts)
            {
                if (Vector3.SqrMagnitude(newPos - nut.transform.position) < 0.5f)
                {
                    continue;
                }
            }
            found = true;
        }

        return newPos;
    }

    public void UpdateNuts()
    {
        for (int i = 0; i < nuts.Count; i++)
        {
            if (!nuts[i].activeSelf)
            {
                nuts[i].SetActive(true);
                nuts[i].transform.position = GetNewPos();
                score += 10;
                movementSpeed = MovementSpeedAdjustment();
                playerManager.IncreasePouch();
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (gameState != GameState.Started)
        {
            return;
        }

        playerManager.MovePlayer(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), movementSpeed);

        if (nutManager.CheckForNutEaten())
        {
            EatingIncreasing();
        }

        if (gameTime < 30 && nuts.Count < maxNumberofPrefabs + (30 % gameTime))
        {
            if (nextNutDelay > gameTime)
            {
                nutManager.GenerateNut();
                nextNutDelay -= 1f;
            }
        }

        gameTime -= Time.deltaTime;
        if (gameTime < 0)
        {
            EndGame();
            gameState = GameState.Ended;
        }
        uiManager.UpdateScoreAndTimeText(gameTime, score);
    }

    private void EatingIncreasing()
    {
        score += 10;
        movementSpeed = MovementSpeedAdjustment();
        playerManager.IncreasePouch();
    }



    public float MovementSpeedAdjustment()
    {
        Debug.Log(movementSpeed);
        if (score < 100)
            return movementSpeed -= adjustment;
        else if (score < 200)
            return movementSpeed -= adjustment * 0.5f;
        else if (score < 300)
            return movementSpeed -= adjustment * 0.4f;
        else if (score < 400)
            return movementSpeed -= adjustment * 0.3f;
        else if (score < 500)
            return movementSpeed -= adjustment * 0.2f;

        if (movementSpeed < 2f)
            return 2f; ;

        return movementSpeed -= 0.1f;
               
    }

    public void EndGame()
    {  
        Time.timeScale = 0f;
        uiManager.ActivatePanel(Panels.Menu);
        int highScore = PlayerPrefs.GetInt("HighScore");
        string messageText = "Your Highscore remains at" + highScore;
        if ( highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            messageText = "Your new Highscore is " + highScore;
        }
        uiManager.SetText(Panels.HighScore, messageText);
    }


    public void StratGame()
    {
        playerManager.ResetPouch();
        gameTime = startGameTime;
        score = 0;
        movementSpeed = startMovementSpeed;
        Time.timeScale = 1f;
        uiManager.ActivatePanel(Panels.HighScore);
        gameState = GameState.Started;
    }


    private Vector2 CheckMovement(Vector2 movement)
    {
        if (gameBounds.Contains(movement))
        {
            return movement;
        }
        else
        {
            return gameBounds.ClosestPoint(movement);
        }
    }
}

public enum GameState
{
    Ended, Started
}