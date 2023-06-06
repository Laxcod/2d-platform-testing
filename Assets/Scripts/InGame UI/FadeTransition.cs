using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    public float fadeDuration = 1f;
    public Image fadeImage;
    private bool isFading;

    private void Start()
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
        StartFadeIn();
    }

    private void StartFadeIn()
    {
        fadeImage.CrossFadeAlpha(0f, fadeDuration, false);
    }

    private void StartFadeOut()
    {
        fadeImage.CrossFadeAlpha(1f, fadeDuration, false);
    }

    public void StartFadeTransition()
    {
        if (!isFading)
        {
            isFading = true;
            StartFadeOut();
            Invoke(nameof(CompleteFadeTransition), fadeDuration);
        }
    }

    private void CompleteFadeTransition()
    {
        isFading = false;
    }
}