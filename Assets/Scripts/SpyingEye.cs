using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpyingEye : MonoBehaviour
{
    public float health;
    public Slider healthSlider;
    public GameObject DeathParticles;
    public float speed = 3.0f;
    public Transform eyePupil;

    Coroutine myCoroutine = null;
    private bool canControl = true;
    private Transform target;
    public int value = 3;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        target = GameObject.Find("CamLens").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            Vector3 eyePupilPosition = direction * 0.3f;
            eyePupil.transform.localPosition = eyePupilPosition;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            GetHit();
        }
        else if (collision.tag == "CamLens")
        {
            FindObjectOfType<GUIscript>().EndGame(FindObjectOfType<Gun>().numberOfKilledSpies);
            FindObjectOfType<Gun>().gameObject.SetActive(false);
            FindObjectOfType<Bullets>().transform.parent.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    public void GetHit()
    {
        if (health > 1)
        {
            if (myCoroutine == null)
            {
                myCoroutine = StartCoroutine(PlaySliderAnimation(health - 1));
            }
            else
            {
                StopCoroutine(myCoroutine);
                myCoroutine = StartCoroutine(PlaySliderAnimation(health - 1));
            }
            GetComponent<Animator>().Play("HitEye", -1, 0);

        }
        else
        {
            if (myCoroutine == null)
            {
                StartCoroutine(PlayDeathAnimation(health - 1));
            }
            else
            {
                StopCoroutine(myCoroutine);
                StartCoroutine(PlayDeathAnimation(health - 1));
            }
            GetComponent<Animator>().Play("HitEye", -1, 0);

            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        FindObjectOfType<Score>().UpdateScore(value);
        yield return new WaitForSeconds(0.15f);
        FindObjectOfType<Gun>().IncreaseNumberOfKilledSpies();
        Instantiate(DeathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
    IEnumerator PlaySliderAnimation(float Reqhealth)
    {


        float currentHealth = health;
        health = Reqhealth;
        while (currentHealth > Reqhealth)
        {
            currentHealth -= 0.07f;
            healthSlider.value = currentHealth;
            yield return new WaitForSeconds(0.1f);
        }

        healthSlider.value = Reqhealth;
    }
    IEnumerator PlayDeathAnimation(float Reqhealth)
    {
        float currentHealth = health;
        health = Reqhealth;
        while (currentHealth > Reqhealth)
        {
            currentHealth -= 0.1f;
            healthSlider.value = currentHealth;
            yield return new WaitForSeconds(0.01f);
        }
        
        healthSlider.value = Reqhealth;
    }
}
