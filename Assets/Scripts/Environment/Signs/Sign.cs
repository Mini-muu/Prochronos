using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI explanationText;
    [SerializeField] private GameObject explanationWindow;

    [SerializeField] private SignData data;
    [SerializeField] private bool isDemoTutorial = false;

    [SerializeField] private GameObject interactionObject;

    [SerializeField] private List<GameObject> signType;

    private bool hasBeenOpenedOnce = false; // nuova variabile boleana per vedere se il cartello è gia stato attivato o meno

    private void OnValidate()
    {
        if (data == null) return;

        InitialSetup();
    }

    private void SelectSignType() 
    { 
        int randomIndex = Random.Range(0, signType.Count);

        for(int i = 0; i < signType.Count; i++)
        {
            if(randomIndex == i)
                signType[i].SetActive(true);
            else
                signType[i].SetActive(false);
        }
    }

    private void Start()
    {
        SelectSignType();
        InitialSetup();
    }

    private void InitialSetup()
    {
        gameObject.name = isDemoTutorial ? $"DemoSign - {data.SignName}" : $"Sign - {data.SignName}";
        GetComponent<Collider2D>().isTrigger = true;

        explanationText.text = data.SignText;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (!hasBeenOpenedOnce)
            {
                OpenExplanationWindow();
                hasBeenOpenedOnce = true; // in questo caso lo marchia poi come gia aperto
            }
            else
            {
                ShowInteractible();
                PlayerInputManager.instance.interact.performed += InteractionPerformed;
            }

            data.UnlockActions();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            HideInteractible();
            CloseExplanationWindow();
            PlayerInputManager.instance.interact.performed -= InteractionPerformed;   
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