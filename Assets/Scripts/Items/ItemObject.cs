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

    public void PickupItem()
    {
        if (itemData.itemType == ItemType.Equipment)
        {
            ItemData_Equipment equipment = itemData as ItemData_Equipment;
            Inventory.instance.EquipItem(equipment);
        }
        else if (itemData.itemType == ItemType.Bones)
        {
            BonesData bones = itemData as BonesData;
            PlayerManager.instance.IncreaseAmountBy(bones.amount);
        } else
        {
            Inventory.instance.AddItem(itemData);
            //itemData.ExecuteItemEffects();
        }

        Destroy(gameObject);
    }

    public void SetupItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;

        SetUpItemData();
    }
}

/*
 
    private void Update()
    {
        TryPickUpItem();
    }

    private void TryPickUpItem() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickUpRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                if(IsPickUpUnlocked())
                    PickUp();
            }
        }
    }

    private bool IsPickUpUnlocked() => PlayerManager.instance.unlockedActions.Contains(PlayerAction.AutoPickUp);

    private void PickUp()
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }*/