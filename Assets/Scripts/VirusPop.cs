using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class VirusPop : MonoBehaviour
{

    private Animator animator;
    public bool isMoving = false;
    public bool isDying = false;
    private CinemachineImpulseSource impulseSource;
    public VirusColor virusColor;
    public bool isAShield = false;
    private float shieldPercentage = 0;
    private float popUpInterval = 25f;

    // Start is called before the first frame update
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        animator = GetComponent<Animator>();
        StartCoroutine(PopUpCoroutine());
        InvokeRepeating("DecreaseTime", 2f, 5f);
    }
    IEnumerator PopUpCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, popUpInterval));
            PopUp();
        }
    }
    void DecreaseTime()
    {
        if (popUpInterval > 2)
        {
            popUpInterval -= 1f;
        }
    }
    public void FinishedPopAnimation()
    {
        isMoving = false;
    }
    public void FinishedDyingAnimation()
    {
        isAShield = false;
        isDying = false;
        virusColor.SetRandomVirusType();
    }
    // Update is called once per frame
    public void PopUp()
    {
        if (shieldPercentage < 11)
        {
            shieldPercentage += 0.5f;
        }
        int rnd = Random.Range(0, 20);
        if (rnd < shieldPercentage)
        {
            isAShield = true;
        }
        else
        {
            isAShield = false;
        }
        FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.virusJump,0.5f);
        if (isAShield)
        {
            if (!isMoving && !isDying)
            {
                isMoving = true;
                animator.Play("ShieldJump");
            }
        }
        else
        {
            if (!isMoving && !isDying)
            {
                isAShield = false;
                isMoving = true;
                animator.Play("VirusJump");
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hammer"))
        {
            if (isAShield)
            {
                isAShield = false;
                isMoving = false;
                animator.Play("ShieldBreak");
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.shieldBreak);
                CamShakeManager.Instance.CameraShake(impulseSource, 1f);
                FindObjectOfType<GUIscript>().EndGame(FindObjectOfType<Hammer>().numberOfKilledViruses,FindObjectOfType<Hammer>().BestCombo);
                FindObjectOfType<Hammer>().enabled = false;
                return;
            }
            else
            {
                isDying = true;
                isMoving = false;
                animator.Play("EnemyDie");
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.virusPunch);
                CamShakeManager.Instance.CameraShake(impulseSource, 1f);
                
                FindObjectOfType<Hammer>().IncreaseNumberOfKilledViruses();
            }
            
        }
    }
}
