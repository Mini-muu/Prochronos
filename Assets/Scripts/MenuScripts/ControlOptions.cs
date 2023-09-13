using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ControlOptions: MonoBehaviour
{
    public void BackGame()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void checkButton()
    {
        //int getButton;
        Debug.Log("Checking button...");
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode))
            {
                Debug.Log("KeyCode down: " + kcode);
                //getButton = Input.GetKey(kcode);
            }
        }
    }
}
