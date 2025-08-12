using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAnimation : MonoBehaviour
{
    private Animator anim;
    public float delay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        StartCoroutine(EnableAnimation());
    }
    private IEnumerator EnableAnimation()
    {
        yield return new WaitForSeconds(delay);
        anim.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
