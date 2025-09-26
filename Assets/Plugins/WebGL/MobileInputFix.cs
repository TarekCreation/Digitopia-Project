using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
public class MobileInputFix : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void ShowMobileInput(string currentText);

    [DllImport("__Internal")]
    private static extern string GetMobileInput();

    [DllImport("__Internal")]
    private static extern void HideMobileInput();
#endif

    public TMP_InputField inputField;

    public void OnFocus()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ShowMobileInput(inputField.text);
#endif
    }

    public void OnEndEdit(string text)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        inputField.text = GetMobileInput();
        HideMobileInput();
#endif
    }

    public void OnDeselect()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        HideMobileInput();
#endif
    }
}
