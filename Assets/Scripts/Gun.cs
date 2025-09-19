using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Animator animator;
    private Vector2 movingPosition;
    public int numberOfKilledSpies;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void IncreaseNumberOfKilledSpies()
    {
        numberOfKilledSpies++;
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        movingPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = movingPosition;
        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("Gun", -1, 0);
            FindObjectOfType<Bullets>().Shoot(transform.position);
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                movingPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, Camera.main.nearClipPlane));
                transform.position = movingPosition;
                animator.Play("Gun", -1, 0);
                FindObjectOfType<Bullets>().Shoot(transform.position);
            }
        }
#endif
    }
}
