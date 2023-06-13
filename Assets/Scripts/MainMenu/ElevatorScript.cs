using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorScript : MonoBehaviour
{
    public GameObject player;
    public Sprite OpenDoorImage;
    public Sprite CloseDoorImage;
    public float TimeBeforeNextScene;
    public bool PlayerIsAtTheDoor;
    public CanvasGroup canvasGroup;
    public bool fadein = false;
    public bool fadeout = false;
    public float TimeToFade;

    private void Update()
    {
        if(PlayerIsAtTheDoor == true)
        {
            if(fadein == true)
            {
                if(canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha += TimeToFade * Time.deltaTime;
                    if(canvasGroup.alpha >= 1)
                    {
                        fadein = false;
                    }
                }
            }

            if(fadeout == true)
            {
                if(canvasGroup.alpha >= 0)
                {
                    canvasGroup.alpha -= TimeToFade * Time.deltaTime;
                    if(canvasGroup.alpha == 0)
                    {
                        fadeout = false;
                    }
                }
            }
            StartCoroutine(_OpenDoor());
        }
    }

    public void FadeIn()
    {
        fadein = true;
    }

    public void FadeOut()
    {
        fadeout = true;
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.gameObject.tag == "Player")
        {
            PlayerIsAtTheDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerIsAtTheDoor = false;
    }

    public IEnumerator _OpenDoor()
    {
        transform.GetComponent<SpriteRenderer>().sprite = OpenDoorImage;
        yield return new WaitForSeconds(TimeBeforeNextScene);
        player.SetActive(false);
        yield return new WaitForSeconds(TimeBeforeNextScene);
        transform.GetComponent<SpriteRenderer>().sprite = CloseDoorImage;
        yield return new WaitForSeconds(TimeBeforeNextScene);
        SceneManager.LoadScene("Gameplay 1");
    }
}
