using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<InventoryItem> inventoryItems;
   // public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    //
      
    public List<KeyValuePair<ItemData, InventoryItem>> inventoryItemsAlt;

    //

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipemntDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    private UI_ItemSlot[] inventoryItemSlot;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventoryItems = new List<InventoryItem>();
        //inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        inventoryItemsAlt = new List<KeyValuePair<ItemData, InventoryItem>>();

        equipment = new List<InventoryItem>();
        equipemntDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Entrato nel for");
            if (!inventoryItems[i].IsUnityNull())
            {
                Debug.Log("Entrato not Null");
                inventoryItemSlot[i].UpdateSlot(inventoryItems[i]);
                Debug.Log("Uscito not Null");
            }
            else
            {
                Debug.Log("Entrato Null");
                inventoryItemSlot[i].UpdateSlot();
                Debug.Log("Uscito Null");
            }
        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(_item);

        //If equipment is unique then check in dictionary its presence

        equipment.Add(newItem);
        equipemntDictionary.Add(newEquipment, newItem);

        newEquipment.AddModifiers();
    }

    public void UnEquipItem(ItemData_Equipment itemToRemove)
    {
        if(equipemntDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipemntDictionary.Remove(itemToRemove);

            itemToRemove.RemoveModifiers();
        }
    }

    public void AddItem(ItemData _item)
    {
        /*if (!IsMeat(_item) && inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        } else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventoryItems.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }*/

        if (!IsMeat(_item) && TryGetValue(_item, out InventoryItem value) != null)
        {
            value.AddStack();
        } else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventoryItems.Add(newItem);
            inventoryItemsAlt.Add(new KeyValuePair<ItemData, InventoryItem>(_item, newItem));
        }
        Debug.Log("Item Added");
        UpdateSlotUI();
    }

    public InventoryItem TryGetValue(ItemData key, out InventoryItem value)
    {
        value = null;

        foreach(var itemPair in inventoryItemsAlt) {
            if(itemPair.Key == key)
            {
                value = itemPair.Value;
            }
        }
        return value;
    }

    private bool IsMeat(ItemData item)
    {
        return item.itemType == ItemType.Meat;
    }

    public void RemoveItem(ItemData _item)
    {
        /*if(inventoryDictionary.TryGetValue(_item,out InventoryItem value) != null)
        {
            if(value.stackSize <= 0)
            {
                inventoryItems.Remove(value);
                inventoryDictionary.Remove(_item);
            } else
            {
                value.RemoveStack();
            }
        }*/

        Debug.Log("ItemID: " + _item.GetInstanceID());

        if(TryGetValue(_item, out InventoryItem value) != null)
        {
            Debug.Log("Stack pre remove " + value.stackSize);
            value.RemoveStack();
            Debug.Log("Stack post remove " + value.stackSize);
            if (value.stackSize <= 0)
            {
                inventoryItems.Remove(value);
                inventoryItemsAlt.Remove(new KeyValuePair<ItemData, InventoryItem>(_item, value));
            }
        }

        UpdateSlotUI();
    }
}
