using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    FadeInOut fadeInOut;

    void Start()
    {
        fadeInOut = FindObjectOfType<FadeInOut>();
    }

    public IEnumerator _ChangeScene()
    {
        fadeInOut.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Gameplay 1");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(_ChangeScene());
        }
    }
}
