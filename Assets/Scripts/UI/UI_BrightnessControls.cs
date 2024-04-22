using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UI_BrightnessControls : MonoBehaviour
    {
        [SerializeField] private BrightnessSettings brightnessSettings;

        public void OnButtonPress(float value)
        {
            brightnessSettings.brightnessSlider.value += value;
            brightnessSettings.AdjustBrightness(brightnessSettings.brightnessSlider.value);
        }

        public void OnSliderChange(float value)
        {
            brightnessSettings.AdjustBrightness(value);
        }
    }
}