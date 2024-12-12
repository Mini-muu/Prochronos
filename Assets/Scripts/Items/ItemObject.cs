using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;

    private void OnValidate()
    {
        if (itemData == null) return;

        SetUpItemData();
    }

    private void SetUpItemData()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = $"Item Object - {itemData.name}";
    }

    //TODO - Adapt to Graphics
    public void PickupItem()
    {
        switch (itemData.itemType)
        {
            case ItemType.Bones:
                BonesData bones = itemData as BonesData;
                bones.GenerateAmount();
                PlayerManager.instance.wallet.IncreaseAmountBy(bones.DroppedAmount);
                break;
            case ItemType.Equipment:
                ItemData_Equipment equipment = itemData as ItemData_Equipment;
                UniqueInventory.instance.EquipItem(equipment);
                break;
            case ItemType.Consumable:
            case ItemType.Meat:
            default:
                if (!UniqueInventory.instance.AreUpperUISlotsFull())
                    UniqueInventory.instance.AddItem(itemData);
                else
                    return;
                break;
        }

        //TODO - Item Effects on Equip, Pickup, Use
        //itemData.ExecuteItemEffects();

        Destroy(gameObject);
    }

    public void SetupItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;

        SetUpItemData();
    }
}