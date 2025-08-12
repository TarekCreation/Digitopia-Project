using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class lvlButton : MonoBehaviour
{
    public int number;
    public GameObject Music;
    public void loadScene()
    {
        DontDestroyOnLoad(Music);
        SceneManager.LoadScene(number);
    }
}
