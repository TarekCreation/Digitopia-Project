using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private Animator animator;
    private bool isMovingHammer = false;
    private Vector2 movingPosition;
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
#if UNITY_EDITOR || UNITY_STANDALONE
        movingPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = movingPosition + offset;
        if (Input.GetMouseButtonDown(0) && !isMovingHammer)
        {
            isMovingHammer = true;
            animator.Play("HammerHit");
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                movingPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, Camera.main.nearClipPlane));
                transform.position = movingPosition + offset;
                if (!isMovingHammer)
                {
                    isMovingHammer = true;
                    animator.Play("HammerHit");
                }
            }
        }
#endif
    }
}
