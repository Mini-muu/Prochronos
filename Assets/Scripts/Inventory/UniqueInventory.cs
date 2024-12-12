using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UniqueInventory : MonoBehaviour, ISaveManager
{
    public static UniqueInventory instance;

    public List<KeyValuePair<ItemData, InventoryItem>> inventoryItems;

    public Dictionary<ItemData_Equipment, InventoryItem> equipment;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    private UI_ItemSlot[] inventoryItemSlot;

    [Header("Database")]
    public List<ItemData> itemDataBase;
    public List<InventoryItem> loadedItems;
    public List<ItemData_Equipment> loadedEquipment;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventoryItems = new List<KeyValuePair<ItemData, InventoryItem>>();

        equipment = new Dictionary<ItemData_Equipment, InventoryItem>();

        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventoryItems[i].Value);
        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = Instantiate(_item as ItemData_Equipment);
        InventoryItem newItem = new InventoryItem(_item);

        equipment.Add(newEquipment, newItem);

        newEquipment.AddModifiers();
    }

    public void UnEquipItem(ItemData_Equipment itemToRemove)
    {
        if (equipment.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(itemToRemove);

            itemToRemove.RemoveModifiers();
        }
    }

    public void AddItem(ItemData _item)
    {
        if (!IsMeat(_item) && TryGetUniqueInventoryItem(_item, out InventoryItem value) != null)
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new(_item);
            inventoryItems.Add(new KeyValuePair<ItemData, InventoryItem>(Instantiate(_item), newItem));
        }

        UpdateSlotUI();
    }

    public InventoryItem TryGetUniqueInventoryItem(ItemData key, out InventoryItem value)
    {
        value = null;

        foreach (var itemPair in inventoryItems)
        {
            if (itemPair.Key == key)
            {
                value = itemPair.Value;
            }
        }
        return value;
    }

    private bool IsMeat(ItemData item) => item.itemType == ItemType.Meat;

    public void RemoveItem(ItemData _item)
    {
        if (TryGetUniqueInventoryItem(_item, out InventoryItem uniqueItem) != null)
        {
            if (uniqueItem.stackSize <= 1)
            {
                inventoryItems.Remove(new KeyValuePair<ItemData, InventoryItem>(_item, uniqueItem));
            }
            else
            {
                uniqueItem.RemoveStack();
            }
        }

        UpdateSlotUI();
    }

    public bool AreUpperUISlotsFull() => inventoryItems.Count > 2;

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, int> pair in _data.inventory)
        {
            foreach (var item in itemDataBase)
            {
                if (item != null && item.itemId == pair.Key)
                {
                    InventoryItem itemToLoad = new InventoryItem(item);
                    itemToLoad.stackSize = pair.Value;

                    loadedItems.Add(itemToLoad);
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();

        foreach (KeyValuePair<ItemData, InventoryItem> pair in inventoryItems)
        {
            _data.inventory.Add(pair.Key.itemId, pair.Value.stackSize);
        }
    }

    public void ReplaceFirstOccurrence(ItemType oldItemType, ItemData newItemData)
    {
        foreach (var item in inventoryItems)
        {
            if (item.Key.itemType == oldItemType)
            {
                int index = inventoryItems.IndexOf(item);
                inventoryItems.RemoveAt(index);
                var newItem = new KeyValuePair<ItemData, InventoryItem>(newItemData, item.Value);
                inventoryItems.Insert(index, newItem);
            }
        }
    }

    public bool HasItem(ItemType type)
    {
        foreach (var item in inventoryItems)
        {
            if (item.Key.itemType == type)
                return true;
        }

        return false;
    }

#if UNITY_EDITOR
    [ContextMenu("Fill up item data base")]
    private void FillUpItemDataBase() => itemDataBase = new List<ItemData>(GetItemDataBase());

    private List<ItemData> GetItemDataBase()
    {
        List<ItemData> itemDataBase = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/ScriptableObjects/Items" });

        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            itemDataBase.Add(itemData);
        }

        return itemDataBase;
    }
#endif
}
