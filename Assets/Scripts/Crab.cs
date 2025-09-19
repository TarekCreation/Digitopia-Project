using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public float health = 3f;
    public GameObject DeathParticles;
    public List<SpriteRenderer> sprites;
    public Color HitColor;
    public GameObject particles;
    // Start is called before the first frame update
    public void SpawnParticles()
    {
        Instantiate(particles, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
    }
    public void ReColor(Color mycolor)
    {
        foreach (var item in sprites)
        {
            item.color = mycolor;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.GetComponent<Bullet>().Die();
            if (health > 1)
            {

                health -= 1f;
                StartCoroutine(Hit());


            }
            else
            {
                StartCoroutine(Hit());
                StartCoroutine(Death());
            }

        }
    }
    IEnumerator Hit()
    {
        Color actualColor = sprites[0].color;
        ReColor(HitColor);
        yield return new WaitForSeconds(0.06f);
        ReColor(actualColor);
    }
    IEnumerator Death()
    {
        GetComponent<Collider2D>().enabled = false;
        Score score = FindObjectOfType<Score>();

        score.UpdateScore(2);
        FindObjectOfType<playerMovement>().IncreaseNumberOfKilledViruses();

        Instantiate(DeathParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.15f);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
