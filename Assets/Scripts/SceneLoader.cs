using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    private bool isArabic = false;
    public void LoadScene(int levelIndex, AudioClip NewMusic){
        isArabic = PlayerPrefs.GetString("Language","English") == "Arabic";
        if (isArabic)
        {
            loadingBar.direction = Slider.Direction.RightToLeft;
        }else
        {
            loadingBar.direction = Slider.Direction.LeftToRight;
        }
        StartCoroutine(LoadSceneAsynchronously(levelIndex, NewMusic));
    }
    
    IEnumerator LoadSceneAsynchronously(int levelIndex, AudioClip NewMusic)
    {
        if (FindObjectOfType<MusicGO>() != null)
        {
            FindObjectOfType<MusicGO>().SwitchAudio(NewMusic);
        }
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        if (FindObjectOfType<MusicGO>() != null)
        {
            FindObjectOfType<MusicGO>().currentOperation = operation;
        }
        yield return new WaitForSeconds(0.2f);
        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
            
        }
    }
    public void LoadMenuAsynchronously(){
        isArabic = PlayerPrefs.GetString("Language","English") == "Arabic";
        if (isArabic)
        {
            loadingBar.direction = Slider.Direction.RightToLeft;
        }else
        {
            loadingBar.direction = Slider.Direction.LeftToRight;
        }
        StartCoroutine(LoadMenuAsynchronously_Wait());
    }
    
    IEnumerator LoadMenuAsynchronously_Wait()
    {
        if (FindObjectOfType<MusicGO>() != null)
        {
            FindObjectOfType<MusicGO>().GoBackToDefault();
        }
        
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        if (FindObjectOfType<MusicGO>() != null)
        {
            FindObjectOfType<MusicGO>().currentOperation = operation;
        }
        yield return new WaitForSeconds(0.2f);
        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
    public void LoadScene_WithTheSameMusic(int levelIndex){
        isArabic = PlayerPrefs.GetString("Language","English") == "Arabic";
        if (isArabic)
        {
            loadingBar.direction = Slider.Direction.RightToLeft;
        }else
        {
            loadingBar.direction = Slider.Direction.LeftToRight;
        }
        StartCoroutine(LoadSceneAsynchronously_WithTheSameMusic(levelIndex));
    }
    
    IEnumerator LoadSceneAsynchronously_WithTheSameMusic(int levelIndex)
    {
        if (FindObjectOfType<MusicGO>() != null)
        {
            FindObjectOfType<MusicGO>().Fade();
        }
        
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        if (FindObjectOfType<MusicGO>() != null)
        {
            FindObjectOfType<MusicGO>().currentOperation = operation;
        }
        yield return new WaitForSeconds(0.2f);
        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
}
