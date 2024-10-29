using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver
{
    public GameOver()
    {
        LoadDemo();
    }

    private void LoadDemo() => SceneManager.LoadScene("DemoTutorial");

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
