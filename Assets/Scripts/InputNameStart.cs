using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNameStart : MonoBehaviour
{
    public string Myname;
    public Button button;
    // Start is called before the first frame update
    public void UpdateName(string name)
    {
        
        if (name != "")
        {
            button.interactable = true;
            Myname = name;
            PlayerPrefs.SetString("Player", Myname);
        }else
        {
            button.interactable = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
