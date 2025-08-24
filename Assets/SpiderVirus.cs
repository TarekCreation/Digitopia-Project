using System.Collections;
using System.Collections.Generic;
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
    public float[] waitingPeriod;

    void Start()
    {
        StartCoroutine(waitingCheck());
    }
    IEnumerator waitingCheck()
    {
        while (true)
        {
            float randomWait = Random.Range(waitingPeriod[0], waitingPeriod[1]);
            yield return new WaitForSeconds(randomWait);
            if (isWaitingForANewVirus)
            {
                ParentAnim.SetTrigger("Go");
                isWaitingForANewVirus = false;
                StartCoroutine(isWaitingForANewVirusTrue());
            }
            
        }
        
    }
    // Update is called once per frame
    void Update()
    {


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
                SpriteAnim.Play("EnemyHit2");
                StartCoroutine(ReActivateControl());


            }
            else
            {
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
        Score score = FindObjectOfType<Score>();
        
        score.UpdateScore(1);
        
    
        Instantiate(DeathParticles, VirusPos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.15f);
        ParentAnim.Play("NormalMode");
    }
}
