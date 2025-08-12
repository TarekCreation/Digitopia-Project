using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }
    public GameObject SFXGO;
    public AudioClip jumpSound;
    private void Awake()
    {
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

    public void PlaySound(AudioClip clip, Transform position, float volume)
    {
        if (clip == null) return;

        GameObject soundObject = Instantiate(SFXGO, position.position, Quaternion.identity);
        AudioSource audioSource = soundObject.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
            Destroy(soundObject, clip.length);
        }
    }

}
