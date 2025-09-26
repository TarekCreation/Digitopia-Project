using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollider : MonoBehaviour
{
    public GameObject Broken;
    public Animator parentAnim;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Instantiate(Broken, transform.position, Quaternion.identity);
            parentAnim.SetTrigger("Stop");
            FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.shieldBreak,0.5f);
            FindObjectOfType<GUIscript>().EndGame(FindObjectOfType<playerMovement>().numberOfKilledViruses, FindObjectOfType<playerMovement>().numberOfLostItems);
            FindObjectOfType<playerMovement>().CanControl = false;
        }
    }
}
