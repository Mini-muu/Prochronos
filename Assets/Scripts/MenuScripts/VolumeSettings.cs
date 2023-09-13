using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [SerializeField] private AudioMixer _mixer;
    
    // All'inizio del gioco, se esistono, carica i valori dei volumi salvati
    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            LoadMasterVolume();
        }
        else
        {
            SetMasterVolume();
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadMusicVolume();
        }
        else
        {
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadSFXVolume();
        }
        else
        {
            SetSFXVolume();
        }
    }

    // Salva i valori degli slider
    public void SetMasterVolume()
    {
        float volume = _masterSlider.value;
        _mixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        _mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = _sfxSlider.value;
        _mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    // Carica i valori degli slider (se esistono)
    private void LoadMasterVolume()
    {
        _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");

        SetMasterVolume();
    }

    private void LoadMusicVolume()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");

        SetMusicVolume();
    }

    private void LoadSFXVolume()
    {
        _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetSFXVolume();
    }
}
