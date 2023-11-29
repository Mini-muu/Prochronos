using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public GameObject menu;
    public GameObject pAnyK;
    [SerializeField] private Animator zoomInAnimator;

    private void Start()
    {
        //Test Only
        if (PlayerPrefs.GetInt("played") == 1) {
            PlayerPrefs.SetInt("played", 0);
        } 
        
        menu.SetActive(false);
        pAnyK.SetActive(true);
    }

    void Update()
    {
        /*if (PlayerPrefs.GetInt("played") == 0 && Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            menu.SetActive(true);
            pAnyK.SetActive(false);
            PlayerPrefs.SetInt("played", 1);
        }*/

        if (Input.anyKeyDown)
        {
            pAnyK.SetActive(false);
            zoomInAnimator.SetBool("ZoomIn", true);
        }
    }

    public void ActivateMenu()
    {
        menu.SetActive(true);
    }
}