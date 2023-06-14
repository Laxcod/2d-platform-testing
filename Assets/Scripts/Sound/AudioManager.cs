using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource bgmAudio;
    [SerializeField] AudioSource sfxAudio;

    [Header("---------- Audio Clip ----------")]
    public AudioClip BGM;
    public AudioClip collectHeart;
    public AudioClip playerDamaged;
    public AudioClip playerAtkShot;
    public AudioClip playerAtkSword;
    public AudioClip playerJump;
    public AudioClip playerWalk;
    public AudioClip wetEnemyDeath;
    public AudioClip dryEnemyDeath;
    public AudioClip gameOver;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        bgmAudio.clip = BGM;
        bgmAudio.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxAudio.PlayOneShot(clip);
    }
}