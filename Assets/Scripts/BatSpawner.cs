using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    private List<Transform> spawnPoints = new List<Transform>();
    public GameObject[] enemyPrefabs;
    public float waitingTime = 10f;
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
        StartCoroutine(StartSpawning());
    }
    IEnumerator StartSpawning()
    {
        while (true)
        {
            int rndSpawn = Random.Range(0, spawnPoints.Count);
            int rndEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[rndEnemy], spawnPoints[rndSpawn].position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2f, waitingTime));

            if (waitingTime > 4f)
            {
                waitingTime -= 0.1f;
            }
        }
        
    }
}
