using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; set; }
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource bgmAudio;
    [SerializeField] AudioSource sfxAudio;

    [Header("---------- Audio Clip ----------")]
    public AudioClip[] bgmClip;
    public AudioClip collectHeart;
    public AudioClip playerDamaged;
    public AudioClip playerAtkShot;
    public AudioClip playerAtkSword;
    public AudioClip playerJump;
    public AudioClip wetEnemyDeath;
    public AudioClip dryEnemyDeath;
    public AudioClip gameOver;

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
    public void PlaySFX(AudioClip clip)
    {
        sfxAudio.PlayOneShot(clip);
    }

    public void ChangeMusic(int indexMusic)
    {
        if(bgmAudio.clip != bgmClip[indexMusic])
        {
            bgmAudio.Stop();
            bgmAudio.clip = bgmClip[indexMusic];
            bgmAudio.Play();
        }
    }

    public void MuteSound()
    {
        if(bgmAudio.mute == false && sfxAudio.mute == false)
        {
            bgmAudio.mute = true;
            sfxAudio.mute = true;
        }
        else
        {
            bgmAudio.mute = false;
            sfxAudio.mute = false;
        }
    }
}
