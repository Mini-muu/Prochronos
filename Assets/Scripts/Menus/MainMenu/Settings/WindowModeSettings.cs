using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowModeSettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private void Start()
    {
        // Registra un listener per il cambiamento dell'opzione nel dropdown
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int newIndex)
    {
        // Ottieni il testo dell'opzione selezionata nel dropdown
        string selectedOption = dropdown.options[newIndex].text.ToLower();

        // Imposta la modalità finestra in base all'opzione selezionata
        switch (selectedOption)
        {
            case "windowed":
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case "fullscreen":
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case "borderless":
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            default:
                break;
        }
    }
}
