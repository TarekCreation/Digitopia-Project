using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topDownMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement;

    public FixedJoystick Joystick;
    void Start()
    {
#if UNITY_WEBGL
        if (Application.isMobilePlatform)
        {
            Joystick.gameObject.SetActive(true); 
        }else
        {
            Joystick.gameObject.SetActive(false); 
        }
#elif UNITY_ANDROID || UNITY_IOS
    Joystick.gameObject.SetActive(true); 
#else
    Joystick.gameObject.SetActive(false); 
#endif
        
    }
    void Update()
    {
#if UNITY_WEBGL
        if (Application.isMobilePlatform)
        {
            movement.x = Joystick.Horizontal;
            movement.y = Joystick.Vertical;
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
#elif UNITY_ANDROID || UNITY_IOS
    movement.x = Joystick.Horizontal;
    movement.y = Joystick.Vertical;
#else
    movement.x = Input.GetAxisRaw("Horizontal");
    movement.y = Input.GetAxisRaw("Vertical");
#endif
    }
    void FixedUpdate()
    {
        if (GetComponent<PlayerTopDown>().CanControl)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
