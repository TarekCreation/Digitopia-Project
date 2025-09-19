using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class Background : MonoBehaviour
{
    public Image image;
    public float waiting = 0.5f;
    public void ChangeBackground(Sprite sprite){
        StopAllCoroutines();
        StartCoroutine(Waiting(sprite));
    }
    IEnumerator Waiting(Sprite sprite)
    {
        GetComponent<Animator>().Play("ChangeBackground");
        yield return new WaitForSeconds(waiting);
        image.sprite = sprite;
    }
}
