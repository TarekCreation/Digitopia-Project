using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpySpawner : MonoBehaviour
{
    private List<Transform> spawnPoints = new List<Transform>();
    public GameObject[] enemyPrefabs;
    public float waitingTime = 5f;
    private int enemiesThatCanSpawn = 2;
    private int counter = 3;
    
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
            if (enemiesThatCanSpawn <= enemyPrefabs.Length)
            {
                int rndEnemy = Random.Range(0, enemiesThatCanSpawn);
                Instantiate(enemyPrefabs[rndEnemy], spawnPoints[rndSpawn].position, Quaternion.identity);
            }
            else
            {
                int rndEnemy = Random.Range(0, enemyPrefabs.Length);
                Instantiate(enemyPrefabs[rndEnemy], spawnPoints[rndSpawn].position, Quaternion.identity);
            }
            
            
            yield return new WaitForSeconds(Random.Range(0.5f, waitingTime));

            if (waitingTime > 2f)
            {
                waitingTime -= 0.1f;
            }
            if (enemyPrefabs.Length > enemiesThatCanSpawn)
            {
                counter--;
                if (counter == 0)
                {
                    counter = 3;
                    enemiesThatCanSpawn++;
                }
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
