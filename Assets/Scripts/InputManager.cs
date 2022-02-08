using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private  GameObject player;

    private GameState gameState;
    private PlayerDirection playerDirection;
    private float movementSpeed;
    private int score;
    private GameManager gameManager;
    private float adjustment;
    private readonly float MIN_X = -5.75f;
    private readonly float MAX_X = 5.75f;
    private readonly float MIN_Y = -4.2f;
    private readonly float MAX_Y = 4.2f;

    public InputManager(Rigidbody2D rb, GameObject player)
    {
        this.rb = rb;
        this.player = player;
    }

    private void Awake()
    {

        gameManager = GetComponent<GameManager>();
        gameState = gameManager.gameState;
        playerDirection = gameObject.GetComponent<PlayerManager>().playerDirection;
        score = gameManager.score;
    }

    internal Vector3 GetUpdateMovement()
    {
        if (gameState != GameState.Started) return Vector3.zero;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 playerMovement = new Vector2(x, y);

        if (rb != null)
        {
            Vector2 deltaMovement = playerMovement * movementSpeed * Time.deltaTime;
            SetPlayDirection(deltaMovement);
            rb.MovePosition(CheckMovement(rb.position + deltaMovement));
        }

        return Vector3.up;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != GameState.Started) return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 playerMovement = new Vector2(x, y);

        if (rb != null)
        {
            Vector2 deltaMovement = playerMovement * movementSpeed * Time.deltaTime;
            SetPlayDirection(deltaMovement);
            rb.MovePosition(CheckMovement(rb.position + deltaMovement));
        }

    }



    internal void SetDirection(Vector2 deltaMovement)
    {
        if (deltaMovement.x > 0 && Mathf.Abs(deltaMovement.x) > Mathf.Abs(deltaMovement.y))
        {
            playerDirection = PlayerDirection.Right;
        }
        else if (deltaMovement.x < 0 && Mathf.Abs(deltaMovement.x) > Mathf.Abs(deltaMovement.y))
        {
            playerDirection = PlayerDirection.Left;
        }
        else if (deltaMovement.y > 0 && Mathf.Abs(deltaMovement.y) > Mathf.Abs(deltaMovement.x))
        {
            playerDirection = PlayerDirection.Up;
        }
        else
        {
            playerDirection = PlayerDirection.Down;
        }
    }

    private void SetPlayDirection(Vector2 deltaMovement)
    {
        player.GetComponent<PlayerManager>().SetDirection(deltaMovement);
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

    private Vector2 CheckMovement(Vector2 movement)
    {
        if (movement.x < MIN_X)
        {
            movement.x = MIN_X;
        }
        if (movement.x > MAX_X)
        {
            movement.x = MAX_X;
        }
        if (rb.position.y < MIN_Y)
        {
            movement.y = MIN_Y;
        }
        if (rb.position.y > MAX_Y)
        {
            movement.y = MAX_Y;
        }
        return movement;
    }
}
