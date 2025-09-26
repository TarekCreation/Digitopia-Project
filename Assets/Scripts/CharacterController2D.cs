using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public GameObject particles;
	[SerializeField] private float m_JumpForce = 400f;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
	[SerializeField] private bool m_AirControl = false;
	[SerializeField] private LayerMask m_WhatIsGround;
	[SerializeField] private Transform m_GroundCheck;
	
	const float k_GroundedRadius = .3f;
	private bool m_Grounded;
	private Rigidbody2D m_Rigidbody2D;
	


    float fJumpPressedRemember = 0;
    [SerializeField]
    float fJumpPressedRememberTime = 0.2f;

    float fGroundedRemember = 0f;
    [SerializeField]
    float fGroundedRememberTime = 0.15f;
	[SerializeField]
    [Range(0, 1)]
    float fCutJumpHeight = 0.5f;

	private bool m_FacingRight = true;
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	
	

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}
	
	public void Jump()
	{
		FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.jump,0.5f);
		fJumpPressedRemember = fJumpPressedRememberTime;
	}
	public void NotJump()
	{
		if (m_Rigidbody2D.velocity.y > 0)
		{
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y * fCutJumpHeight);
		}
	}
	// Update is called once per frame
	public void Update()
	{
		fGroundedRemember -= Time.deltaTime;
		if (m_Grounded)
		{
			fGroundedRemember = fGroundedRememberTime;
		}

		fJumpPressedRemember -= Time.deltaTime;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
		if (Input.GetButtonDown("Jump"))
		{
			FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.jump, 0.5f);
			fJumpPressedRemember = fJumpPressedRememberTime;
		}

		if (Input.GetButtonUp("Jump"))
		{
			if (m_Rigidbody2D.velocity.y > 0)
			{
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y * fCutJumpHeight);
			}
		}
#endif
		

		if ((fJumpPressedRemember > 0) && (fGroundedRemember > 0))
		{
			fJumpPressedRemember = 0;
			fGroundedRemember = 0;
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
			fJumpPressedRemember = 0;
			fGroundedRemember = 0;
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		if (m_Grounded || m_AirControl)
		{
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}
		
		
		
        



        
	}


	private void Flip()
	{
		m_FacingRight = !m_FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	public void land()
	{
		Instantiate(particles, m_GroundCheck.transform.position, Quaternion.identity);
	}
}
