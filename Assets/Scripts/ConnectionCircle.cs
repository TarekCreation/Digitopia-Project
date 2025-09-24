using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.EnemyHitTarget,1f);
            collision.gameObject.GetComponent<Bat>().DeathFunction();
            FindObjectOfType<GUIscript>().EndGame(FindObjectOfType<RotatingShield>().numberOfKilledViruses);
            foreach (var item in FindObjectsOfType<Bat>())
            {
                item.Disappear();
            }
            FindObjectOfType<BatSpawner>().enabled = false;
        }
        else if (collision.tag == "Bullet")
        {
            FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.EnemyHitTarget,1f);
            
            collision.gameObject.GetComponent<Bullet>().Die();
            FindObjectOfType<GUIscript>().EndGame(FindObjectOfType<RotatingShield>().numberOfKilledViruses);
            foreach (var item in FindObjectsOfType<Bat>())
            {
                item.Disappear();
            }
            FindObjectOfType<BatSpawner>().enabled = false;
        }
    }
}
