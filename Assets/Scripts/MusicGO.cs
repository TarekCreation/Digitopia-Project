using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Music : MonoBehaviour
{
    public static AudioClip Track_MainMenuMusic;
    public static AudioClip Track_StoryMusic;
    public static AudioClip Track_Minigame1;
    public static AudioClip Track_Minigame2;
    public static AudioClip Track_Minigame3;
    public static AudioClip Track_Minigame4;
    public static AudioClip Track_Minigame5;
    public static AudioClip Track_Minigame6;
    public static AudioClip Track_Minigame7;
    public static AudioClip Track_Minigame8;
    public static float volume_MainMenuMusic = 1;
    public static float volume_StoryMusic = 1;
    public static float volume_Minigames = 1;
    

}
public class MusicGO : MonoBehaviour
{
    
    public static MusicGO instance;
    private AudioSource myAudioSource;

    public AudioClip GO_Track_MainMenuMusic;
    public AudioClip GO_Track_StoryMusic;
    public AudioClip GO_Track_Minigame1;
    public AudioClip GO_Track_Minigame2;
    public AudioClip GO_Track_Minigame3;
    public AudioClip GO_Track_Minigame4;
    public AudioClip GO_Track_Minigame5;
    public AudioClip GO_Track_Minigame6;
    public AudioClip GO_Track_Minigame7;
    public AudioClip GO_Track_Minigame8;
    public float GO_volume_MainMenuMusic = 1;
    public float GO_volume_StoryMusic = 1;
    public float GO_volume_Minigames = 1;
    

    public float DecreaseTo_Volume = 0.2f;
    public bool isDecreased = false;
    public AsyncOperation currentOperation = null;
    public static MusicGO Instance { get; private set; }

    private void Awake()
    {
        Music.Track_MainMenuMusic = GO_Track_MainMenuMusic;
        Music.Track_StoryMusic = GO_Track_StoryMusic;
        Music.Track_Minigame1 = GO_Track_Minigame1;
        Music.Track_Minigame2 = GO_Track_Minigame2;
        Music.Track_Minigame3 = GO_Track_Minigame3;
        Music.Track_Minigame4 = GO_Track_Minigame4;
        Music.Track_Minigame5 = GO_Track_Minigame5;
        Music.Track_Minigame6 = GO_Track_Minigame6;
        Music.Track_Minigame7 = GO_Track_Minigame7;
        Music.Track_Minigame8 = GO_Track_Minigame8;
        Music.volume_MainMenuMusic = GO_volume_MainMenuMusic;
        Music.volume_StoryMusic = GO_volume_StoryMusic;
        Music.volume_Minigames = GO_volume_Minigames;
        myAudioSource = GetComponent<AudioSource>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SwitchAudio(AudioClip audioClip)
    {
        StopAllCoroutines();
        float volume = 1;
        if (audioClip == Music.Track_MainMenuMusic)
        {
            volume = Music.volume_MainMenuMusic;
        }else if (audioClip == Music.Track_StoryMusic)
        {
            volume = Music.volume_StoryMusic;
        }else
        {
            volume = Music.volume_Minigames;
        }
        StartCoroutine(SwitchAudioWait(audioClip, volume));
    }
    IEnumerator SwitchAudioWait(AudioClip audioClip, float theVolume)
    {
        float timetoFade = 0.9f;
        float timeElapsed = 0;
        if (isDecreased)
        {
            while (timeElapsed < timetoFade)
            {
                myAudioSource.volume = Mathf.Lerp(DecreaseTo_Volume,0, timeElapsed/timetoFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }else
        {
            while (timeElapsed < timetoFade)
            {
                myAudioSource.volume = Mathf.Lerp(theVolume,0, timeElapsed/timetoFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }

        // while (!operation.isDone)
        // {
        //     loadingBar.value = operation.progress;
        //     yield return null;
        // }
        if (currentOperation != null)
        {
            while (!currentOperation.isDone)
            {
                yield return new WaitForSeconds(0.1f);
                
            }

        }
        else
        {
            yield return new WaitForSeconds(0.2f);
        }
        currentOperation = null;
        myAudioSource.clip = audioClip;
        myAudioSource.Play();
        float timetoFade2 = 0.9f;
        float timeElapsed2 = 0;
        while (timeElapsed2 < timetoFade2)
        {
            myAudioSource.volume = Mathf.Lerp(0,theVolume, timeElapsed2/timetoFade2);
            timeElapsed2 += Time.deltaTime;
            yield return null;
        }
        isDecreased = false;
    }
    public void GoBackToDefault()
    {
        SwitchAudio(Music.Track_MainMenuMusic);
    }
    public void Fade()
    {
        StopAllCoroutines();
        float volume = 1;
        if (myAudioSource.clip == Music.Track_MainMenuMusic)
        {
            volume = Music.volume_MainMenuMusic;
        }else if (myAudioSource.clip == Music.Track_StoryMusic)
        {
            volume = Music.volume_StoryMusic;
        }else
        {
            volume = Music.volume_Minigames;
        }
        StartCoroutine(FadeWait(volume));
    }
    IEnumerator FadeWait(float theVolume)
    {
        float timetoFade = 0.5f;
        float timeElapsed = 0;
        while (timeElapsed < timetoFade)
        {
            myAudioSource.volume = Mathf.Lerp(theVolume,0, timeElapsed/timetoFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        if (currentOperation != null)
        {
            while (!currentOperation.isDone)
            {
                yield return new WaitForSeconds(0.1f);
                
            }

        }
        else
        {
            yield return new WaitForSeconds(0.2f);
        }
        
        
        float timetoFade2 = 0.5f;
        float timeElapsed2 = 0;
        while (timeElapsed2 < timetoFade2)
        {
            myAudioSource.volume = Mathf.Lerp(0,theVolume, timeElapsed2/timetoFade2);
            timeElapsed2 += Time.deltaTime;
            yield return null;
        }
    }
    public void DecreaseVolume(float decreaseToVolume)
    {
        isDecreased = true;
        DecreaseTo_Volume = decreaseToVolume;
        StartCoroutine(DecreaseVolumeWait());
    }
    IEnumerator DecreaseVolumeWait()
    {
        float timetoFade = 0.5f;
        float timeElapsed = 0.5f;
        float theVolume = myAudioSource.volume;
        while (myAudioSource.volume > DecreaseTo_Volume)
        {
            myAudioSource.volume = Mathf.Lerp(theVolume,DecreaseTo_Volume, timeElapsed/timetoFade);
            timeElapsed -= Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        
    }
    public void IncreaseVolume()
    {
        isDecreased = false;
        float volume = 1;
        if (myAudioSource.clip == Music.Track_MainMenuMusic)
        {
            volume = Music.volume_MainMenuMusic;
        }else if (myAudioSource.clip == Music.Track_StoryMusic)
        {
            volume = Music.volume_StoryMusic;
        }else
        {
            volume = Music.volume_Minigames;
        }
        StartCoroutine(IncreaseVolumeWait(volume));
    }
    IEnumerator IncreaseVolumeWait(float theVolume)
    {
        
        
        float timetoFade2 = 0.5f;
        float timeElapsed2 = 0;
        while (myAudioSource.volume < theVolume)
        {
            myAudioSource.volume = Mathf.Lerp(DecreaseTo_Volume,theVolume, timeElapsed2/timetoFade2);
            timeElapsed2 += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
    }
}
