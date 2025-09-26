using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderVirusParent : MonoBehaviour
{
    

    // Update is called once per frame
    public void DecreaseScore()
    {
        Score score = FindObjectOfType<Score>();
        if (score != null)
        {
            score.UpdateScore(-1);
        }
        FindObjectOfType<playerMovement>().IncreaseNumberOfLostItems();
        FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.Reload,0.7f);
    }
    public void PlayPickUpSound()
    {
        FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.pickupItem,0.5f);
    }
}
