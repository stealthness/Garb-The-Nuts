using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public InputManager inputManager;
    public Rigidbody2D rb;
    public PlayerDirection playerDirection;
    public GameObject player;
    public GameObject pouch;
    

    private bool hasEaten = false;

    //private Animation animation;
    //private float _nextWink = 1f;

    private PlayerState _playerState;
    private float _stunnedTime;
    private readonly string _mushroomName = "Mushroom";


    public void Start()
    {
        playerDirection = PlayerDirection.Down;
        _playerState = PlayerState.Normal;
    }

    public void Update()
    {
        if (hasEaten)
        {
            IncreasePouchSize();
            hasEaten = false;
        }

    }

    private void IncreasePouchSize()
    {
        float pouchAdjustment;
        if (pouch.gameObject.transform.localScale.x < 1f)
        {
            pouchAdjustment = 0.08f;
        }
        else if (pouch.gameObject.transform.localScale.x < 2f)
        {
            pouchAdjustment = 0.04f;
        }
        else
        {
            pouchAdjustment = 0.02f;
        }
        pouch.gameObject.transform.localScale += new Vector3(1f, 1f, 0f) * pouchAdjustment;
    }

    public void MovePlayer(Vector3 playerMoveDirection, float movementSpeed)
    {
        if (_playerState != PlayerState.Stunned)
        {
            Vector2 deltamovement = playerMoveDirection * movementSpeed * Time.deltaTime;
            SetDirection(deltamovement);
            rb.MovePosition(inputManager.CheckMovement(rb.position + deltamovement));
        }
        if (_playerState == PlayerState.Stunned)
        {
            Debug.Log("Stunned");
            _stunnedTime -= Time.deltaTime;
            if (_stunnedTime < 0f)
            {
                _stunnedTime = 2f;
                _playerState = PlayerState.Normal;
            }
        }
    }

    public void StunPlayer()
    {
        if (_playerState == PlayerState.Stunned) return;
        if (rb == null)
        {
            Debug.Log("StunPlayer() --> rb is null", this);
        }
        rb.velocity = Vector3.zero;
        _playerState = PlayerState.Stunned;
    }

    public void IncreasePouch()
    {    
        hasEaten=true;
    }

    public void ResetPouch()
    {
        pouch.gameObject.transform.localScale = Vector3.one;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name, this);
        if (collision.name.StartsWith(_mushroomName))
        {
            StunPlayer();
        }

    }

    
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);    
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
}

public enum PlayerDirection { Left, Right, Up, Down }
public enum PlayerState { Normal, SpeedBoosted, Stunned }
