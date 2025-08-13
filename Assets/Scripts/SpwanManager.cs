using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    private List<Transform> spawnPoints = new List<Transform>();
    public GameObject enemyPrefab;
    public int enemyCount = 100;
    public float waitingTimeBetweenSpawn = 2f;
    private int NumberOfSpwanedEnemies = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in GetComponentsInChildren<Transform>())
        {
            if (item != transform)
            {
                spawnPoints.Add(item);
            }
        }
        InvokeRepeating("SpawnEnemies", 2f, waitingTimeBetweenSpawn);
    }

    // Update is called once per frame
    public void SpawnEnemies()
    {
        if (NumberOfSpwanedEnemies < enemyCount)
        {
            NumberOfSpwanedEnemies++;
            int index = Random.Range(0, spawnPoints.Count);
            Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.identity);
        }
    }
}
