using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene3 : MonoBehaviour
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
        SceneManager.LoadScene("Chapter1-End");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(_ChangeScene());
        }
    }
}
