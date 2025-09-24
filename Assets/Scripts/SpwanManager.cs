using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    private List<Transform> spawnPoints = new List<Transform>();
    public GameObject[] enemyPrefabs;
    public GameObject BossPrefab;
    public int numberOfKilledEnemies_V1 = 0;
    public int numberOfKilledEnemies_V2 = 0;
    public int numberOfKilledEnemies_V3 = 0;
    public int numberOfKilledEnemies_V4 = 0;
    public int enemyCount = 100;
    public float waitingTimeBetweenSpawn = 2f;
    public Transform[] BossSpawnPoints;
    private int currentDifficulty = 1;
    bool AbossIsAlive = false;
    public void IncreaseKilledEnemyCount(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Virus1:
                numberOfKilledEnemies_V1++;
                break;
            case EnemyType.Virus2:
                numberOfKilledEnemies_V2++;
                break;
            case EnemyType.Virus3:
                numberOfKilledEnemies_V3++;
                break;
            case EnemyType.Boss:
                numberOfKilledEnemies_V4++;
                AbossIsAlive = false;
                break;
        }
    }
    
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
        if (waitingTimeBetweenSpawn < 5f)
        {
            waitingTimeBetweenSpawn += 0.2f;
        }
        
        currentDifficulty++;
        int rndEnemy = 0;
        if (currentDifficulty < 5)
        {
            rndEnemy = 0;
        }
        else if (currentDifficulty >= 5 && currentDifficulty < 20)
        {
            int integer = Random.Range(0, 20);
            if (integer < 5)
            {
                rndEnemy = 1;
            }else
            {
                rndEnemy = 0;
            }
            
        }
        else if (currentDifficulty >= 20 && currentDifficulty < 35)
        {
            int integer = Random.Range(0, 30);
            if (integer < 8)
            {
                rndEnemy = 1;
            }else if (integer >= 8 && integer < 15)
            {
                rndEnemy = 2;
            }
            else
            {
                rndEnemy = 0;
            }
        }
        else
        {
            if (Random.Range(0, 10) > 7 && AbossIsAlive == false)
            {
                
                int Spawnindex = Random.Range(0, BossSpawnPoints.Length);
                Instantiate(BossPrefab, BossSpawnPoints[Spawnindex].position, Quaternion.identity);
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.enemyAppear2,0.3f);
                AbossIsAlive = true;
                return;
                
                
            }else
            {
                int integer = Random.Range(0, 30);
                if (integer < 8)
                {
                    rndEnemy = 1;
                }else if (integer >= 8 && integer < 15)
                {
                    rndEnemy = 2;
                }
                else
                {
                    rndEnemy = 0;
                }
            }
            
            
        }
        if (currentDifficulty < 50)
        {
            if (!AbossIsAlive)
            {
                int index = Random.Range(0, spawnPoints.Count);

                Instantiate(enemyPrefabs[rndEnemy], spawnPoints[index].position, Quaternion.identity);
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.enemyAppear2, 0.2f);
            }
        }
        else if (currentDifficulty >= 50 && currentDifficulty < 100)
        {
            int index = Random.Range(0, spawnPoints.Count);

            Instantiate(enemyPrefabs[rndEnemy], spawnPoints[index].position, Quaternion.identity);
            FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.enemyAppear2,0.2f);
        }
        else
        {
            int numberOfEnemiesToSpawn = Random.Range(1, Mathf.RoundToInt(currentDifficulty / 50));
            for (int i = 0; i < numberOfEnemiesToSpawn; i++)
            {
                int index = Random.Range(0, spawnPoints.Count);

                Instantiate(enemyPrefabs[rndEnemy], spawnPoints[index].position, Quaternion.identity);
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.enemyAppear2,0.2f);
            }
        }
        
        
        
    }
}
