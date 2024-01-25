using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionOptions : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    private void Start()
    {
        // Ottieni la lista delle risoluzioni supportate dallo schermo
        resolutions = Screen.resolutions;

        // Pulisci le opzioni esistenti dalla dropdown
        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>() { "800 x 600", "1280 x 720", "1440 x 900", "1920 x 1080" };

        // Aggiungi le opzioni alla dropdown
        resolutionDropdown.AddOptions(resolutionOptions);

        // Imposta la risoluzione predefinita come quella corrente
        int currentResolutionIndex = GetCurrentResolutionIndex();
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        SetResolution(currentResolutionIndex);
    }

    //https://www.youtube.com/watch?v=YOaYQrN1oYQ
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    private int GetCurrentResolutionIndex()
    {
        Resolution currentResolution = Screen.currentResolution;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentResolution.width && resolutions[i].height == currentResolution.height)
            {
                return i;
            }
        }

        return 0; // Ritorna l'indice 0 se la risoluzione corrente non è stata trovata nella lista
    }

    public void ApplyResolution()
    {
        int selectedResolutionIndex = resolutionDropdown.value;
        SetResolution(selectedResolutionIndex);
    }
}
