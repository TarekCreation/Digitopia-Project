using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI Stat1;
    public TextMeshProUGUI Stat2;
    public TextMeshProUGUI Stat3;
    public TextMeshProUGUI Stat4;
    public TextMeshProUGUI FinalScore;
    public GameObject NewHighScore;
    public GameObject[] ItemsToDisable;
    public string MinigameNumber = "1";

    public void UpdateData(int _1Stat, int? _2Stat = null, int? _3Stat = null, int? _4Stat = null)
    {
        Stat1.text = "x " + _1Stat.ToString();
        if (_2Stat != null)
        {
            Stat2.text = "x " + _2Stat.ToString();
        }
        else
        {
            Stat2.transform.parent.gameObject.SetActive(false);
        }
        if (_3Stat != null)
        {
            Stat3.text = "x " + _3Stat.ToString();

        }
        else
        {
            Stat3.transform.parent.gameObject.SetActive(false);
        }
        if (_4Stat != null)
        {
            Stat4.text = "x " + _4Stat.ToString();

        }
        else
        {
            Stat4.transform.parent.gameObject.SetActive(false);
        }
        FindObjectOfType<Score>().isDisabled = true;
        FinalScore.text = FindObjectOfType<Score>().score.ToString();
        foreach (GameObject go in ItemsToDisable)
        {
            go.SetActive(false);
        }
        if (FindObjectOfType<Score>().score > PlayerPrefs.GetInt("MiniGame"+ MinigameNumber +"_Score", 0))
        {

            PlayerPrefs.SetInt("MiniGame"+ MinigameNumber +"_Score", FindObjectOfType<Score>().score);
            NewHighScore.SetActive(true);
        }
    }
}
