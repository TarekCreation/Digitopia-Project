using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class lvlButton : MonoBehaviour
{
    public int number;
    public void loadScene()
    {
        FindObjectOfType<SceneLoader>().LoadScene(number, null);
    }
}
