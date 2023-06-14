using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text textTimer;
    [SerializeField] float Waktu = 100;

    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public GameObject howToPlayMenu;
    public GameObject settings;
    public static bool isPaused;

    public bool isGameover;
    public bool TimerActive = false;
    float s;

    public Image fadePanel;
    private bool isFading;

    void SetText()
    {
        int Minute = Mathf.FloorToInt(Waktu / 60);
        int Seconds = Mathf.FloorToInt(Waktu % 60);
        textTimer.text = Minute.ToString("00") + ":" + Seconds.ToString("00");
    }

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
        pauseMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ExitSettings()
    {
        pauseMenu.SetActive(true);
        settings.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    //public void MainMenu()
    //{
    //    SceneManager.LoadScene("MainMenu");
    //}

    private void Start()
    {
        TimerActive = true;
        pauseMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
    }

    private void Update()
    {
        if (isFading)
        {
            fadePanel.gameObject.SetActive(true);
        }

        //if (finished)
        //    return;

        if (TimerActive)
        {
            s += Time.deltaTime;
            if (s >= 1)
            {
                Waktu--;
                s = 0;
            }
        }

        if (TimerActive && Waktu <= 0)
        {
            TimerActive = false;
            StartCoroutine(GameOverRoutine());
        }

        SetText();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                Resume2();
            }
            else
            {
                HowToPlay();
            }
        }
    }


    public void HowToPlay()
    {
        howToPlayMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume2()
    {
        howToPlayMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
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
        settings.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

}