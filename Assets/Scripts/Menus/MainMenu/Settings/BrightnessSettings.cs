using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using TMPro;

public class BrightnessSettings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueText;

    public Slider brightnessSlider;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;

    public float defaultBrightness = 1f;

    private AutoExposure exposure;

    //TODO - Fix Brightness -> weird behaviour on game start
    //https://www.youtube.com/watch?v=XiJ-kb-NvV4

    // All'inizio del gioco, se esistono, carica i valori dei volumi salvati
    private void Start()
    {
        //TODO - Fix Possible leak cause
        // -> brightness.TryGetSettings(out exposure);
        exposure = brightness.GetSetting<AutoExposure>();
        
        if (PlayerPrefs.HasKey("Brightness"))
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
            exposure.keyValue.value = value/5.0f;
        } else
        {
            exposure.keyValue.value = 0.05f;
        }
        valueText.text = exposure.keyValue.value.ToString();

        PlayerPrefs.SetFloat("Brightness", value*5);
    }

    public void SetDefaultBrightness()
    {
        brightnessSlider.value = defaultBrightness*5;
        PlayerPrefs.SetFloat("Brightness", defaultBrightness);
    }

    // Carica i valori degli slider
    public void LoadBrightness()
    {
        float brightnessValue = PlayerPrefs.GetFloat("Brightness")/5;
        brightnessSlider.value = brightnessValue;
    }
}
