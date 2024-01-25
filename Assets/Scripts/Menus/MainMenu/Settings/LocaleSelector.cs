using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    //https://www.youtube.com/watch?v=qcXuvd7qSxg

    private bool active = false;
    private TMP_Dropdown localeDropdown;

    private void OnEnable()
    {
        localeDropdown = GetComponent<TMP_Dropdown>();
        int ID = PlayerPrefs.GetInt("LocaleKey", 1);
        localeDropdown.value = ID;
        //ChangeLocale(ID);
    }

    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 1);
        ChangeLocale(ID);
    }

    public void GetSelectedLocale()
    {
        int localeID = localeDropdown.value;
        ChangeLocale(localeID);
    }

    private void ChangeLocale(int localeID)
    {
        if (active)
        {
            return;
        }
        StartCoroutine(SetLocale(localeID));
        PlayerPrefs.SetInt("LocaleKey", localeID);
    }

    private IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        active = false;
    }
}
