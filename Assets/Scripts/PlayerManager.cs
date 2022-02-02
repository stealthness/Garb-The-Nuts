using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameObject pouch;
    private bool hasEaten = false;

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
}
