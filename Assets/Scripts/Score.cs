using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public int score = 0;
    public ArabicFixerTMPRO scoreText;
    public bool isDisabled = false;

    void Start()
    {
        scoreText.ValueChanged_PlusArabic("Score: " + score.ToString(), "النقاط: " + score.ToString());
        UpdateScore(0);
        StartCoroutine(WaitAndUpdate());

    }
    IEnumerator WaitAndUpdate()
    {
        yield return new WaitForSeconds(1f);
        scoreText.ValueChanged_PlusArabic("Score: " + score.ToString(), "النقاط: " + score.ToString());
        UpdateScore(0);
    }
    // Start is called before the first frame update
    public void UpdateScore(int value)
    {
        if (!isDisabled)
        {
            score += value;
            if (score < 0)
            {
                score = 0;
            }
            scoreText.ValueChanged_PlusArabic("Score: " + score.ToString(), "النقاط: " + score.ToString());
        }



    }


    
}
