using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BrightnessSettings : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private Slider _slider;


    // All'inizio del gioco, se esistono, carica i valori dei volumi salvati
    private void Start()
    {
        if (PlayerPrefs.HasKey("Brightness"))
        {
            LoadBrightness();
        }
        else
        {
            SetBrightness();
        }
    }

    // Salva i valori degli slider
    public void SetBrightness()
    {
        float brightness = _slider.value;
        _light.intensity = brightness;
        PlayerPrefs.SetFloat("Brightness", brightness);
    }

    // Carica i valori degli slider
    public void LoadBrightness()
    {
        float brightness = PlayerPrefs.GetFloat("Brightness");
        _light.intensity = brightness;
        _slider.value = brightness;
    }

}
