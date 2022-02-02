using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool testMode = false;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScorePanelText;
    public GameObject highScorePanel;
    public GameObject instructionPanel;
    public GameObject menuPanel;

    public Rigidbody2D rb;
    private readonly float startMovementSpeed = 20f;
    private float startGameTime = 60;
    private float startAdjustment = 0.7f;
    private float movementSpeed;
    private Vector2 playerMovement;
        
    private readonly float MIN_X = -5.75f;
    private readonly float MAX_X = 5.75f;
    private readonly float MIN_Y = -4.2f;
    private readonly float MAX_Y = 4.2f;

    public GameObject prefabNut;
    private List<GameObject> nuts;
    private readonly int maxNumberofPrefabs = 7;
    private float adjustment;
    private float gameTime;
    private int score;

    public GameObject player;

    public GameState gameState;

    void Start()
    {
        adjustment = startAdjustment;
        nuts = new List<GameObject>();
        for (int i = 0; i < maxNumberofPrefabs; i++)
        {
            GenerateNut();
        }
        score = 0;
        Time.timeScale = 0f;
        if (testMode)
        {
            PlayerPrefs.SetInt("HighScore", 0);
            startGameTime = 5f;
        }
        gameState = GameState.Ended;
        movementSpeed = startMovementSpeed;
        gameTime = startGameTime;
        timeText.text = string.Format("Time : {0} ", (int)gameTime);
        scoreText.text = string.Format("Score : {0}", score);
    }

    private void GenerateNut()
    {
        Vector2 randomPos = new Vector2(Random.Range(MIN_X + 1, MAX_X - 1), Random.Range(MIN_Y + 1, MAX_Y - 1));
        nuts.Add(Instantiate(prefabNut, randomPos, Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {

        if (gameState == GameState.Started)
        {
            float x = Input.GetAxis("Horizontal"); 
            float y = Input.GetAxis("Vertical"); 
            playerMovement = new Vector2(x, y);

            if (rb != null)
            {
                Vector2 deltaMovement = playerMovement * movementSpeed * Time.deltaTime;
                rb.MovePosition(CheckMovement(rb.position + deltaMovement));
            }


            for (int i = 0; i < nuts.Count; i++)
            {
                if (nuts[i] == null)
                {
                    nuts.RemoveAt(i);
                    score += 10;
                    movementSpeed = MovementSpeedAdjustment();
                    player.GetComponent<PlayerManager>().IncreasePouch();
                    GenerateNut();
                }
            }

            gameTime -= Time.deltaTime;
            if (gameTime < 0 )
            {
                EndGame();
                gameState = GameState.Ended;
            }        
            timeText.text = string.Format("Time : {0} ", (int)gameTime);
            scoreText.text = string.Format("Score : {0}", score);
        }
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
        int highScore = PlayerPrefs.GetInt("HighScore");
        menuPanel.SetActive(true);
        if ( highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScorePanelText.text = "Your new Highscore is " + highScore;
        }
        else
        {
            highScorePanelText.text = "Your Highscore remains at " + highScore;
        }
    }


    public void StratGame()
    {
        player.GetComponent<PlayerManager>().ResetPouch();
        gameTime = startGameTime;
        score = 0;
        movementSpeed = startMovementSpeed;
        Time.timeScale = 1f;
        instructionPanel.SetActive(false);
        highScorePanel.SetActive(true);
        menuPanel.SetActive(false);
        gameState = GameState.Started;
    }


    private Vector2 CheckMovement(Vector2 movement)
    {
        if (movement.x < MIN_X )
        {
            movement.x = MIN_X;
        }
        if (movement.x > MAX_X)
        {
            movement.x = MAX_X;
        }
        if (rb.position.y  < MIN_Y )
        {
            movement.y = MIN_Y;
        }
        if (rb.position.y  > MAX_Y )
        {
            movement.y = MAX_Y;
        }
        return movement;
    }
}

public enum GameState
{
    Ended, Started
}