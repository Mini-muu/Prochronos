using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar_UI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private Slider slider;

    private void Start()
    {
        slider = GetComponentInChildren<Slider>();

        playerStats.onHealthChanged += UpdateHealthUI;

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealth;
    }
}