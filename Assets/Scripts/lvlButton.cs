using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class lvlButton : MonoBehaviour
{
    public int number;
    public void loadScene()
    {
        FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.SelectMinigame);
        FindObjectOfType<SceneLoader>().LoadScene(number, null);
    }
}
