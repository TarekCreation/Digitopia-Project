using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullets : MonoBehaviour
{
    public int numberOfBullets = 6;
    public Image[] bullets;
    public Color OnColor;
    public Color OffColor;
    private int currentNumberOfBullets = 6;
    public GameObject ReloadGO;
    public GameObject ReloadMobile;
    public GameObject BulletPrefab;
    public GameObject ReloadButton;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_WEBGL
    if (Application.isMobilePlatform)
        ReloadButton.SetActive(true);
    else
        ReloadButton.SetActive(false);
#elif UNITY_ANDROID || UNITY_IOS
    ReloadButton.SetActive(true);
#else
    ReloadButton.SetActive(false);
#endif
      
        currentNumberOfBullets = numberOfBullets;
        ReloadGO.SetActive(false);
        ReloadMobile.SetActive(false);
        foreach (var item in bullets)
        {
            item.color = OnColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Reload();
        }
    }
    public void Reload()
    {
        FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.Reloading,0.9f);
        currentNumberOfBullets = numberOfBullets;
        ReloadGO.SetActive(false);
        ReloadMobile.SetActive(false);
        foreach (var item in bullets)
        {
            item.color = OnColor;
        }
    }
    public bool Shoot(Vector3 pos)
    {
        bool didShoot = false;
        if (currentNumberOfBullets > 0)
        {
            currentNumberOfBullets--;
            didShoot = true;
            Instantiate(BulletPrefab, pos, Quaternion.identity);
            FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.shootGun);
            foreach (var item in bullets)
            {
                item.color = OffColor;
            }
            for (int i = 0; i < currentNumberOfBullets; i++)
            {
                bullets[i].color = OnColor;
            }
            if (currentNumberOfBullets == 0)
            {
#if UNITY_WEBGL
                if (Application.isMobilePlatform)
                    ReloadMobile.SetActive(true);
                else
                    ReloadGO.SetActive(true);
#elif UNITY_ANDROID || UNITY_IOS
                ReloadMobile.SetActive(true);
#else
                ReloadGO.SetActive(true);
#endif
            }
        }else
        {
            FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.Reload);
        }
        return didShoot;
    }
}
