using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutScript : MonoBehaviour
{
    private readonly float MIN_SPAWN_TIME = 1f;
    private readonly float MAX_SPWN_TIME = 3f;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, Random.Range(MIN_SPAWN_TIME, MAX_SPWN_TIME));
    }

}
