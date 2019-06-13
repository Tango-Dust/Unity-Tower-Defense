using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseScript : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;


    // Update is called once per frame
    void Update()
    {

        //on escape bring up the pause panel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

     private void Pause()
    {
        pauseMenuUI.SetActive(true);

        //disables scripts that still work when time is set to 0
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        

        //enables scripts 
        GameIsPaused = false;
        Time.timeScale = 1f;
    }
    public void LoadMainMenu()
    {
        //loads main menu and sets the timescale back to normal
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        //closes application
        Debug.Log("Quiting");
        Application.Quit();
        
    }
}
