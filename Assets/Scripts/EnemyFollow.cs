using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Virus1,
    Virus2,
    Virus3,
    Boss
}
public class EnemyFollow : MonoBehaviour
{
    public EnemyType enemyType;
    private Transform playerTransform = null;
    public float speed = 3.0f;
    public float health = 3f;
    public GameObject DeathParticles;
    private Rigidbody2D rb;
    public float knockbackForce = 5f;
    public bool CanControl = true;
    public bool AttackOnContact = false;
    public bool cannotMove = false;
    public float minDistance = 1f;
    public int value = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (cannotMove) return;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform != null)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) > minDistance)
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
            if (!cannotMove)
            {
                if (AttackOnContact)
                {
                    Vector2 direction = (collision.transform.position - transform.position).normalized;
                    rb.velocity = Vector2.zero;
                    rb.AddForce(direction * knockbackForce * 0.5f, ForceMode2D.Impulse);
                    StartCoroutine(ResetVelocity());
                }
                else
                {
                    Vector2 direction = (transform.position - collision.transform.position).normalized;
                    rb.velocity = Vector2.zero;
                    rb.AddForce(direction * knockbackForce * 0.3f, ForceMode2D.Impulse);
                    StartCoroutine(ResetVelocity());
                }
            }



        }


    }
    IEnumerator ResetVelocity()
    {
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.GetComponent<Bullet>().Die();

            if (health > 1)
            {

                health -= 1f;
                GetComponent<Animator>().Play("EnemyHit");
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.enemyHit);
                if (!cannotMove)
                {
                    CanControl = false;
                    StartCoroutine(ReActivateControl());
                    rb.velocity = Vector2.zero;
                    Vector2 direction = other.GetComponent<Bullet>().GetGlobalUp().normalized;
                    rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
                }

            }
            else
            {
                GetComponent<Animator>().Play("EnemyHit");
                switch (enemyType)
                {
                    case EnemyType.Virus1:
                        FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.enemyDie,0.9f,2f);
                        break;
                    case EnemyType.Virus2:
                        FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.enemyDie,0.9f,1f);
                        break;
                    case EnemyType.Virus3:
                        FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.enemyDie,0.9f,3f);
                        break;
                    case EnemyType.Boss:
                        FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.enemyDie,0.9f,0.7f);
                        
                        break;
                }
                
                if (!cannotMove)
                {
                    CanControl = false;
                    StartCoroutine(ReActivateControl());
                    rb.velocity = Vector2.zero;
                    Vector2 direction = other.GetComponent<Bullet>().GetGlobalUp().normalized;
                    rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
                }
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
        
        FindObjectOfType<SpwanManager>().IncreaseKilledEnemyCount(enemyType); 
        
        FindObjectOfType<Score>().UpdateScore(value);
        Destroy(gameObject);
    }
}
