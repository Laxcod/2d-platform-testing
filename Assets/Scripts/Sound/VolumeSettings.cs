using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] TMP_Text bgmVolText;
    [SerializeField] TMP_Text sfxVolText;
    [SerializeField] private Toggle muteToggle;

    private void Start()
    {
        if(PlayerPrefs.HasKey("bgmVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetBgmVolume();
            SetSfxVolume();
        }
    }

    public void SetBgmVolume()
    {
        float volume = bgmSlider.value;
        myMixer.SetFloat("BGM", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("bgmVolume", volume);
        SetBgmVolText(bgmSlider.value);
    }

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
        SetSfxVolText(sfxSlider.value);
    }

    private void LoadVolume()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetBgmVolume();
        SetSfxVolume();
    }

    public void SetBgmVolText(float value)
    {
        bgmVolText.text = Mathf.RoundToInt(value * 100).ToString();
    }

    public void SetSfxVolText(float value)
    {
        sfxVolText.text = Mathf.RoundToInt(value * 100).ToString();
    }

    void OnEnable()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume", 0);
    }

    void OnDisable()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1);
    }

    public void MuteSound(bool value)
    {
        if(value)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
        PlayerPrefs.Save();
    }
}