using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    [SerializeField] public GameObject menu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu.activeSelf)
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
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        menu.SetActive(false);
    }

    public void closeMenuWithUI()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        closeMenuWithUI();
    }

    public bool getIsOpen()
    {
        return pauseMenu.activeSelf;
    }
}