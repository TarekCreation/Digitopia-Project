using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DropDownScript : MonoBehaviour
{
    private GameObject _dropdownMenu;
    public GameObject text;
    public bool isArabic;

    // Start is called before the first frame update
    void Start()
    {
        _dropdownMenu = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        int menuIndex = _dropdownMenu.GetComponent<TMP_Dropdown>().value;
 
        
        List<TMP_Dropdown.OptionData> menuOptions = _dropdownMenu.GetComponent<TMP_Dropdown>().options;
 
        
        string value = menuOptions[menuIndex].text;
        
        
        if (isArabic)
        {
            text.GetComponent<ArabicFixerTMPRO>().ArabicText = value;
            text.GetComponent<ArabicFixerTMPRO>().EnglishText = value;
        }else
        {
            text.GetComponent<ArabicFixerTMPRO>().ArabicText = value;
            text.GetComponent<ArabicFixerTMPRO>().EnglishText = value;
        }
        
    }
}
