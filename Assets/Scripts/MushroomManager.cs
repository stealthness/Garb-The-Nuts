using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomManager : MonoBehaviour
{
    private PlayerManager player;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Mushroom hit");
        player.StunPlayer();
    }
}

class Mushroom : ScriptableObject
{
    Vector3 position;
    float growthTime = 4f; 



}
