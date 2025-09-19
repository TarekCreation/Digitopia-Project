using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{

    public GameObject ChildFace;
    public GameObject RobotFace;
    private Animator currentAnimator;
    public bool isTalking;
    public int CharacterFaceIndex = 0;
    private AudioSource audioSource;
    public float checkInterval = 0.1f;
    public float silenceThreshold = 0.01f;
    public int sampleSize = 256;
    private float[] samples;
    private bool isCurrentlySilent = false;

    private void Start()
    {
        audioSource = GameObject.Find("VoiceLineReader").GetComponent<AudioSource>();
        samples = new float[sampleSize];
        StopAllCoroutines();
        StartCoroutine(CheckSilenceRoutine());
    }

    private IEnumerator CheckSilenceRoutine()
    {
        while (true)
        {

            yield return new WaitForSeconds(checkInterval);
            
            if (audioSource.isPlaying)
            {
                isCurrentlySilent = IsSilent();
                
            }
        }
    }

    private bool IsSilent()
    {
        audioSource.GetOutputData(samples, 0);

        float sum = 0f;
        for (int i = 0; i < sampleSize; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }

        float average = sum / sampleSize;
        return average < silenceThreshold;
    }
    void Update()
    {
        if (isTalking)
        {
            if (!isCurrentlySilent)
            {
                currentAnimator.SetBool("isTalking", true);
            }else
            {
                currentAnimator.SetBool("isTalking", false);
            }
            
        }else
        {
            currentAnimator.SetBool("isTalking", false);
        }
    }
    // Update is called once per frame
    public void ChangeItems()
    {
        if (CharacterFaceIndex == 0)
        {
            ChildFace.SetActive(true);
            RobotFace.SetActive(false);
            currentAnimator = ChildFace.GetComponent<Animator>();
            audioSource = GameObject.Find("VoiceLineReader").GetComponent<AudioSource>();
            samples = new float[sampleSize];
            StopAllCoroutines();
            StartCoroutine(CheckSilenceRoutine());
        }
        else if (CharacterFaceIndex == 1)
        {
            ChildFace.SetActive(false);
            RobotFace.SetActive(true);
            currentAnimator = RobotFace.GetComponent<Animator>();
            audioSource = GameObject.Find("VoiceLineReader").GetComponent<AudioSource>();
            samples = new float[sampleSize];
            StopAllCoroutines();
            StartCoroutine(CheckSilenceRoutine());
        }
        
        
    }
}
