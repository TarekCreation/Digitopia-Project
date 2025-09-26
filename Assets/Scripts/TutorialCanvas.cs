using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvas : MonoBehaviour
{
    public GameObject PCTutorial;
    public GameObject MobileTutorial;
    public GameObject arabicButton;
    public GameObject englishButton;
    public int MinigameSceneIndex = 0;
    // Start is called before the first frame update
    void Awake()
    {
        bool isArabic = PlayerPrefs.GetString("Language","English") == "Arabic";
        if (isArabic)
        {
            arabicButton.SetActive(true);
            englishButton.SetActive(false);
        }else
        {
            arabicButton.SetActive(false);
            englishButton.SetActive(true);
        }
#if UNITY_WEBGL
        if (Application.isMobilePlatform)
        {
            MobileTutorial.SetActive(true); 
        }else
        {
            PCTutorial.SetActive(true);
        }
#elif UNITY_ANDROID || UNITY_IOS
    MobileTutorial.SetActive(true); 
#else
    PCTutorial.SetActive(true);
#endif
    }

    // Update is called once per frame
    public void StartGame()
    {
        AudioClip audio = null;
        switch (MinigameSceneIndex)
        {
            case 2: // Minigame 1
                audio = Music.Track_Minigame1;
                break;
            case 3: // Minigame 2
                audio = Music.Track_Minigame2;
                break;
            case 4: // Minigame 3
                audio = Music.Track_Minigame3;
                break;
            case 5: // Minigame 4
                audio = Music.Track_Minigame4;
                break;
            case 6: // Minigame 5
                audio = Music.Track_Minigame5;
                break;
            case 7: // Minigame 6
                audio = Music.Track_Minigame6;
                break;
            case 8: // Minigame 7
                audio = Music.Track_Minigame7;
                break;
            default:
                break;
        }
        FindObjectOfType<SceneLoader>().LoadScene(MinigameSceneIndex, audio);
    }
}
