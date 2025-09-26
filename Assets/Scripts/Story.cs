using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
public class Story : MonoBehaviour
{
    public enum Character
    {
        Narrator,
        Hassan,
        Robot,
        

    }

    
    
    [System.Serializable]
    public struct Dialogue
    {
        public Character character;
        [TextArea(10,116)]
        public string TextInArabic;
        [TextArea(10,116)]
        public string TextInEnglish;
        public AudioClip ArabicVoiceLine;
        public AudioClip EnglishVoiceLine;
        public Sprite OptionalBackground;
    }
    public int TutorialSceneIndex = 0;
    public float waitingBetweenLetters = 0.1f;
    public Dialogue[] dialogues;
    public GameObject EnglishGO;
    public GameObject ArabicGO;
    public GameObject English_Narrator_GO;
    public GameObject Arabic_Narrator_GO;
    public Color[] colors;
    public UnityEngine.UI.Image[] coloredSprites;
    public GameObject characterEnglish;
    public GameObject characterArabic;
    public ArabicFixerTMPRO NameValue_Arabic;
    public TextMeshProUGUI MessageValue_Arabic;
    public TextMeshProUGUI NameValue_English;
    public TextMeshProUGUI MessageValue_English;
    public TextMeshProUGUI Narrator_MessageValue_Arabic;
    public TextMeshProUGUI Narrator_MessageValue_English;
    public UnityEngine.UI.Button prev_button_A1;
    public ArabicFixOnlyOnce next_button_A1;
    public UnityEngine.UI.Button prev_button_E1;
    public ArabicFixOnlyOnce next_button_E1;
    public UnityEngine.UI.Button prev_button_A2;
    public ArabicFixOnlyOnce next_button_A2;
    public UnityEngine.UI.Button prev_button_E2;
    public ArabicFixOnlyOnce next_button_E2;

    public int currentIndex = 0;
    private bool isArabic;
    private int currentCharacterIndex;
    private string currentCharacterName_English;
    private string currentCharacterName_Arabic;
    private Sprite currentSprite;
    public AudioSource VoiceLineReader;
    
    
    void OnEnable()
    {
        
        isArabic = PlayerPrefs.GetString("Language","English") == "Arabic";
        if (isArabic)
        {
            ArabicGO.SetActive(true);
            EnglishGO.SetActive(false);
        }else
        {
            ArabicGO.SetActive(false);
            EnglishGO.SetActive(true);
        }
        StartDialogue();
    }

    public void StartDialogue()
    {
        if (isArabic && currentIndex < dialogues.Length - 1)
        {

            if (currentIndex == dialogues.Length - 1) {
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    ArabicGO.SetActive(false);
                    Arabic_Narrator_GO.SetActive(true);
                    next_button_A1.ValueChanged("أنهي");
                    
                    prev_button_A1.interactable = false;
                    UpdateBackground();
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, Narrator_MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));
                } else
                {
                    ArabicGO.SetActive(true);
                    Arabic_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);
                    next_button_A2.ValueChanged("أنهي");
                    
                    prev_button_A2.interactable = false;
                    UpdateItems();

                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));
                    NameValue_Arabic.ArabicText = currentCharacterName_Arabic;
                    NameValue_Arabic.EnglishText = currentCharacterName_English;
                    NameValue_Arabic.ValueChanged(currentCharacterName_Arabic);
                }
            } else if (currentIndex < dialogues.Length && currentIndex >= 0)
            {
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    ArabicGO.SetActive(false);
                    Arabic_Narrator_GO.SetActive(true);
                    next_button_A1.ValueChanged("التالي");
                     
                    UpdateBackground();
                    prev_button_A1.interactable = false;
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, Narrator_MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));
                } else
                {
                    ArabicGO.SetActive(true);
                    Arabic_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);

                    next_button_A2.ValueChanged("التالي");
                    
                    prev_button_A2.interactable = false;
                    UpdateItems();
                    NameValue_Arabic.ArabicText = currentCharacterName_Arabic;
                    NameValue_Arabic.EnglishText = currentCharacterName_English;
                    NameValue_Arabic.ValueChanged(currentCharacterName_Arabic);
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));

                }
            }
        } else if (!isArabic && currentIndex < dialogues.Length - 1)
        {

            if (currentIndex == dialogues.Length - 1) {
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    EnglishGO.SetActive(false);
                    English_Narrator_GO.SetActive(true);
                    UpdateBackground();
                    next_button_E1.ValueChanged("Finish");
                    
                    prev_button_E1.interactable = false;
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, Narrator_MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));
                } else
                {
                    EnglishGO.SetActive(true);
                    English_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);
                    next_button_E2.ValueChanged("Finish");
                    
                    prev_button_E2.interactable = false;
                    UpdateItems();
                    NameValue_English.text = currentCharacterName_English;
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));

                }
            } else if (currentIndex < dialogues.Length && currentIndex >= 0)
            {
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    EnglishGO.SetActive(false);
                    English_Narrator_GO.SetActive(true);

                    next_button_E1.ValueChanged("Next");
                    
                    prev_button_E1.interactable = false;
                    UpdateBackground();
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, Narrator_MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));
                } else
                {
                    EnglishGO.SetActive(true);
                    English_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);
                    next_button_E2.ValueChanged("Next");
                    

                    prev_button_E2.interactable = false;
                    UpdateItems();
                    NameValue_English.text = currentCharacterName_English;
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));

                }
            }
        }
    }
    public void Next()
    {
        
        if (isArabic && currentIndex < dialogues.Length - 1)
        {
            currentIndex ++;
            if (currentIndex == dialogues.Length - 1){
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    
                    ArabicGO.SetActive(false);
                    Arabic_Narrator_GO.SetActive(true);
                    next_button_A1.ValueChanged("أنهي");
                    
                    prev_button_A1.interactable = true;
                    UpdateBackground();
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, Narrator_MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));
                }else
                {
                    prev_button_A1.interactable = true;
                    ArabicGO.SetActive(true);
                    Arabic_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);
                    next_button_A2.ValueChanged("أنهي");
                    
                    prev_button_A2.interactable = true;
                    UpdateItems();
                    NameValue_Arabic.ArabicText = currentCharacterName_Arabic;
                    NameValue_Arabic.EnglishText = currentCharacterName_English;
                    NameValue_Arabic.ValueChanged(currentCharacterName_Arabic);
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));
                    
                }
            }else if (currentIndex < dialogues.Length && currentIndex > 0)
            {
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    ArabicGO.SetActive(false);
                    Arabic_Narrator_GO.SetActive(true);
                    next_button_A1.ValueChanged("التالي");
                    
                    prev_button_A1.interactable = true;
                    UpdateBackground();
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, Narrator_MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));
                }else
                {
                    ArabicGO.SetActive(true);
                    Arabic_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);
                    next_button_A2.ValueChanged("التالي");
                    
                    prev_button_A2.interactable = true;
                    UpdateItems();
                    NameValue_Arabic.ArabicText = currentCharacterName_Arabic;
                    NameValue_Arabic.EnglishText = currentCharacterName_English;
                    NameValue_Arabic.ValueChanged(currentCharacterName_Arabic);
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));
                    
                }
            }else if (currentIndex == 0)
            {
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    ArabicGO.SetActive(false);
                    Arabic_Narrator_GO.SetActive(true);
                    next_button_A1.ValueChanged("التالي");
                    
                    prev_button_A1.interactable = false;
                    UpdateBackground();
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, Narrator_MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));
                }else
                {
                    ArabicGO.SetActive(true);
                    Arabic_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);
                    next_button_A2.ValueChanged("التالي");
                    
                    prev_button_A2.interactable = false;
                    UpdateItems();
                    NameValue_Arabic.ArabicText = currentCharacterName_Arabic;
                    NameValue_Arabic.EnglishText = currentCharacterName_English;
                    NameValue_Arabic.ValueChanged(currentCharacterName_Arabic);
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));
                    
                }
            }
        }else if (isArabic && currentIndex == dialogues.Length - 1)
        {
            prev_button_A1.interactable = true;
            prev_button_A2.interactable = true;
            
            CloseStory();
        }else if (!isArabic && currentIndex < dialogues.Length - 1)
        {
            currentIndex ++;
            if (currentIndex == dialogues.Length - 1){
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    EnglishGO.SetActive(false);
                    English_Narrator_GO.SetActive(true);

                    next_button_E1.ValueChanged("Finish");
                    
                    prev_button_E1.interactable = true;
                    UpdateBackground();
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, Narrator_MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));
                }else
                {
                    EnglishGO.SetActive(true);
                    English_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);
                    next_button_E2.ValueChanged("Finish");
                    
                    prev_button_E2.interactable = true;
                    UpdateItems();
                    NameValue_English.text = currentCharacterName_English;
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));
                    
                }
            }else if (currentIndex < dialogues.Length && currentIndex > 0)
            {
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    EnglishGO.SetActive(false);
                    English_Narrator_GO.SetActive(true);
                    next_button_E1.ValueChanged("Next");
                    
                    prev_button_E1.interactable = true;
                    UpdateBackground();
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, Narrator_MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));
                }else
                {
                    EnglishGO.SetActive(true);
                    English_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);
                    next_button_E2.ValueChanged("Next");
                    
                    prev_button_E2.interactable = true;
                    UpdateItems();
                    NameValue_English.text = currentCharacterName_English;
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));
                    
                }
            }else if (currentIndex == 0)
            {
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    EnglishGO.SetActive(false);
                    English_Narrator_GO.SetActive(true);
                    next_button_E1.ValueChanged("Next");
                    
                    prev_button_E1.interactable = false;
                    UpdateBackground();
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, Narrator_MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));
                }else
                {
                    EnglishGO.SetActive(true);
                    English_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);
                    next_button_E2.ValueChanged("Next");
                    
                    prev_button_E2.interactable = false;
                    UpdateItems();
                    NameValue_English.text = currentCharacterName_English;
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));
                    
                }
            }
        }else if (!isArabic && currentIndex == dialogues.Length - 1)
        {
            prev_button_A1.interactable = true;
            prev_button_A2.interactable = true;
            
            CloseStory();
        }
    }

    public void Previous()
    {
        
        if (isArabic && currentIndex > 0)
        {
            currentIndex --;
            if (currentIndex == 0)
            {
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    ArabicGO.SetActive(false);
                    Arabic_Narrator_GO.SetActive(true);
                    
                    next_button_A1.ValueChanged("التالي");
                    
                    prev_button_A1.interactable = false;
                    UpdateBackground();
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, Narrator_MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));
                }else
                {
                    ArabicGO.SetActive(true);
                    Arabic_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);
                    next_button_A2.ValueChanged("التالي");
                    
                    prev_button_A2.interactable = false;
                    UpdateItems();
                    NameValue_Arabic.ArabicText = currentCharacterName_Arabic;
                    NameValue_Arabic.EnglishText = currentCharacterName_English;
                    NameValue_Arabic.ValueChanged(currentCharacterName_Arabic);
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));
                    
                }
                
            }else if (currentIndex <= dialogues.Length - 1)
            {
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    ArabicGO.SetActive(false);
                    Arabic_Narrator_GO.SetActive(true);
                    next_button_A1.ValueChanged("التالي");
                    
                    prev_button_A1.interactable = true;
                    UpdateBackground();
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, Narrator_MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));
                }else
                {
                    ArabicGO.SetActive(true);
                    Arabic_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);
                    next_button_A2.ValueChanged("التالي");
                    
                    prev_button_A2.interactable = true;
                    UpdateItems();
                    NameValue_Arabic.ArabicText = currentCharacterName_Arabic;
                    NameValue_Arabic.EnglishText = currentCharacterName_English;
                    NameValue_Arabic.ValueChanged(currentCharacterName_Arabic);
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInArabic, MessageValue_Arabic, dialogues[currentIndex].ArabicVoiceLine));
                    
                }
            }
        }else if (!isArabic && currentIndex < dialogues.Length)
        {
            currentIndex --;
            if (currentIndex == 0)
            {
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    EnglishGO.SetActive(false);
                    English_Narrator_GO.SetActive(true);
                    next_button_E1.ValueChanged("Next");
                    
                    prev_button_E1.interactable = false;
                    UpdateBackground();
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, Narrator_MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));
                }else
                {
                    EnglishGO.SetActive(true);
                    English_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);
                    next_button_E2.ValueChanged("Next");
                    
                    prev_button_E2.interactable = false;
                    UpdateItems();
                    NameValue_English.text = currentCharacterName_English;
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));
                    
                }
                
            }else if (currentIndex <= dialogues.Length - 1)
            {
                if (dialogues[currentIndex].character == Character.Narrator)
                {
                    EnglishGO.SetActive(false);
                    English_Narrator_GO.SetActive(true);
                    next_button_E1.ValueChanged("Next");
                    
                    prev_button_E1.interactable = true;
                    UpdateBackground();
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, Narrator_MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));
                }else
                {
                    EnglishGO.SetActive(true);
                    English_Narrator_GO.SetActive(false);
                    CheckCharacterIndex(dialogues[currentIndex].character);
                    next_button_E2.ValueChanged("Next");
                    
                    prev_button_E2.interactable = true;
                    UpdateItems();
                    NameValue_English.text = currentCharacterName_English;
                    StartCoroutine(AddCharactersText(dialogues[currentIndex].TextInEnglish, MessageValue_English, dialogues[currentIndex].EnglishVoiceLine));
                    
                }
            }
        }
    }
    public void CheckCharacterIndex(Character character)
    {
        if (character == Character.Hassan)
        {
            currentCharacterIndex = 0;
            currentCharacterName_English = "Hassan";
            currentCharacterName_Arabic = "حسن";
        }else if (character == Character.Robot)
        {
            currentCharacterIndex = 1;
            currentCharacterName_English = "Zero";
            currentCharacterName_Arabic = "زيرو";
        }
    }
    IEnumerator AddCharactersText(string text, TextMeshProUGUI textMeshProUGUI, AudioClip voiceLine)
    {
        StopAllCoroutines();
        StartCoroutine(ActualCode(text,textMeshProUGUI,voiceLine));
        yield return null;
        
    }
    IEnumerator ActualCode(string text, TextMeshProUGUI textMeshProUGUI, AudioClip voiceLine)
    {
        if (isArabic)
        {
            string currentText = "";
            textMeshProUGUI.text = currentText;
            if (characterArabic.GetComponent<CharacterScript>().isActiveAndEnabled)
            {
                characterArabic.GetComponent<CharacterScript>().isTalking = true;
                characterArabic.GetComponent<CharacterScript>().ChangeItems();
            }

            VoiceLineReader.Stop();
            VoiceLineReader.clip = voiceLine;
            VoiceLineReader.Play();
            float voiceLinePeriod = voiceLine.length - 0.2f;
            yield return new WaitForSeconds(0.1f);
            float waitingTimeBetweenLetters = voiceLinePeriod / text.Length;

            int i = 0;
            while (currentText.Length < text.Length)
            {
                currentText += text[i];
                yield return new WaitForSeconds(waitingTimeBetweenLetters);

                textMeshProUGUI.GetComponent<ArabicFixerTMPRO>().ValueChanged(currentText);
                i++;
            }
            if (characterArabic.GetComponent<CharacterScript>().isActiveAndEnabled)
            {
                characterArabic.GetComponent<CharacterScript>().isTalking = false;
                characterArabic.GetComponent<CharacterScript>().ChangeItems();
            }

        }
        else
        {
            string currentText = "";
            textMeshProUGUI.text = currentText;
            if (characterEnglish.GetComponent<CharacterScript>().isActiveAndEnabled)
            {
                characterEnglish.GetComponent<CharacterScript>().isTalking = true;
                characterEnglish.GetComponent<CharacterScript>().ChangeItems();
            }
            VoiceLineReader.Stop();
            VoiceLineReader.clip = voiceLine;
            VoiceLineReader.Play();
            float voiceLinePeriod = voiceLine.length - 0.2f;
            yield return new WaitForSeconds(0.1f);
            float waitingTimeBetweenLetters = voiceLinePeriod / text.Length;
            int i = 0;
            while (currentText.Length < text.Length)
            {
                currentText += text[i];
                yield return new WaitForSeconds(waitingTimeBetweenLetters);

                textMeshProUGUI.text = currentText;
                i++;
            }
            if (characterEnglish.GetComponent<CharacterScript>().isActiveAndEnabled)
            {
                characterEnglish.GetComponent<CharacterScript>().isTalking = false;
                characterEnglish.GetComponent<CharacterScript>().ChangeItems();
            }
            }
        
    }
    public void CloseStory()
    {
        FindObjectOfType<SceneLoader>().LoadScene(TutorialSceneIndex, null);
    }
    public void UpdateItems()
    {
        foreach (var item in coloredSprites)
        {
            if (item.isActiveAndEnabled)
            {
                item.color = colors[currentCharacterIndex];
            }
            
        }
        
        if (currentSprite != dialogues[currentIndex].OptionalBackground)
        {
            FindObjectOfType<Background>().ChangeBackground(dialogues[currentIndex].OptionalBackground);
            currentSprite = dialogues[currentIndex].OptionalBackground;
        }
        if (isArabic)
        {
            characterArabic.GetComponent<CharacterScript>().CharacterFaceIndex = currentCharacterIndex;
            characterArabic.GetComponent<CharacterScript>().ChangeItems();
        }else
        {
            characterEnglish.GetComponent<CharacterScript>().CharacterFaceIndex = currentCharacterIndex;
            characterEnglish.GetComponent<CharacterScript>().ChangeItems();
        }
        
                
            
        
        
    }
    public void UpdateBackground()
    {
        
        if (currentSprite != dialogues[currentIndex].OptionalBackground)
        {
            FindObjectOfType<Background>().ChangeBackground(dialogues[currentIndex].OptionalBackground);
            currentSprite = dialogues[currentIndex].OptionalBackground;
        }
        
    }

}
