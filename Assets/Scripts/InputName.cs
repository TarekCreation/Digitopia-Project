using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputName : MonoBehaviour
{
    public string Myname;
    public AlwaysArabicTMPro tMPRO;
    public string lastName = "_";
    // Start is called before the first frame update
    void Start()
    {
        tMPRO.ValueChanged(PlayerPrefs.GetString("Player", "Unnamed"));
        GetComponent<TMP_InputField>().text = PlayerPrefs.GetString("Player", "Unnamed");
    }
    public void UpdateName(string name)
    {

        if (name != "")
        {
            Myname = name;
            PlayerPrefs.SetString("Player", Myname);
            tMPRO.ValueChanged(Myname);

        }
        else
        {
            Myname = name;
            tMPRO.ValueChanged(Myname);

        }

    }

    public void StartEditing()
    {
        lastName = GetComponent<TMP_InputField>().text;
    }

    public void FinishEditing(string name)
    {
        if (name != "")
        {
            Myname = name;
            PlayerPrefs.SetString("Player", Myname);
            tMPRO.ValueChanged(Myname);
        }else
        {
            Myname = lastName;
            GetComponent<TMP_InputField>().text = Myname;
            PlayerPrefs.SetString("Player", Myname);
            tMPRO.ValueChanged(Myname);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
