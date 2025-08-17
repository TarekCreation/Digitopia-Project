using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTopDown : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject Arrow;
    private Vector2 mousePosition;
    public float health = 3f;
    public GameObject DeathParticles;
    private Rigidbody2D rb;
    public float knockbackForce = 5f;
    public GameObject[] ThingsToDisableOnDeath;
    public bool CanControl = true;
    public Slider healthSlider;
    Coroutine myCoroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, angle));
        Arrow.transform.rotation = quaternion;
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(angle);
        }
    }
    public void Shoot(float angle)
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle - 90f)));

    }
    IEnumerator ReActivateControl()
    {
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        CanControl = true;

    }
    public void GetHit(Transform enemyTransform)
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
            GetComponent<Animator>().Play("PlayerHit");
            if (enemyTransform != null)
            {
                CanControl = false;
                StartCoroutine(ReActivateControl());
                rb.velocity = Vector2.zero;
                Vector2 direction = (transform.position - enemyTransform.position).normalized;
                rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

            }
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



            GetComponent<Animator>().Play("PlayerDeath");
            if (enemyTransform != null)
            {
                CanControl = false;
                StartCoroutine(ReActivateControl());
                rb.velocity = Vector2.zero;
                Vector2 direction = (transform.position - enemyTransform.position).normalized;
                rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            }
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        Instantiate(DeathParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.15f);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<topDownMovement>().enabled = false;
        for (int i = 0; i < ThingsToDisableOnDeath.Length; i++)
        {
            ThingsToDisableOnDeath[i].SetActive(false);
        }
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<PlayerTopDown>().enabled = false;
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
