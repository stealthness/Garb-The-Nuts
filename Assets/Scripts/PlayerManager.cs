using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject pouch;
    public PlayerDirection playerDirection = PlayerDirection.Down;
    private Vector2 prePos = Vector3.zero;
    public GameObject player;

    private bool hasEaten = false;

    //private Animation animation;
    private float nextWink = 1f;
    private PlayerState playerState;
    private float stunnedTime;

    public void Start()
    {
        playerState = PlayerState.Normal;
    }


    public void Update()
    {
        if (hasEaten)
        {
            IncreasePouchSize();
            hasEaten = false;
        }

        //if (nextWink < 0f)
        //{
            
        //    //animation = gameObject.GetComponent<Animation>();
        //    //if (animation != null)
        //    //{
        //    //animation.Play();
        //    //}

        //    nextWink = UnityEngine.Random.Range(2f, 10f);
        //}
        // nextWink -= Time.deltaTime;


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

    public void FixedUpdate()
    {
        if (playerState != PlayerState.Stunned)
        {
            return;
        }
        if (playerState == PlayerState.Stunned)
        {
            Debug.Log("Stunned");
            stunnedTime -= Time.fixedTime;
            if (stunnedTime < 0f)
            {
                stunnedTime = 2f;
                playerState = PlayerState.Normal;
            }
        }
    }

    public void StunPlayer()
    {
        playerState = PlayerState.Stunned;
    }

    public void IncreasePouch()
    {
        Debug.Log("Increase Pounch");        
        hasEaten=true;
    }

    public void ResetPouch()
    {
        pouch.gameObject.transform.localScale = Vector3.one;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
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
