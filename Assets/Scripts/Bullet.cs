using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 2f;
    public GameObject hitEffect;
    public float effectVolume = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    public Vector2 GetGlobalUp()
    {
        return transform.TransformDirection(Vector2.up);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            Die();
        }
    }
    public void Die()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.explosion,effectVolume);
        Instantiate(hitEffect, transform.position, rotation);
        Destroy(gameObject);
    }
}
