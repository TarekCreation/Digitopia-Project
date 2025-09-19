using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour
{
    public GameObject bubble;
    public Transform BubblePos;
    public float timeBetweenBubbles = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateBubble", 0.4f, timeBetweenBubbles);
    }

    // Update is called once per frame
    void CreateBubble()
    {
        float rndSize = Random.Range(0.6f, 2f);
        int rndX = Random.Range(0, 2);
        GameObject _bubble = Instantiate(bubble, BubblePos.position, Quaternion.identity);
        Vector3 size = _bubble.transform.localScale;
        size *= rndSize;
        size.x *= -1 * rndX;
        _bubble.transform.localScale = size;
    }
}
