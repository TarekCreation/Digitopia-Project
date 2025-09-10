using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryPicked : MonoBehaviour
{
    private int PlayerPrefsId = 0;
    public Image FlagImage;
    public Sprite[] countryFlags;
    public GameObject SelectMenu;

    // Start is called before the first frame update
    void Start()
    {
        UpdateFlag();
    }
    public void UpdateFlag()
    {
        PlayerPrefsId = PlayerPrefs.GetInt("Country", 0);

        FlagImage.sprite = countryFlags[PlayerPrefsId];
    }
    public void ClickOnFlag()
    {
        SelectMenu.SetActive(true);
    }
}
