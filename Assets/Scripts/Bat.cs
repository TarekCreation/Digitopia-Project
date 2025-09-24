using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public GameObject DeathParticles;
    public float speed = 3.0f;
    public Transform eyePupil;
    public int value = 3;
    public bool isShooting = false;
    public float TimeBetweenShooting = 1;
    public GameObject Bullet;
    public Transform shootingPos;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.batAppear,1f);
        if (isShooting)
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(2f);
        while (isShooting)
        {
            yield return new WaitForSeconds(Random.Range(3f, TimeBetweenShooting));
            Vector3 direction = (Vector3.zero - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Instantiate(Bullet, shootingPos.position, Quaternion.Euler(new Vector3(0, 0, angle - 90f)));
            FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.batShoot,1f);
            if (TimeBetweenShooting > 8)
            {
                TimeBetweenShooting -= 0.5f;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 0.5f)
        {
            Vector3 direction = (Vector3.zero - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            Vector3 eyePupilPosition = direction * 0.1f;
            eyePupilPosition.z = 0;
            eyePupil.transform.localPosition = eyePupilPosition;
        }
        if (transform.position.magnitude < 10f)
        {
            isShooting = false;
        }

    }
    public void DeathFunction()
    {
        GetComponent<Collider2D>().enabled = false;
        FindObjectOfType<Score>().UpdateScore(value);
        FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.shieldHitBat,1f);
        Instantiate(DeathParticles, transform.position, Quaternion.identity);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
    public void Disappear()
    {
        Instantiate(DeathParticles, transform.position, Quaternion.identity);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
    
}
