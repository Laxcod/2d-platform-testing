using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    public GameObject LoadingPanel;
    public float MinLoadTime;

    public GameObject LoadingGear;
    [SerializeField] float gearSpeed;

    public Image FadeImage;
    [SerializeField] float FadeTime;

    private string targetScene;
    private bool isLoading;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadingPanel.SetActive(false);
        FadeImage.gameObject.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        targetScene = sceneName;
        StartCoroutine(LoadSceneRoutine());
        Time.timeScale = 1f;
    }

    private IEnumerator LoadSceneRoutine()
    {
        isLoading = true;

        FadeImage.gameObject.SetActive(true);
        FadeImage.canvasRenderer.SetAlpha(0);

        while (!Fade(1))
            yield return null;

        LoadingPanel.SetActive(true);
        StartCoroutine(SpinGearRoutine());

        while (!Fade(0))
            yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(targetScene);
        float elapsedLoadTime = 0f;

        while (!op.isDone)
        {
            yield return null;
        }

        while (elapsedLoadTime < MinLoadTime)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }

        while (!Fade(1))
            yield return null;

        LoadingPanel.SetActive(false);

        while (!Fade(0))
            yield return null;

        isLoading = false;
        FadeImage.gameObject.SetActive(false);
    }

    private bool Fade(float target)
    {
        FadeImage.CrossFadeAlpha(target, FadeTime, true);

        if(Mathf.Abs(FadeImage.canvasRenderer.GetAlpha()- target) <= 0.05f)
        {
            FadeImage.canvasRenderer.SetAlpha(target);
            return true;
        }

        return false;
    }

    private IEnumerator SpinGearRoutine()
    {
        while (isLoading)
        {
            LoadingGear.transform.Rotate(0, 0, -gearSpeed);
            yield return null;
        }
    }
}