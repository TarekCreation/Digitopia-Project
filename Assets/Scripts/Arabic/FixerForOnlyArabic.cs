using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FixerForOnlyArabic : MonoBehaviour
{
    private string language;
    
    public TMP_FontAsset ArabicTextAsset;
    public TMP_FontAsset EnglishTextAsset;
    public string fixedText;
    private bool ShowTashkeel = true;
    private bool UseHinduNumbers = false;
    private bool isArabic = false;
    public bool doesAlignToLanguage = false;

    TextMeshProUGUI tmpTextComponent;

    private string OldArabicText;
    private string OldEnglishText; // For Refresh on TextChange
    private int OldFontSize; // For Refresh on Font Size Change
    private RectTransform rectTransform;  // For Refresh on resize
    private Vector2 OldDeltaSize; // For Refresh on resize
    private bool OldEnabled = false; // For Refresh on enabled change // when text ui is not active then arabic text will not trigered when the control get active
    private List<RectTransform> OldRectTransformParents = new List<RectTransform>(); // For Refresh on parent resizing
    private Vector2 OldScreenRect = new Vector2(Screen.width, Screen.height); // For Refresh on screen resizing

    bool isInitilized;
    public void Awake()
    {
        GetRectTransformParents(OldRectTransformParents);
        isInitilized = false;
        tmpTextComponent = GetComponent<TextMeshProUGUI>();
    }

    public void Start()
    {
        isArabic = PlayerPrefs.GetString("Language","English") == "Arabic";
        rectTransform = GetComponent<RectTransform>();
        
        //fixedText = tmpTextComponent.text;
        
        
        
        isInitilized = true;
    }

    private void GetRectTransformParents(List<RectTransform> rectTransforms)
    {
        rectTransforms.Clear();
        for (Transform parent = transform.parent; parent != null; parent = parent.parent)
        {
            GameObject goP = parent.gameObject;
            RectTransform rect = goP.GetComponent<RectTransform>();
            if (rect) rectTransforms.Add(rect);
        }
    }

    private bool CheckRectTransformParentsIfChanged()
    {
        bool hasChanged = false;
        for (int i = 0; i < OldRectTransformParents.Count; i++)
        {
            hasChanged |= OldRectTransformParents[i].hasChanged;
            OldRectTransformParents[i].hasChanged = false;
        }
        return hasChanged;
    }

    public void Update()
    {
        isArabic = PlayerPrefs.GetString("Language","English") == "Arabic";
        if (!isInitilized)
            return;

        if (isArabic)
        {
            
            
            // if No Need to Refresh
            if (OldArabicText == fixedText &&
                OldFontSize == tmpTextComponent.fontSize &&
                OldDeltaSize == rectTransform.sizeDelta &&
                OldEnabled == tmpTextComponent.enabled &&
                (OldScreenRect.x == Screen.width && OldScreenRect.y == Screen.height &&
                !CheckRectTransformParentsIfChanged()))
                return;
            tmpTextComponent.font = ArabicTextAsset;
            
            FixTextForUI();
            
            OldArabicText = fixedText;
            OldFontSize = (int)tmpTextComponent.fontSize;
            OldDeltaSize = rectTransform.sizeDelta;
            OldEnabled = tmpTextComponent.enabled;
            OldScreenRect.x = Screen.width;
            OldScreenRect.y = Screen.height;
            
            isArabic = PlayerPrefs.GetString("Language","English") == "Arabic";
            if (doesAlignToLanguage)
            {
                tmpTextComponent.horizontalAlignment = HorizontalAlignmentOptions.Right;
            }
        }else
        {
            
            // if No Need to Refresh
            if (OldEnglishText == fixedText &&
                OldFontSize == tmpTextComponent.fontSize &&
                OldDeltaSize == rectTransform.sizeDelta &&
                OldEnabled == tmpTextComponent.enabled &&
                (OldScreenRect.x == Screen.width && OldScreenRect.y == Screen.height &&
                !CheckRectTransformParentsIfChanged()))
                return;
            tmpTextComponent.font = EnglishTextAsset;
            
            FixTextForUI();
            OldEnglishText = fixedText;
            OldFontSize = (int)tmpTextComponent.fontSize;
            OldDeltaSize = rectTransform.sizeDelta;
            OldEnabled = tmpTextComponent.enabled;
            OldScreenRect.x = Screen.width;
            OldScreenRect.y = Screen.height;
            
            isArabic = PlayerPrefs.GetString("Language","English") == "Arabic";
            if (doesAlignToLanguage)
            {
                tmpTextComponent.horizontalAlignment = HorizontalAlignmentOptions.Left;
            }
        }
        
        
    }
    
    public void FixTextForUI()
    {
        if (!string.IsNullOrEmpty(fixedText))
        {
            string rtlText = ArabicSupport.Fix(fixedText, ShowTashkeel, UseHinduNumbers);
            rtlText = rtlText.Replace("\r", ""); // the Arabix fixer Return \r\n for everyy \n .. need to be removed

            string finalText = "";
            string[] rtlParagraph = rtlText.Split('\n');

            tmpTextComponent.text = "";
            for (int lineIndex = 0; lineIndex < rtlParagraph.Length; lineIndex++)
            {
                string[] words = rtlParagraph[lineIndex].Split(' ');
                System.Array.Reverse(words);
                tmpTextComponent.text = string.Join(" ", words);
                Canvas.ForceUpdateCanvases();
                for (int i = 0; i < tmpTextComponent.textInfo.lineCount; i++)
                {
                    int startIndex = tmpTextComponent.textInfo.lineInfo[i].firstCharacterIndex;
                    int endIndex = (i == tmpTextComponent.textInfo.lineCount - 1) ? tmpTextComponent.text.Length
                        : tmpTextComponent.textInfo.lineInfo[i + 1].firstCharacterIndex;
                    int length = endIndex - startIndex;
                    string[] lineWords = tmpTextComponent.text.Substring(startIndex, length).Split(' ');
                    System.Array.Reverse(lineWords);
                    finalText = finalText + string.Join(" ", lineWords).Trim() + "\n";
                }
            }
            tmpTextComponent.text = finalText.TrimEnd('\n');
        }
}
}



















