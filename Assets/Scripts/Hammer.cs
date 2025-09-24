using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private Animator animator;
    private bool isMovingHammer = false;
    private Vector2 movingPosition;
    public Vector2 offset = new Vector2(0, 0);
    private Score score;
    public int numberOfKilledViruses = 0;
    private int Combo = 0;
    public Animator comboAnimator;
    public TextMeshProUGUI comboText;
    public int ComboMaxSize = 3;
    public int BestCombo = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        score = FindObjectOfType<Score>();
    }
    public void IncreaseNumberOfKilledViruses()
    {
        numberOfKilledViruses++;
        Combo++;
        if (Combo > BestCombo)
        {
            BestCombo = Combo;
        }
        StopAllCoroutines();
        StartCoroutine(ComboResetCoroutine());
        if (Combo > 1)
        {
            
            float targetSize = 1 + (Combo - 2) * 0.2f;
            if (targetSize <= ComboMaxSize)
            {
                
                comboAnimator.transform.localScale = new Vector3(targetSize, targetSize, targetSize);
            }
            else
            {   
                comboAnimator.transform.localScale = new Vector3(ComboMaxSize, ComboMaxSize, ComboMaxSize);
            }
            float targetFloatPitch = targetSize - 0.5f;
            if (targetFloatPitch < -0.5f)
            {
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.comboLow,1,-0.5f);
            }else if (targetFloatPitch > 3)
            {
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.comboLow,1,3);
            }else
            {
                FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.comboLow,1,targetFloatPitch);
            }
            comboAnimator.Play("ComboVisual", -1, 0f);
            comboText.text = "x" + Combo.ToString();
        }
        if (score != null)
        {
            score.UpdateScore(1 + (Combo - 1));
        }
    }
    IEnumerator ComboResetCoroutine()
    {
        int currentCombo = Combo;
        yield return new WaitForSeconds(1f);
        if (currentCombo == Combo)
        {
            Combo = 0;
        }
        
    }
    public void FinishedHammerAnimation()
    {
        isMovingHammer = false;
    }
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        movingPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = movingPosition + offset;
        if (Input.GetMouseButtonDown(0) && !isMovingHammer)
        {
            isMovingHammer = true;
            animator.Play("HammerHit");
            FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.hammerPunch);
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
                    FindObjectOfType<SFXManager>().PlaySound(SFXManager.Instance.hammerPunch);
                }
            }
        }
#endif
    }
}
