using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIscript : MonoBehaviour
{
    public GameObject PauseMenu;
    private bool isPaused = false;

    public GameOverScreen gameOverScreen;




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
            }
            else
            {
                isPaused = false;

                Time.timeScale = 1;
                PauseMenu.SetActive(false);
            }
        }

    }
    public void Retry()
    {
        FindObjectOfType<SceneLoader>().LoadScene_WithTheSameMusic(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1;
    }
    public void Menu()
    {
        Time.timeScale = 1;
        FindObjectOfType<SceneLoader>().LoadMenuAsynchronously();
    }
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    public void closePause()
    {
        isPaused = false;
        Time.timeScale = 1;
        PauseMenu.SetActive(false);

    }
    
    
    public void EndGame(int _1Stat,int? _2Stat = null,int? _3Stat = null,int? _4Stat = null)
    {
        StartCoroutine(EndGameCoroutine(_1Stat,_2Stat,_3Stat,_4Stat));
    }
    IEnumerator EndGameCoroutine(int _1Stat,int? _2Stat = null,int? _3Stat = null,int? _4Stat = null)
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        gameOverScreen.gameObject.SetActive(true);
        gameOverScreen.UpdateData(_1Stat,_2Stat,_3Stat,_4Stat);
    }
    
    
}
