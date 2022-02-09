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
    private float _nextNuteSpawnDelayInSecs;
    private int _maxNumberofPrefabs = 30;

    private void Start()
    {
        bounds = new Bounds(Vector3.zero, new Vector3(10f, 8f, 0f));
        _nextNuteSpawnDelayInSecs = 30;
    }

    public void GenerateNut()
    {
        nuts.Add(Instantiate(prefabNut, GetNewPos(), Quaternion.Euler(0, 0, Random.Range(0, 12) * 60f)));
    }

    private Vector3 GetNewPos()
    {
        Vector3 newPos = new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), 0);
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
            if (nuts[i].activeSelf)
            {
                continue;
            }
            nuts[i].SetActive(true);
            nuts[i].transform.position = GetNewPos();
            hasEaten = true;
        }
        return hasEaten;
    }

    internal void Initialise(int maxNumberofPrefabs)
    {
        _maxNumberofPrefabs = maxNumberofPrefabs;
        for (int i = 0; i < maxNumberofPrefabs; i++)
        {
            GenerateNut();
        }
    }


    public void UpdateNutsList()
    {
        var cdTime = gameManager.GameCountdownTimeInSecs;
        if ( cdTime < 30 && nuts.Count < _maxNumberofPrefabs + (30 % cdTime))
        {
            if (_nextNuteSpawnDelayInSecs > cdTime)
            {
                GenerateNut();
                _nextNuteSpawnDelayInSecs -= 1f;
            }
        }
    }

}
