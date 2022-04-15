using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject redScoreObject;
    [SerializeField] private GameObject redObstacleObject;
    [SerializeField] private GameObject PowerUp1;
    private void Start()
    {
        InvokeRepeating("Spawner", .5f, 2f);
    }

    private void Spawner()
    {
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        float rand = Random.value;
        if (rand <= 0.5f)
        {
            Instantiate(redScoreObject, spawnPoint.transform.position, Quaternion.identity);
        }
        else if(rand>.5f)
        {
            Instantiate(redObstacleObject, spawnPoint.transform.position, Quaternion.identity);
        }
        /*else 
        {
            Instantiate(PowerUp1, spawnPoint.transform.position, Quaternion.identity);
        }*/
    }
}