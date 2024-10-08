using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI explanationText;
    [SerializeField] private GameObject explanationWindow;

    [SerializeField] private SignData data;
    [SerializeField] private TextMeshPro signText;
    [SerializeField] private bool isDemoTutorial = false;

    [SerializeField] private GameObject interactionObject;

    private void OnValidate()
    {
        if (data == null) return;

        Setup();
    }

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        gameObject.name = isDemoTutorial ? $"DemoSign - {data.SignName}" : $"Sign - {data.SignName}";
        GetComponent<Collider2D>().isTrigger = true;
        signText = GetComponentInChildren<TextMeshPro>();

        if (isDemoTutorial)
        {
            signText.text = "! ! !";
            explanationText.text = data.SignText;
        }
        else
        {
            signText.text = data.SignText;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (isDemoTutorial)
            {
                ShowInteractible();
                PlayerInputManager.instance.interact.performed += InteractionPerformed;
            }
            else
            {
                data.UnlockActions();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isDemoTutorial)
        {
            HideInteractible();
            CloseExplanationWindow();
            PlayerInputManager.instance.interact.performed -= InteractionPerformed;
        }
    }

    private void ShowInteractible()
    {
        interactionObject.SetActive(true);
        string input = PlayerInputManager.instance.interact.GetBindingString();
        interactionObject.GetComponent<TextMeshPro>().text = input;
    }

    private void HideInteractible() => interactionObject.SetActive(false);

    private void InteractionPerformed(InputAction.CallbackContext ctx)
    {
        if (!explanationWindow.activeSelf)
        {
            OpenExplanationWindow();
            data.UnlockActions();
        }
        else
        {
            CloseExplanationWindow();
        }
    }

    private void OpenExplanationWindow() => explanationWindow.SetActive(true);

    private void CloseExplanationWindow() => explanationWindow.SetActive(false);
}