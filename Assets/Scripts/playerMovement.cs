using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
	public CharacterController2D controller;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	public GameObject bulletPrefab;
	public Transform gunPoint;
	public GameObject AndroidUI;
	public FixedJoystick Joystick; 
    void Start()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        AndroidUI.SetActive(false);
#elif UNITY_ANDROID || UNITY_IOS
        AndroidUI.SetActive(true); 
#endif
    }

	// Update is called once per frame
	void Update()
	{
#if UNITY_EDITOR || UNITY_STANDALONE
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}
		if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
		
		
#elif UNITY_ANDROID || UNITY_IOS
        
		horizontalMove = Joystick.Horizontal * runSpeed;
#endif




	}

	void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
	public void Shoot()
    {
		if (transform.localScale.x > 0)
		{
			Instantiate(bulletPrefab, gunPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, -90f)));

		}else
		{
			Instantiate(bulletPrefab, gunPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 90f)));

		}
        
    }
}
