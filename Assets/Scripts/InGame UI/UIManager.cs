using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public static bool isPaused;
    public Image fadePanel;
    private bool isFading;


    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += EnableGameOver;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= EnableGameOver;
    }

    public void EnableGameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        isFading = true;
        fadePanel.gameObject.SetActive(true);
        fadePanel.canvasRenderer.SetAlpha(0f);
        fadePanel.CrossFadeAlpha(1f, 1f, true);
        yield return new WaitForSeconds(1f);

        fadePanel.canvasRenderer.SetAlpha(1f);

        gameOverMenu.SetActive(true);
        yield return new WaitForSeconds(1f);

        fadePanel.CrossFadeAlpha(0f, 1f, true);
        yield return new WaitForSeconds(1f);

        fadePanel.gameObject.SetActive(false);
        isFading = false;

        

        //isFading = false;
        //fadePanel.CrossFadeAlpha(0f, 1f, true);
        //yield return new WaitForSeconds(1f);





    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (isFading)
        {
            fadePanel.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
