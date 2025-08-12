using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardEntryUI : MonoBehaviour
{
    public Image rankImage;
    public Image countryImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI score;
    public TextMeshProUGUI rankText;

    public void SetData(Sprite rank, Sprite country, string playerName, double scoreVal, int rankNumber)
    {
        rankImage.sprite = rank;
        countryImage.sprite = country;
        nameText.GetComponent<FixerForOnlyArabic>().fixedText = playerName;
        //nameText.GetComponent<FixerForOnlyArabic>().FixTextForUI();
        score.text = scoreVal.ToString();
        rankText.text = "#" + rankNumber.ToString();
    }
}

