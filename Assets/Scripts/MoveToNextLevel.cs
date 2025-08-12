using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    public int nextSceneLoad;
    public int levelsCount = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        levelsCount = SceneManager.sceneCountInBuildSettings;
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == levelsCount - 2)
        {
            loadWinningScene();
        }else
        {
            SceneManager.LoadScene(nextSceneLoad);
            if (nextSceneLoad > PlayerPrefs.GetInt("levelAt" , 1))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }
    }
    public void LoadMenuAndSave()
    {
        
        SceneManager.LoadScene(0);
        if (nextSceneLoad > PlayerPrefs.GetInt("levelAt" , 1))
        {
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        }
        
    }
    public void loadWinningScene()
    {
        PlayerPrefs.SetInt("levelAt", levelsCount - 1);
        SceneManager.LoadScene(levelsCount - 1);
        
    }
}
