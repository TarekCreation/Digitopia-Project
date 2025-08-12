using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonAlignLogoAndText : MonoBehaviour
{
    HorizontalLayoutGroup horizontalLayoutGroup;
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
            horizontalLayoutGroup.reverseArrangement = true;
        }else
        {
            horizontalLayoutGroup.reverseArrangement = false;
        }
    }
}
