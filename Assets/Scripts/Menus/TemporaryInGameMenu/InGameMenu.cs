using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Menus.TemporaryInGameMenu
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private GameObject inGameMenu;

        // Use this for initialization
        void Start()
        {
            PlayerInputManager.instance.openMenu.performed += ToggleMenu;
        }

        private void ToggleMenu(InputAction.CallbackContext ctx)
        {
            inGameMenu.SetActive(!inGameMenu.activeSelf);
            TogglePause();
        }

        private void TogglePause()
        {
            if(inGameMenu.activeSelf)
            {
                Time.timeScale = 0f;
                PlayerInputManager.instance.DisableActions();
            }
            else
            {
                PlayerInputManager.instance.EnableActions(); 
                Time.timeScale = 1f;
            }
        }
    }
}