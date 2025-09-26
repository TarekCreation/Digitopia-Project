using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AlignSlider : MonoBehaviour
{
    HorizontalLayoutGroup horizontalLayoutGroup;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        horizontalLayoutGroup = gameObject.GetComponent<HorizontalLayoutGroup>();
        slider = gameObject.GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("Language","English") == "Arabic")
        {
            horizontalLayoutGroup.reverseArrangement = true;
            slider.direction = Slider.Direction.RightToLeft;
        }else
        {
            horizontalLayoutGroup.reverseArrangement = false;
            slider.direction = Slider.Direction.LeftToRight;
        }
    }
}
