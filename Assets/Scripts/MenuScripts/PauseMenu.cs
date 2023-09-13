using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool isOpen = false;
    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isOpen && pauseMenu.active)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void OpenMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        isOpen = true;
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isOpen = false;
    }

    public void closeMenuWithUI()
    {
        Time.timeScale = 1;
        isOpen = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        closeMenuWithUI();
    }

    public bool getIsOpen()
    {
        return isOpen;
    }
}