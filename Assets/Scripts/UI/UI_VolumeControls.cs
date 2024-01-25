using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeControls : MonoBehaviour
{
    public Slider slider;
    public string parameter;
    public float defaultValue = 5f;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float multiplier = 25f;

    //TODO - Temp, maybe remove
    private void Start()
    {
        float volumeValue = defaultValue;

        if (PlayerPrefs.HasKey($"{parameter}Volume"))
        {
            volumeValue = PlayerPrefs.GetFloat($"{parameter}Volume");
            slider.value = volumeValue;
        }

        SliderValue(volumeValue);
    }

    public void ButtonPressed(float _value)
    {
        slider.value += _value;
        SliderValue(slider.value);
    }

    public void SliderValue(float _value)
    {
        audioMixer.SetFloat(parameter, Mathf.Log10(_value / 5f) * multiplier);
        PlayerPrefs.SetFloat($"{parameter}Volume", _value);
    }

    public void LoadSlider(float _value)
    {
        if(_value >= 0.001f)
        {
            slider.value = _value;
        }
    }

    public void Revert()
    {
        slider.value = defaultValue;
        SliderValue(defaultValue);
    }
}
