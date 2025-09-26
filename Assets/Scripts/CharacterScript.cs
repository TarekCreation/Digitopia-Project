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

    
    private Dictionary<AudioClip, float[]> bakedAmplitudes = new Dictionary<AudioClip, float[]>();
    private Dictionary<AudioClip, float> bakedDynamicThreshold = new Dictionary<AudioClip, float>();

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

            if (audioSource != null && audioSource.isPlaying && audioSource.clip != null)
            {
                AudioClip clip = audioSource.clip;

                
                if (!bakedAmplitudes.ContainsKey(clip))
                {
                    LoadBakedForClip(clip);
                }

                if (bakedAmplitudes.ContainsKey(clip))
                {
                    float[] arr = bakedAmplitudes[clip];
                    int index = Mathf.FloorToInt(audioSource.time / (checkInterval));
                    index = Mathf.Clamp(index, 0, arr.Length - 1);
                    float amp = arr[index];

                    float thr = bakedDynamicThreshold.ContainsKey(clip) ? bakedDynamicThreshold[clip] : silenceThreshold;
                    isCurrentlySilent = amp < thr;
                }
                else
                {
                    
                    isCurrentlySilent = false;
                }
            }
            else
            {
                isCurrentlySilent = true;
            }
        }
    }

    private void LoadBakedForClip(AudioClip clip)
    {
        
        TextAsset ta = Resources.Load<TextAsset>("BakedAudio/" + clip.name + "_baked");
        if (ta != null)
        {
            try
            {
                var wrapper = JsonUtility.FromJson<BakedWrapper>(ta.text);
                if (wrapper != null && wrapper.values != null && wrapper.values.Length > 0)
                {
                    bakedAmplitudes[clip] = wrapper.values;

                    
                    float[] copy = (float[])wrapper.values.Clone();
                    System.Array.Sort(copy);
                    int take = Mathf.Max(1, copy.Length / 10);
                    float sum = 0f;
                    for (int i = 0; i < take; i++) sum += copy[i];
                    float noiseFloor = sum / take;

                    
                    float dyn = Mathf.Max(silenceThreshold, noiseFloor * 1.5f);
                    bakedDynamicThreshold[clip] = dyn;
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Failed parsing baked audio for " + clip.name + ": " + ex.Message);
            }
        }
    }


    void Update()
    {
        if (isTalking)
        {
            currentAnimator.SetBool("isTalking", !isCurrentlySilent);
        }
        else
        {
            currentAnimator.SetBool("isTalking", false);
        }
    }

    public void ChangeItems()
    {
        if (CharacterFaceIndex == 0)
        {
            ChildFace.SetActive(true);
            RobotFace.SetActive(false);
            currentAnimator = ChildFace.GetComponent<Animator>();
        }
        else if (CharacterFaceIndex == 1)
        {
            ChildFace.SetActive(false);
            RobotFace.SetActive(true);
            currentAnimator = RobotFace.GetComponent<Animator>();
        }

        audioSource = GameObject.Find("VoiceLineReader").GetComponent<AudioSource>();
        samples = new float[sampleSize];
        StopAllCoroutines();
        StartCoroutine(CheckSilenceRoutine());
    }

    [System.Serializable]
    class BakedWrapper { public float[] values; public float step; public int sampleRate; }
}
