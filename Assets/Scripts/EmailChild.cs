using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailChild : MonoBehaviour
{
    public bool isAScam = true;
    public SpriteRenderer image;
    public Sprite[] goodSprites;
    public Sprite[] badSprites;
    public Sprite UnknownSprite;
    public GameObject destroyedVersion;
    
    // Start is called before the first frame update
    public void UpdateContent()
    {
        int rnd = Random.Range(0, 20);
        if (rnd < 1)
        {
            if (isAScam)
            {
                int rndSprite = Random.Range(0, badSprites.Length);
                image.sprite = badSprites[rndSprite];
            }
            else
            {
                int rndSprite = Random.Range(0, goodSprites.Length);
                image.sprite = goodSprites[rndSprite];
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isAScam)
            {
                
                FindObjectOfType<playerMovement>().Die();
            }
            else
            {
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.pickUpEmail);
                transform.parent.parent.GetComponent<Animator>().Play("EmailNormal");
                transform.parent.parent.GetComponent<PhishingEmail>().GotDestroyed = true;
                FindObjectOfType<Score>().UpdateScore(5);
                FindObjectOfType<playerMovement>().IncreaseNumberOfCollectedEmails();
            }
        }
        else if (collision.tag == "Bullet")
        {
            transform.parent.parent.GetComponent<Animator>().Play("EmailNormal");
            transform.parent.parent.GetComponent<PhishingEmail>().GotDestroyed = true;
            Instantiate(destroyedVersion, transform.position, Quaternion.identity);
            FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.destoryEmail,0.2f);
            if (isAScam)
            {
                FindObjectOfType<Score>().UpdateScore(5);
                FindObjectOfType<playerMovement>().IncreaseNumberOfDestoyedBadEmails();
            }
            else
            {
                FindObjectOfType<Score>().UpdateScore(-2);
                FindObjectOfType<playerMovement>().IncreaseNumberOfDestoyedGoodEmails();
            }
        }
    }
}
