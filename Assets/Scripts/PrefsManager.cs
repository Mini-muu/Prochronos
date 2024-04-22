using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    [SerializeField] private string brightnessPrefName = ""; //Brightness
    [SerializeField] private string localeKeyPrefName = ""; //LocaleKey
    [SerializeField] private string playedPrefName = ""; //played

    public static PrefsManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
    }

    #region Brightness
    public bool HasBrightnessKey() => PlayerPrefs.HasKey(brightnessPrefName);
    public float GetBrightness() => PlayerPrefs.GetFloat(brightnessPrefName, 5);
    public void SetBrightnes(float value) => PlayerPrefs.SetFloat(brightnessPrefName, value);
    #endregion

    #region Locale
    public bool HasLocaleKey() => PlayerPrefs.HasKey(localeKeyPrefName);
    public int GetLocale() => PlayerPrefs.GetInt(localeKeyPrefName, 1);
    public void SetLocale(int value) => PlayerPrefs.SetInt(localeKeyPrefName, value);
    #endregion

    #region Played
    public bool HasPlayedKey() => PlayerPrefs.HasKey(playedPrefName);
    public int GetPlayed() => PlayerPrefs.GetInt(playedPrefName, 0);
    public void SetPlayed(int value) => PlayerPrefs.SetInt(playedPrefName, value);
    #endregion
}
