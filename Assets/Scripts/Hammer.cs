using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private Animator animator;
    private bool isMovingHammer = false;
    private Vector2 mousePosition;
    public Vector2 offset = new Vector2(0, 0); 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void FinishedHammerAnimation()
    {
        isMovingHammer = false;
    }
    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition + offset;
        if (Input.GetMouseButtonDown(0) && !isMovingHammer)
        {
            isMovingHammer = true;
            animator.Play("HammerHit");
        }
    }
}
