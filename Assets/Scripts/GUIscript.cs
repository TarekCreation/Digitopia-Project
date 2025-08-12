using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class GUIscript : MonoBehaviour
{
    public GameObject PauseMenu;
    private bool isPaused = false;
 
    
    
    
    bool CanWin = false;
 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Cancel"))
        {
            if (!isPaused)
            {
                isPaused = true;
                
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
            }else
            {
                isPaused = false;
                
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
            }
        }
        
        
            
        
        if (CanWin)
        {
            gameObject.GetComponent<GameCanvas>().Win();
            
        }
    }
    public void Retry()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void Menu()
    {
        Time.timeScale = 1;
        
        // Destroy(GameObject.FindGameObjectWithTag("Music").gameObject);
        SceneManager.LoadScene("MainMenu");
    }
    public void Pause()
    {
        
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }
    
    public void closePause()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        
    }
    
    
 
    
    
}
