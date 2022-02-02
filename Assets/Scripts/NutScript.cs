using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutScript : MonoBehaviour
{
    private readonly float MIN_SPAWN_TIME = 1f;
    private readonly float MAX_SPWN_TIME = 3f;

    private void Start()
    {
        Debug.Log("NUTTY TIME");
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {      
        gameObject.SetActive(false);
    }






}
