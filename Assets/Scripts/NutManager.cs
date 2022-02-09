using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutManager : MonoBehaviour
{

    List<GameObject> nuts = new List<GameObject>();
    public GameObject prefabNut;
    public PlayerManager playerManager;
    public GameManager gameManager;

    private Bounds bounds;


    private void Start()
    {
        bounds = gameManager.gameBounds;
    }

    public void GenerateNut()
    {
        nuts.Add(Instantiate(prefabNut, GetNewPos(), Quaternion.Euler(0, 0, Random.Range(0, 12) * 60f)));
    }

    private Vector3 GetNewPos()
    {
        Vector3 newPos = new Vector3(Random.Range(bounds.min.x + 1, bounds.max.x + 1 - 1), Random.Range(bounds.min.y + 1, bounds.max.y - 1), 0);
        bool found = false;
        while (found)
        {
            if (Vector3.SqrMagnitude(newPos - playerManager.transform.position) < 1f)
            {
                continue;
            }
            foreach (GameObject nut in nuts)
            {
                if (Vector3.SqrMagnitude(newPos - nut.transform.position) < 0.5f)
                {
                    continue;
                }
            }
            found = true;
        }

        return newPos;
    }

    public bool CheckForNutEaten()
    {
        bool hasEaten = false;
        for (int i = 0; i < nuts.Count; i++)
        {
            if (!nuts[i].activeSelf)
            {
                nuts[i].SetActive(true);
                nuts[i].transform.position = GetNewPos();
                hasEaten = true;
            }
        }
        return hasEaten;
    }
    
}
