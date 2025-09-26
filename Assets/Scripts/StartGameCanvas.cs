using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameCanvas : MonoBehaviour
{
    public GameObject items;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("DidStartGameBefore", "No") == "No")
        {
            items.SetActive(true);
        }else
        {
            FindObjectOfType<SceneLoader>().LoadMenuAsynchronously();
        }
    }
}
