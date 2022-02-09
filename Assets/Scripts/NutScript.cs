using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutScript : MonoBehaviour
{
    private readonly float MIN_SPAWN_TIME = 1f;
    private readonly float MAX_SPWN_TIME = 3f;
    public GameObject nutPrefab;
    public NutState nutState;


    private void Start()
    {
        Debug.Log("NUTTY TIME");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Nutscript hit " +collision);
    }

}

public enum NutState
{
    Falling, Landed, Hidden
}
