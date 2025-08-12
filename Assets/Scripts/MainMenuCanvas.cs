using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject LevelsMenu;
    public GameObject AboutMenu;
    public GameObject LeaderBoardMenu;
    
    public GameObject MusicGameObject;
    
    public AudioMixer Music;
    public AudioMixer SFX;
    public Slider MusicSlider;
    public Slider SFXSlider;
    public GameObject Transition;
    
    void Start()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        SetMusic(PlayerPrefs.GetFloat("MusicVolume", 1));
        SetSFX(PlayerPrefs.GetFloat("SFXVolume", 1));
    }
    
    public void Play()
    {
        Instantiate(Transition, transform.position, Quaternion.identity);
        StartCoroutine(Play_Wait());
        
        
    }
    IEnumerator Play_Wait()
    {
        
        yield return new WaitForSeconds(0.5f);
        MainMenu.SetActive(false);
        LevelsMenu.SetActive(true);
    }
    public void Leaderboard()
    {
        Instantiate(Transition, transform.position, Quaternion.identity);
        StartCoroutine(Leaderboard_Wait());
        
        
    }
    IEnumerator Leaderboard_Wait()
    {
        yield return new WaitForSeconds(0.5f);
        MainMenu.SetActive(false);
        LeaderBoardMenu.SetActive(true);
    }
    public void Settings()
    {
        Instantiate(Transition, transform.position, Quaternion.identity);
        StartCoroutine(Settings_Wait());

    }
    IEnumerator Settings_Wait()
    {
        
        yield return new WaitForSeconds(0.5f);
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }
    
    public void About()
    {
        Instantiate(Transition, transform.position, Quaternion.identity);
        StartCoroutine(About_Wait());
        
    }
    IEnumerator About_Wait()
    {
        
        yield return new WaitForSeconds(0.5f);
        MainMenu.SetActive(false);
        AboutMenu.SetActive(true);
    }
    
    public void BackFromLevels()
    {
        Instantiate(Transition, transform.position, Quaternion.identity);
        StartCoroutine(BackFromLevels_Wait());
        
    }
    IEnumerator BackFromLevels_Wait()
    {
        
        yield return new WaitForSeconds(0.5f);
        MainMenu.SetActive(true);
        LevelsMenu.SetActive(false);
    }
    public void BackFromLeaderboard()
    {
        Instantiate(Transition, transform.position, Quaternion.identity);
        StartCoroutine(BackFromLeaderboard_Wait());
        
    }
    IEnumerator BackFromLeaderboard_Wait()
    {
        
        yield return new WaitForSeconds(0.5f);
        MainMenu.SetActive(true);
        LeaderBoardMenu.SetActive(false);
    }
    public void BackFromSettings()
    {
        Instantiate(Transition, transform.position, Quaternion.identity);
        StartCoroutine(BackFromSettings_Wait());

    }
    IEnumerator BackFromSettings_Wait()
    {
        
        yield return new WaitForSeconds(0.5f);
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }
    public void BackFromAbout()
    {
        Instantiate(Transition, transform.position, Quaternion.identity);
        StartCoroutine(BackFromAbout_Wait());
    }
    IEnumerator BackFromAbout_Wait()
    {
        
        yield return new WaitForSeconds(0.5f);
        MainMenu.SetActive(true);
        AboutMenu.SetActive(false);
    }
    public void Quit()
    {
        Instantiate(Transition, transform.position, Quaternion.identity);
        StartCoroutine(Quit_Wait());
    }

    IEnumerator Quit_Wait()
    {
        
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
        Debug.Log("quit");
    }

    
    public void SetMusic(float volume)
    {
        Music.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetSFX(float volume)
    {
        SFX.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
        
    }
    public void SetQuality(int qualityInt)
    {
        QualitySettings.SetQualityLevel(qualityInt);
    }
    
    public void OpenWebsite(string url)
    {
        Application.OpenURL(url);
    }
}
