using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    AudioManager audioManager;

    private void Awake()
    {
        if(!UIManager.isPaused)
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }
    }

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!UIManager.isPaused)
        {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            if(!canFire)

            {
                timer += Time.deltaTime;
                if(timer > timeBetweenFiring)
                {
                    canFire = true;
                    timer = 0;
                }
            }

            if(Input.GetMouseButton(0) && canFire)
            {
                audioManager.PlaySFX(audioManager.playerAtkShot);
                canFire = false;
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            }
        }
    }
}
