using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public Transform[] points;
    public GameObject[] blocksPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            int rnd = Random.Range(0, blocksPrefabs.Length);
            GameObject block = Instantiate(blocksPrefabs[rnd], points[i].position, blocksPrefabs[rnd].transform.rotation);
            block.GetComponent<Movable>().isTheStartingOne = true;
            block.GetComponent<Movable>().isLocked = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
