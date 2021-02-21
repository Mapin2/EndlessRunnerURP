using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;

    [SerializeField] GameObject pauseMenu = null, gameOverMenu = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.isGameActive)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        AudioManager.Instance.Play("ConfirmButton");
        AudioManager.Instance.Play("GameTheme");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void PlayAgain()
    {
        AudioManager.Instance.Play("ConfirmButton");
        AudioManager.Instance.Play("GameTheme");
        GameManager.Instance.isGameActive = true;
        SceneLoaderManager.Instance.FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        AudioManager.Instance.Pause("GameTheme");
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        AudioManager.Instance.Play("ConfirmButton");
        AudioManager.Instance.Stop("GameTheme");
        SceneLoaderManager.Instance.FadeToLevel(0);
        AudioManager.Instance.Play("MenuTheme");
        GameManager.Instance.isGameActive = true;
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        AudioManager.Instance.Play("CancelButton");
        Application.Quit();
    }

    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
}
