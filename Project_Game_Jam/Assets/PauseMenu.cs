using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausemenu : MonoBehaviour
{
    static public bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject HealthBar;
    public GameObject CurrencyBar;


    void Start()
    {
        HealthBar.SetActive(true);
        CurrencyBar.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if(GameIsPaused)
        {
            HealthBar.SetActive(false);
            CurrencyBar.SetActive(false);
        }
        else if(!GameIsPaused)
        {
            HealthBar.SetActive(true);
            CurrencyBar.SetActive(true);
        }
        
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        
        pauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
    }

}
