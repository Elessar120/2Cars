using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RightSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject blueScoreObject;
    [SerializeField] private GameObject blueObstacleObject;
    [SerializeField] private GameObject PowerUp1;

    private void Start()
    {
        InvokeRepeating("Spawner", 1f, 2f);
    }

    private void Spawner()
    {
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        float rand = Random.value;
        if (rand >= 0.5f)
        {
            Instantiate(blueScoreObject, spawnPoint.transform.position, Quaternion.identity);
        }
        else if( rand<.5f)
        {
            Instantiate(blueObstacleObject, spawnPoint.transform.position, Quaternion.identity);
        }
         /*else 
        {
            Instantiate(PowerUp1, spawnPoint.transform.position, Quaternion.identity);
        }*/
    }
}