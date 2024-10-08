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
