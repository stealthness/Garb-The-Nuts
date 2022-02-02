using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject pouch;
    private bool hasEaten = false;
    public PlayerDirection playerDirection = PlayerDirection.Down;
    private Vector2 prePos = Vector3.zero;


    public void Update()
    {
        if (hasEaten)
        {
            if (pouch.gameObject.transform.localScale.x < 1f)
            {
                pouch.gameObject.transform.localScale += new Vector3(0.08f, 0.08f, 0f);
            }else if (pouch.gameObject.transform.localScale.x < 2f)
            {
                pouch.gameObject.transform.localScale += new Vector3(0.04f, 0.04f, 0f);
            }
            else if (pouch.gameObject.transform.localScale.x < 3f)
            {
                pouch.gameObject.transform.localScale += new Vector3(0.02f, 0.02f, 0f);
            }
            
            hasEaten = false;
        }

        Debug.Log(playerDirection.ToString());
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
