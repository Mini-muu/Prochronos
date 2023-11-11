using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public bool isMenuOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    private void OpenMenu()
    {
        Time.timeScale = 0f;
        isMenuOpen = true;

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    private void CloseMenu()
    {
        Time.timeScale = 1f;
        isMenuOpen = false;

        SceneManager.UnloadSceneAsync("MainMenu");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}