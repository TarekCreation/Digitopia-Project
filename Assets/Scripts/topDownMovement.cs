using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topDownMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement;

    public FixedJoystick Joystick;
    
    
#if UNITY_EDITOR || UNITY_STANDALONE
    void Start()
    {
        Joystick.gameObject.SetActive(false); 
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
#elif UNITY_ANDROID || UNITY_IOS
    void Start()
    {
        Joystick.gameObject.SetActive(true); 
    }
    void Update()
    {
        movement.x = Joystick.Horizontal;
        movement.y = Joystick.Vertical;
    }
#endif
    void FixedUpdate()
    {
        if (GetComponent<PlayerTopDown>().CanControl)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
