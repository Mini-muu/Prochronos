using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    //https://www.youtube.com/watch?v=qcXuvd7qSxg

    private bool active = false;

    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(ID);
    }

    public void ChangeLocale(int loclaeID)
    {
        if (active)
        {
            return;
        }
        StartCoroutine(SetLocale(loclaeID));
    }

    private IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        active = false; 
    }
}
