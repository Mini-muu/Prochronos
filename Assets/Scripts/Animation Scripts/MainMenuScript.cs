using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Animator anim;

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsButton()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.Play("OptionTransition");
    }

    public void BackButton()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.Play("BackToMain");
    }
}
