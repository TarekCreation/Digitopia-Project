using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AlignDropDown : MonoBehaviour
{
    HorizontalLayoutGroup horizontalLayoutGroup;
   
    public GameObject EnglishDropDown;
    public GameObject ArabicDropDown;
    // Start is called before the first frame update
    void Start()
    {
        horizontalLayoutGroup = gameObject.GetComponent<HorizontalLayoutGroup>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("Language","English") == "Arabic")
        {
            ArabicDropDown.SetActive(true);
            EnglishDropDown.SetActive(false);
            horizontalLayoutGroup.reverseArrangement = true;
            
        }else
        {
            ArabicDropDown.SetActive(false);
            EnglishDropDown.SetActive(true);
            horizontalLayoutGroup.reverseArrangement = false;
            
        }
    }
    public void OnChangeValueArabic(int value)
    {
        EnglishDropDown.GetComponent<TMP_Dropdown>().value = value;
        
    }
    public void OnChangeValueEnglish(int value)
    {
        
        ArabicDropDown.GetComponent<TMP_Dropdown>().value = value;
    }
}
