using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomManager : MonoBehaviour
{
    public GameManager gameManager;

    private PlayerManager player;
    private List<GameObject> gameObjects = new List<GameObject>();
    private float _nextMushroomSpawnInSecs;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }



    // Start is called before the first frame update
    void Start()
    {
        _nextMushroomSpawnInSecs = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GameCountdownTimeInSecs < 60 - _nextMushroomSpawnInSecs)
        {

        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Mushroom hit");
        player.StunPlayer();
        collision.gameObject.SetActive(false);
    }
}

class Mushroom : ScriptableObject
{
    Vector3 position;
    float growthTime = 4f; 



}
