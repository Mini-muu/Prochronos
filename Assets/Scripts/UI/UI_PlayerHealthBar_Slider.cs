using UnityEngine;
using UnityEngine.UI;

//https://docs.unity3d.com/ScriptReference/Transform.SetAsLastSibling.html
public class UI_PlayerHealthBar_Slider : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform cellsParent;
    [SerializeField] private Transform cellsBackgroundParent;
    [SerializeField] private Transform enderObj;
    [SerializeField] private Image filler;
    private int currentCellsAmount;
    private float perCellWidth;

    private void Start()
    {
        playerStats.onHealthChanged += UpdateHealthUI;

        currentCellsAmount = cellsParent.childCount-1;

        perCellWidth = filler.rectTransform.rect.width / 3;

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        filler.fillAmount = (float)playerStats.currentHealth / (float)playerStats.GetMaxHealthValue();

        SetChildrenDuplicates();
    }

    private void SetChildrenDuplicates()
    {
        if (currentCellsAmount == playerStats.GetMaxHealthValue()) return;

        if (currentCellsAmount > playerStats.GetMaxHealthValue())
        {
            RemoveExtraCell();
        }
        else
        {
            AddExtraCell();
        }

        currentCellsAmount = cellsParent.childCount-1;
    }

    private void AddExtraCell()
    {
        GameObject firstChild = cellsParent.GetChild(0).gameObject;

        GameObject clone = Instantiate(firstChild, cellsParent);
        
        clone.transform.SetAsLastSibling();
        enderObj.SetAsLastSibling();

        AddExtraCellBackground();

        Vector2 size = filler.rectTransform.sizeDelta;
        filler.rectTransform.sizeDelta = new Vector2(size.x + perCellWidth, size.y);
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

        Vector2 size = filler.rectTransform.sizeDelta;
        filler.rectTransform.sizeDelta = new Vector2(size.x - perCellWidth, size.y);

        if (currentCellsAmount > playerStats.GetMaxHealthValue())
            RemoveExtraCell();
    }
}