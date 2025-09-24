using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpiderVirus : MonoBehaviour
{
    public float health = 3f;
    public bool CanControl = true;
    public GameObject DeathParticles;
    public Animator SpriteAnim;
    public Transform VirusPos;
    public Animator ParentAnim;
    public bool isWaitingForANewVirus = true;
    public bool itemIsShielded = false;
    public float[] waitingPeriod;
    public GameObject[] Visuals;
    public GameObject VisualsParent;
    public CircleCollider2D circleCollider;
    public float startingWaitingTime = 1;
    public GameObject maskObject;

    void Start()
    {
        StartCoroutine(waitingCheck());
    }
    IEnumerator waitingCheck()
    {
        yield return new WaitForSeconds(startingWaitingTime);
        while (true)
        {
            float randomWait = Random.Range(waitingPeriod[0], waitingPeriod[1]);
            yield return new WaitForSeconds(randomWait);
            if (isWaitingForANewVirus && !itemIsShielded)
            {
                int rnd = Random.Range(0, 20);
                if (rnd < 2)
                {
                    ParentAnim.SetTrigger("Shield");
                    FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.shieldSFX,0.2f);
                    itemIsShielded = true;
                    StartCoroutine(DeactivateShield());
                }
                else
                {
                    int random = Random.Range(0, Visuals.Length);
                    foreach (var item in Visuals)
                    {
                        item.SetActive(false);
                    }
                    Visuals[random].SetActive(true);
                    maskObject.GetComponent<Animator>().Play("VirusHitMask",-1,0);
                    int TypeRnd = Random.Range(0, 10);
                    if (TypeRnd == 0)
                    {
                        circleCollider.radius = 1.32f;
                        health = 4;
                        VisualsParent.transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
                    }
                    else
                    {
                        circleCollider.radius = 0.9556336f;
                        health = 2;
                        VisualsParent.transform.localScale = new Vector3(1f, 1f, 1f);
                    }

                    ParentAnim.SetTrigger("Go");
                    FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.warning,0.35f);
                    isWaitingForANewVirus = false;
                    StartCoroutine(isWaitingForANewVirusTrue());
                }

            }

        }
        
    }
    // Update is called once per frame
    void Update()
    {


    }
    IEnumerator DeactivateShield()
    {
        yield return new WaitForSeconds(16f);
        itemIsShielded = false;
    }
    IEnumerator isWaitingForANewVirusTrue()
    {
        yield return new WaitForSeconds(10f);
        isWaitingForANewVirus = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            if (health > 1)
            {

                health -= 1f;
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.enemyHit);
                SpriteAnim.Play("EnemyHit2");
                StartCoroutine(ReActivateControl());


            }
            else
            {
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.enemyDie);
                SpriteAnim.Play("EnemyHit2");
                StartCoroutine(ReActivateControl());
                StartCoroutine(Death());
            }

        }
    }
    IEnumerator ReActivateControl()
    {
        yield return new WaitForSeconds(0.3f);
        CanControl = true;

    }
    IEnumerator Death()
    {
        GetComponent<Collider2D>().enabled = false;
        Score score = FindObjectOfType<Score>();
        
        score.UpdateScore(2);
        FindObjectOfType<playerMovement>().IncreaseNumberOfKilledViruses();

        Instantiate(DeathParticles, VirusPos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.15f);
        ParentAnim.Play("NormalMode");
        GetComponent<Collider2D>().enabled = true;
    }
}
