using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhishingEmail : MonoBehaviour
{
    private Animator anim;
    public GameObject[] emails;

    public int currentIndex = 0;
    private float waitingTime = 25f;
    public float MinwaitingTimeHanging = 4f;
    public float MaxWaitingTimeHanging = 8f;
    public bool GotDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Playing());
    }
    IEnumerator Playing()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            currentIndex = Random.Range(0, emails.Length);
            foreach (var item in emails)
            {
                item.SetActive(false);
            }
            emails[currentIndex].SetActive(true);
            emails[currentIndex].GetComponent<EmailChild>().UpdateContent();
            if (emails[currentIndex].GetComponent<EmailChild>().isAScam)
            {
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.badEmail,0.2f);
            }else
            {
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.goodEmail,0.4f);
            }
            yield return new WaitForSeconds(Random.Range(2, waitingTime));
            anim.Play("DropDown");
            yield return new WaitForSeconds(Random.Range(3 + MinwaitingTimeHanging, 3 + MaxWaitingTimeHanging));
            if (!GotDestroyed)
            {
                
                anim.Play("GoBackUp");
                yield return new WaitForSeconds(4);
            }else
            {
                GotDestroyed = false;
                yield return new WaitForSeconds(2);
            }
            
            
            if (waitingTime > 3)
            {
                waitingTime--;
            }
            if (MinwaitingTimeHanging > 0)
            {
                MinwaitingTimeHanging--;
            }
            int rnd = Random.Range(0, 10);
            if (rnd < 4)
            {
                MaxWaitingTimeHanging--;
            }
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
