using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    private int score = 0;
    public TextMeshProUGUI scoreText;
    
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    // Start is called before the first frame update
    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score.ToString();
    }


    
}
