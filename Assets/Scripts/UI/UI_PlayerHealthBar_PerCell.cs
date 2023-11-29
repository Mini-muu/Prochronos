using UnityEngine;
using UnityEngine.UI;

//https://docs.unity3d.com/ScriptReference/Transform.SetAsLastSibling.html
public class UI_PlayerHealthBar_PerCell : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform cellsParent;
    [SerializeField] private Transform cellsBackgroundParent;
    [SerializeField] private Transform enderObj;
    private int currentCellsAmount;

    private void Start()
    {
        playerStats.onHealthChanged += UpdateHealthUI;

        currentCellsAmount = cellsParent.childCount;

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        SetChildrenDuplicates();
        SetValue();
    }

    private void SetValue()
    {
        Image[] childrensImage = cellsBackgroundParent.GetComponentsInChildren<Image>();

        for(int i = 0; i < childrensImage.Length; i++)
        {
            if(i < playerStats.currentHealth)
                childrensImage[i].color = Color.red;
            else
                childrensImage[i].color = new Color32(75,81,85,255);
        }
    }

    private void SetChildrenDuplicates()
    {
        if (currentCellsAmount == playerStats.GetMaxHealthValue() + 1) return;

        if (currentCellsAmount > playerStats.GetMaxHealthValue() + 1)
        {
            RemoveExtraCell();
        }
        else
        {
            AddExtraCell();
        }

        currentCellsAmount = cellsParent.childCount;
    }

    private void AddExtraCell()
    {
        GameObject firstChild = cellsParent.GetChild(0).gameObject;

        GameObject clone = Instantiate(firstChild, cellsParent);
        
        clone.transform.SetAsLastSibling();
        enderObj.SetAsLastSibling();

        AddExtraCellBackground();
    }

    private void AddExtraCellBackground()
    {
        GameObject firstChild = cellsBackgroundParent.GetChild(0).gameObject;

        GameObject clone = Instantiate(firstChild, cellsBackgroundParent);

        clone.transform.SetAsLastSibling();
    }

    private void RemoveExtraCell()
    {
        Destroy(cellsParent.GetChild(currentCellsAmount - 2).gameObject);
        Destroy(cellsBackgroundParent.GetChild(currentCellsAmount - 1).gameObject);

        if (currentCellsAmount > playerStats.GetMaxHealthValue() + 1)
            RemoveExtraCell();
    }
}