using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrabSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject Crab;
    public Color[] colors;
    private List<Transform> positions = new List<Transform>();
    private float waitingTimeBetweenSpawns = 11f;
    // Start is called before the first frame update
    void Start()
    {
        positions = spawnPoints.ToList();
        StartCoroutine(Spawning());
    }
    IEnumerator Spawning()
    {
        yield return new WaitForSeconds(8);
        while (true)
        {
            if (positions.Count > 0)
            {
                int rndPos = Random.Range(0, positions.Count);

                GameObject crab = Instantiate(Crab, positions[rndPos].position, Quaternion.identity);
                int rndColor = Random.Range(0, colors.Length);
                crab.GetComponent<Crab>().ReColor(colors[rndColor]);
                positions.RemoveAt(rndPos);
                yield return new WaitForSeconds(waitingTimeBetweenSpawns);
            }
            else
            {
                positions = spawnPoints.ToList();
                int rndPos = Random.Range(0, positions.Count);

                GameObject crab = Instantiate(Crab, positions[rndPos].position, Quaternion.identity);
                int rndColor = Random.Range(0, colors.Length);
                crab.GetComponent<Crab>().ReColor(colors[rndColor]);
                positions.RemoveAt(rndPos);
                yield return new WaitForSeconds(waitingTimeBetweenSpawns);
            }
            if (waitingTimeBetweenSpawns > 1)
            {
                waitingTimeBetweenSpawns--;
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
