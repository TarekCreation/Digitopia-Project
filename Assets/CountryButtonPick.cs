using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryButtonPick : MonoBehaviour
{
    private int country_id = 0;
    public Color Selected;
    public Color notSelected;
    private int PlayerPrefsId = 0;
    public Image BorderImage;
    public Image FlagImage;
    public Sprite[] countryFlags;
    public GameObject SelectMenu;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefsId = PlayerPrefs.GetInt("Country", 0);
        country_id = transform.GetSiblingIndex();
        FlagImage.sprite = countryFlags[country_id];
        if (PlayerPrefsId == country_id)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }
    public void Deactivate()
    {
        BorderImage.color = notSelected;
    }
    public void Activate()
    {
        BorderImage.color = Selected;
    }

    public void ChooseThisCountry()
    {
        if (transform.parent.GetChild(PlayerPrefsId).GetComponent<CountryButtonPick>() != null)
        {
            transform.parent.GetChild(PlayerPrefsId).GetComponent<CountryButtonPick>().Deactivate();
        }
        
        
        Activate();
        StopAllCoroutines();
        StartCoroutine(ChooseThisCountryWait());
    }
    IEnumerator ChooseThisCountryWait()
    {
        yield return new WaitForSeconds(0.2f);
        PlayerPrefs.SetInt("Country", country_id);
        SelectMenu.SetActive(false);
        FindObjectOfType<CountryPicked>().UpdateFlag();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
