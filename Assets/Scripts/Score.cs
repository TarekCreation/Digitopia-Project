using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    // Start is called before the first frame update
    public void UpdateScore(int value)
    {
        
        score += value;
        if (score < 0)
        {
            score = 0;
        }
        scoreText.text = "Score: " + score.ToString();
        
        
    }


    
}
