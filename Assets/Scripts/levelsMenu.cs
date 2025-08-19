using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelsMenu : MonoBehaviour
{
    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        //////Temporary//////
        PlayerPrefs.SetInt("levelAt", 2); 
        int levelAt = PlayerPrefs.GetInt("levelAt", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i + 1 > levelAt)
            {
                buttons[i].interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
