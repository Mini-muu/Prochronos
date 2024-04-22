using UnityEngine;
using UnityEngine.InputSystem;

public class PressAnyKey : MonoBehaviour
{
    public GameObject menu;
    public GameObject pAnyK;
    public GameObject topRightButtons;
    [SerializeField] private Animator backgroundAC;
    [SerializeField] private Animator logoAC;

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

        if (IsAnyKeyPressed())
        {
            pAnyK.SetActive(false);
            backgroundAC.SetBool("ZoomIn", true);
            logoAC.SetBool("FadeOut", true);
        }
    }

    public void ActivateMenu()
    {
        menu.SetActive(true);
        topRightButtons.SetActive(true);
    }

    private bool IsAnyKeyPressed()
    {
        return Keyboard.current.anyKey.isPressed || Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed;
    }
}