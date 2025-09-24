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
	public int numberOfKilledViruses = 0;
	public int numberOfLostItems = 0;
	public int numberOfCollectedEmails = 0;
	public int numberOfDestoyedGoodEmails = 0;
	public int numberOfDestoyedBadEmails = 0;
	public GameObject deathParticles;
	public bool CanControl = true;
	public GameObject Visual;
	private Rigidbody2D rb;
	public bool isMinigame5 = false;
	public Transform gunPoint_UP;
	public SpriteRenderer VisualSprite;
	public Sprite LookingUpSprite;
	public Sprite NormalSprite;
	public bool CanShootUpwards = true;
	public Animator ChargingSprite;
	public bool CanShootNormally = true;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
		AndroidUI.SetActive(false);
#elif UNITY_ANDROID || UNITY_IOS
        AndroidUI.SetActive(true); 
#endif
	}
	public void IncreaseNumberOfKilledViruses()
	{
		numberOfKilledViruses++;
	}
	public void IncreaseNumberOfLostItems()
	{
		numberOfLostItems++;
	}
	public void IncreaseNumberOfDestoyedBadEmails()
	{
		numberOfDestoyedBadEmails++;
	}
	public void IncreaseNumberOfDestoyedGoodEmails()
	{
		numberOfDestoyedGoodEmails++;
	}
	public void IncreaseNumberOfCollectedEmails()
	{
		numberOfCollectedEmails++;
	}
	// Update is called once per frame
	void Update()
	{
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
		if (CanControl)
		{
			horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
			if (Input.GetButtonDown("Jump"))
			{
				jump = true;
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			Shoot();
		}
		if (Input.GetMouseButtonDown(1) && isMinigame5)
		{
			ShootUpwards();
		}
		


#elif UNITY_ANDROID || UNITY_IOS
        if (CanControl)
		{
			horizontalMove = Joystick.Horizontal * runSpeed;
		}
#endif




	}

	void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
	public void Shoot()
	{
		if (CanControl && CanShootNormally)
		{
			FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.shoot,0.4f);
			if (transform.localScale.x > 0)
			{
				Instantiate(bulletPrefab, gunPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, -90f)));

			}
			else
			{
				Instantiate(bulletPrefab, gunPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 90f)));

			}
		}
		

	}
	public void ShootUpwards()
	{

		StartCoroutine(ShootingUpwards());
		
	}
	IEnumerator ShootingUpwards()
	{
		if (CanShootUpwards)
		{
			CanShootNormally = false;
			CanShootUpwards = false;
			VisualSprite.sprite = LookingUpSprite;
			yield return new WaitForSeconds(0.15f);
			FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.shoot,0.4f,0.9f);
			Instantiate(bulletPrefab, gunPoint_UP.transform.position, Quaternion.Euler(new Vector3(0, 0, 0f)));
			yield return new WaitForSeconds(0.15f);
			VisualSprite.sprite = NormalSprite;
			ChargingSprite.Play("Charge", -1, 0);
			CanShootNormally = true;
			yield return new WaitForSeconds(1f);
			CanShootUpwards = true;
			
			
		}
		
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.tag == "Enemy")
		{
			CanControl = false;
			StartCoroutine(Death());
		}
    }
	public void Die()
	{
		
		StartCoroutine(Death());
	}
    IEnumerator Death()
    {
		FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.playerDie);
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		Visual.SetActive(false);
        Instantiate(deathParticles, transform.position, Quaternion.identity);
		GetComponent<Collider2D>().enabled = false;
        GetComponent<CharacterController2D>().enabled = false;
        yield return new WaitForSeconds(0.15f);
        if (isMinigame5)
		{
			FindObjectOfType<GUIscript>().EndGame(numberOfCollectedEmails, numberOfKilledViruses, numberOfDestoyedBadEmails, numberOfDestoyedGoodEmails);
		}else
		{
			FindObjectOfType<GUIscript>().EndGame(numberOfKilledViruses, numberOfLostItems);
		}
        
        GetComponent<playerMovement>().enabled = false;
    }
}
