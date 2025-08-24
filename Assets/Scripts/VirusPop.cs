using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class VirusPop : MonoBehaviour
{

    private Animator animator;
    public bool isMoving = false;
    public bool isDying = false;
    private CinemachineImpulseSource impulseSource;
    public VirusColor virusColor;
    // Start is called before the first frame update
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        animator = GetComponent<Animator>();
        InvokeRepeating("PopUp", Random.Range(1f, 10f), Random.Range(2f, 25f));
    }
    public void FinishedPopAnimation()
    {
        isMoving = false;
    }
    public void FinishedDyingAnimation()
    {
        isDying = false;
        virusColor.SetRandomVirusType();
    }
    // Update is called once per frame
    public void PopUp()
    {
        if (!isMoving && !isDying)
        {
            isMoving = true;
            animator.Play("VirusJump");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hammer"))
        {
            isDying = true;
            isMoving = false;
            animator.Play("EnemyDie");
            CamShakeManager.Instance.CameraShake(impulseSource, 1f);
            Score score = FindObjectOfType<Score>();
            if (score != null)
            {
                score.UpdateScore(1);
            }
        }
    }
}
