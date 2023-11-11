using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

        List<string> resolutionOptions = new List<string>();

        // Aggiungi le opzioni di risoluzione alla lista
        foreach (Resolution resolution in resolutions)
        {
            string option = resolution.width + " x " + resolution.height;
            resolutionOptions.Add(option);
        }

        // Aggiungi le opzioni alla dropdown
        resolutionDropdown.AddOptions(resolutionOptions);

        // Imposta la risoluzione predefinita come quella corrente
        int currentResolutionIndex = GetCurrentResolutionIndex();
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        SetResolution(currentResolutionIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
        // Trova l'oggetto Canvas nella tua scena
        Canvas canvas = FindObjectOfType<Canvas>();

        if (canvas != null)
        {
            // Itera su tutti gli oggetti di gioco nell'oggetto Canvas
            foreach (Transform child in canvas.transform)
            {
                // Scalare l'oggetto di gioco alla metà delle sue dimensioni attuali
                //Vector3 newScale = new Vector3(resolution.width / 3840f, resolution.height / 2160f, 1.0f);
                //child.localScale = newScale;
            }
        }
        else
        {
            Debug.LogWarning("Nessun oggetto Canvas trovato nella scena.");
        }
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
