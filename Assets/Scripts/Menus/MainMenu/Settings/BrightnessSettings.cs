using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BrightnessSettings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private VolumeProfile volumeProfile;
    private ColorAdjustments brightnessSettings;

    public Slider brightnessSlider;

    public float defaultBrightness = 5f;

    private void Start()
    {
        volumeProfile.TryGet(out brightnessSettings);

        if (PrefsManager.instance.HasBrightnessKey())
        {
            LoadBrightness();
        }
        else
        {
            SetDefaultBrightness();
        }

        AdjustBrightness(brightnessSlider.value);
    }

    public void AdjustBrightness(float value)
    {
        if (value != 0)
        {
            value = TranslateValue(value);
            brightnessSettings.postExposure.value = value;
        }
        else
        {
            brightnessSettings.postExposure.value = -2f;
        }
        valueText.text = brightnessSettings.postExposure.value.ToString();

        PrefsManager.instance.SetBrightnes(brightnessSlider.value);
    }

    private float TranslateValue(float value)
    {
        float fixedValue = 0.4f;

        if (value == 5) return 0;
        if (value > 5) return ((value - 5) * fixedValue);
        else return -(2 - (value * fixedValue));
    }

    public void SetDefaultBrightness()
    {
        brightnessSlider.value = defaultBrightness;
    }

    // Carica i valori degli slider
    public void LoadBrightness()
    {
        float brightnessValue = PrefsManager.instance.GetBrightness();
        brightnessSlider.value = brightnessValue;
    }


}
