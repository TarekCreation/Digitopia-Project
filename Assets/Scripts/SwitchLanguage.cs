using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLanguage : MonoBehaviour
{
    
    public void switchLanguage()
    {
        if (PlayerPrefs.GetString("Language","English") == "Arabic")
        {
            PlayerPrefs.SetString("Language", "English");
        }else
        {
            PlayerPrefs.SetString("Language", "Arabic");
        }
    }
}
