using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderVirusParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void DecreaseScore()
    {
        Score score = FindObjectOfType<Score>();
        if (score != null)
        {
            score.UpdateScore(-1);
        }
    }
}
