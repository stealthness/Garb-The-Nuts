using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public  GameObject player;
    public GameManager gameManager;
    public PlayerManager playerManager;

    private GameState gameState;
    private PlayerDirection playerDirection;
    private float movementSpeed;
    private int score;
    private float adjustment;
    private readonly float MAX_X = 5.75f;
    private readonly float MAX_Y = 4.2f;

    private Vector3 centerOfBounds;
    private Vector3 cournBounds;

    private Bounds gameBounds;
    

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

        playerDirection = playerManager.playerDirection;
        gameState = gameManager.currentGameState;
        score = gameManager.GameScore;
        centerOfBounds = Vector3.zero;
        cournBounds = new Vector3(MAX_X * 2f, MAX_Y * 2f, 0f);
        gameBounds = new Bounds(centerOfBounds, cournBounds);
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

    public Vector2 CheckMovement(Vector2 movement)
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
