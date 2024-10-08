using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveManager
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

    [Header("Data base")]
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
        inventoryItems = new List<InventoryItem>();
        //inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        inventoryItemsAlt = new List<KeyValuePair<ItemData, InventoryItem>>();

        equipment = new List<InventoryItem>();
        equipemntDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

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
            inventoryItemSlot[i].UpdateSlot(inventoryItems[i]);
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
        if (equipemntDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipemntDictionary.Remove(itemToRemove);

            itemToRemove.RemoveModifiers();
        }
    }

    public void AddItem(ItemData _item)
    {
        if (!IsMeat(_item) && TryGetValue(_item, out InventoryItem value) != null)
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventoryItems.Add(newItem);
            inventoryItemsAlt.Add(new KeyValuePair<ItemData, InventoryItem>(_item, newItem));
        }

        UpdateSlotUI();
    }

    public InventoryItem TryGetValue(ItemData key, out InventoryItem value)
    {
        value = null;

        foreach (var itemPair in inventoryItemsAlt)
        {
            if (itemPair.Key == key)
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
        if (TryGetValue(_item, out InventoryItem value) != null)
        {
            if (value.stackSize <= 1)
            {
                inventoryItems.Remove(value);
                inventoryItemsAlt.Remove(new KeyValuePair<ItemData, InventoryItem>(_item, value));
            }
            else
            {
                value.RemoveStack();
            }
        }

        UpdateSlotUI();
    }

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

        foreach (KeyValuePair<ItemData, InventoryItem> pair in inventoryItemsAlt)
        {
            _data.inventory.Add(pair.Key.itemId, pair.Value.stackSize);
        }
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
