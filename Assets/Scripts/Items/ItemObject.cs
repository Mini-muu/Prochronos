using UnityEngine;

public class ItemObject : MonoBehaviour
{

    [SerializeField] private ItemData itemData;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = $"Item Object - {itemData.name}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            if (itemData.itemType == ItemType.Equipment)
            {
                ItemData_Equipment equipment = itemData as ItemData_Equipment;
                Inventory.instance.EquipItem(equipment);
            }
            else
            {
                Inventory.instance.AddItem(itemData);
            }

            Destroy(gameObject);
        }
    }
}
