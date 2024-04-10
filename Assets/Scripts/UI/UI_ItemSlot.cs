using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UI_ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    public InventoryItem item = null;

    public void UpdateSlot(InventoryItem _newItem)
    {
        if (_newItem.stackSize <= 0)
        {
            itemImage.color = Color.clear;
            Debug.Log($"Stack Lenght {_newItem.stackSize}");
        }
        else
        {
            Debug.Log($"Stack Lenght UI {_newItem.stackSize}");

            item = _newItem;

            itemImage.color = Color.white;

            if (item != null)
            {
                itemImage.sprite = item.data.icon;

                if (item.stackSize > 1)
                {
                    itemText.text = item.stackSize.ToString();
                }
                else
                {
                    itemText.text = "";
                }
            }
        }
    }

    public void UpdateSlot()
    {
        itemImage.color = Color.clear;
    }

}
