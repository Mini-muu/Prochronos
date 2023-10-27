using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public GameObject menu;
    public GameObject pAnyK;

    private void Start()
    {
        if (PlayerPrefs.GetInt("played") == 1) {
            menu.SetActive(true);
            pAnyK.SetActive(false);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("played") == 0 && Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            menu.SetActive(true);
            pAnyK.SetActive(false);
            PlayerPrefs.SetInt("played", 1);
        }
    }
}