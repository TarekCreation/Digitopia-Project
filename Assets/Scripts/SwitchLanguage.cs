using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLanguage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
