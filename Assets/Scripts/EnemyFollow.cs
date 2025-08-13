using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform playerTransform = null;
    public float speed = 3.0f;
    public float health = 3f;
    public GameObject DeathParticles;
    private Rigidbody2D rb;
    public float knockbackForce = 5f;
    public bool CanControl = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform != null)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) > 1)
            {
                Vector3 direction = playerTransform.position - transform.position;
                direction.Normalize();
                transform.position += direction * speed * Time.deltaTime;
            }

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerTopDown>().GetHit(transform);

        }    
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            if (health > 1)
            {

                health -= 1f;
                GetComponent<Animator>().Play("EnemyHit");
                CanControl = false;
                StartCoroutine(ReActivateControl());
                rb.velocity = Vector2.zero;
                Vector2 direction = other.GetComponent<Bullet>().GetGlobalUp().normalized;
                rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

            }
            else
            {
                GetComponent<Animator>().Play("EnemyHit");
                CanControl = false;
                StartCoroutine(ReActivateControl());
                rb.velocity = Vector2.zero;
                Vector2 direction = other.GetComponent<Bullet>().GetGlobalUp().normalized;
                rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
                GetComponent<Collider2D>().enabled = false;
                StartCoroutine(Death());
            }

        }
    }
    IEnumerator ReActivateControl()
    {
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        CanControl = true;
        
    }
    IEnumerator Death()
    {
        Instantiate(DeathParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}
