using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;
public class SpaceRemover : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject _meassage;
 
    void Start()
    {
        
        inputField.onValueChanged.AddListener(delegate { RemoveSpaces(); });
    }
    
    void RemoveSpaces()
    {
        if (inputField.text.Contains(" ") || inputField.text.Contains("*") || inputField.text.Contains("%") || inputField.text.Contains("$") || inputField.text.Contains("#") || inputField.text.Contains("@"))
        {
            inputField.text = inputField.text.Replace(" ", "");
            inputField.text = inputField.text.Replace("*", "");
            inputField.text = inputField.text.Replace("%", "");
            inputField.text = inputField.text.Replace("$", "");
            inputField.text = inputField.text.Replace("#", "");
            inputField.text = inputField.text.Replace("@", "");
            _meassage.SetActive(true);
            _meassage.GetComponent<Animator>().Play("Message");
        }
        
        //inputField.text = inputField.text.Replace("!", "");
        //inputField.text = inputField.text.Replace("^", "");
    }
}
