using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject WinParticles;
    // Start is called before the first frame update
    public void Start()
    {
        Time.timeScale = 1;
    }
    
   
    

    
    public void Win()
    {
        
        
        Instantiate(WinParticles, GameObject.FindObjectOfType<playerMovement>().gameObject.transform.position, Quaternion.identity);
        Destroy(GameObject.FindObjectOfType<playerMovement>().gameObject);
        StartCoroutine(WinCoroutine());
        
        
        
    }
    public void Lose()
    {
        
        StartCoroutine(LoseCoroutine());
        
    }
    
    IEnumerator WinCoroutine()
    {
        
        
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        WinScreen.SetActive(true);
        
        
    }
    IEnumerator WWinCoroutine()
    {
        

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        WinScreen.SetActive(true);
        
    }
    IEnumerator LoseCoroutine()
    {
        //Sphere.GetComponent<Ball>().Instantiate();
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
        
        LoseScreen.SetActive(true);
        
    }
    
    
}
