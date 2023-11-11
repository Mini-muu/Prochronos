using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerStaminaBar : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private Slider slider;

    private void Start()
    {
        slider = GetComponentInChildren<Slider>();

        playerStats.onStaminaChanged += UpdateStaminaUI;

        UpdateStaminaUI();
    }

    private void UpdateStaminaUI()
    {
        slider.maxValue = playerStats.GetMaxStaminaValue();
        slider.value = playerStats.currentStamina;
    }
}