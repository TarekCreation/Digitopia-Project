using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }
    public GameObject SFXGO;
    public AudioClip badEmail;
    public AudioClip batAppear;
    public AudioClip BatFlying;
    public AudioClip batShoot;
    public AudioClip blockPop;
    public AudioClip[] Bubbles;
    public AudioClip comboHigh;
    public AudioClip comboLow;
    public AudioClip correct1;
    public AudioClip correctShort;
    public AudioClip correctShortest;
    public AudioClip crab;
    public AudioClip crabAppear;
    public AudioClip destoryEmail;
    public AudioClip destroyMediumPassword;
    public AudioClip destroyStrongPassword;
    public AudioClip destroyWeakPassword;
    public AudioClip dropBlocks_Invalid;
    public AudioClip dropBlocks;
    public AudioClip emailAppearWhoosh;
    public AudioClip endLevel;
    public AudioClip enemyAppear;
    public AudioClip enemyAppear2;
    public AudioClip enemyDie;
    public AudioClip enemyHit;
    public AudioClip EnemyHitTarget;
    public AudioClip explosion;
    public AudioClip eyeDie;
    public AudioClip goodEmail;
    public AudioClip hammerPunch;
    public AudioClip hitTarget;
    public AudioClip jump;
    public AudioClip pickUpBlocks;
    public AudioClip pickUpEmail;
    public AudioClip pickupItem;
    public AudioClip playerDie;
    public AudioClip playerHit;
    public AudioClip Reload;
    public AudioClip Reloading;
    public AudioClip SelectMinigame;
    public AudioClip shieldBreak;
    public AudioClip shieldHitBat;
    public AudioClip shieldSFX;
    public AudioClip shoot;
    public AudioClip shootGun;
    public AudioClip UI;
    public AudioClip virusDie;
    public AudioClip virusJump;
    public AudioClip virusPunch;
    public AudioClip warning;

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

    public void PlaySound(AudioClip clip, float volume = 1, float Pitch = 1)
    {
        if (clip == null) return;

        GameObject soundObject = Instantiate(SFXGO, Vector3.zero, Quaternion.identity);
        AudioSource audioSource = soundObject.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.pitch = Pitch;
            audioSource.Play();
            Destroy(soundObject, clip.length);
        }
    }
    public void PlayRandomBubbleSound()
    {
        GameObject soundObject = Instantiate(SFXGO, Vector3.zero, Quaternion.identity);
        AudioSource audioSource = soundObject.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            int rnd = Random.Range(0, 4);
            audioSource.clip = Bubbles[rnd];
            audioSource.volume = 0.3f;
            audioSource.Play();
            Destroy(soundObject, Bubbles[rnd].length);
        }
    }

}
