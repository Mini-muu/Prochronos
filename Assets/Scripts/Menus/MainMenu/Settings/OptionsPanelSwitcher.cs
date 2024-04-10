using UnityEngine;
using UnityEngine.UI;

public class OptionsPanelSwitcher : MonoBehaviour
{
    [SerializeField] private Button[] panelsButtons;
    [SerializeField] private GameObject[] panels;
    private int activePanelIndex = 0;

    public void NextPanel()
    {
        panels[activePanelIndex].SetActive(false);
        activePanelIndex = activePanelIndex == panels.Length - 1 ? 0 : activePanelIndex + 1;
        panels[activePanelIndex].SetActive(true);

        SetInteractableButtons();
    }

    public void PrevPanel()
    {
        panels[activePanelIndex].SetActive(false);
        activePanelIndex = activePanelIndex == 0 ? panels.Length - 1 : activePanelIndex - 1;
        panels[activePanelIndex].SetActive(true);

        SetInteractableButtons();
    }

    private void SetInteractableButtons()
    {
        for (int i = 0; i < panelsButtons.Length; i++)
        {
            panelsButtons[i].interactable = activePanelIndex != i;
        }
    }
}