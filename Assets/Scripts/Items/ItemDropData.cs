using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEngine;

[System.Serializable]
public class ItemDropData
{

    [SerializeField] private ItemData baseItemData;
    [SerializeField] bool overrideDropChance;
    [SerializeField, Range(0.1f, 100)] private float newDropChance;

    public float DropChance
    {
        get
        {
            if (overrideDropChance)
                return newDropChance;
            else
                return baseItemData.dropChance;
        }
    }

    public ItemType ItemType { get { return baseItemData.itemType; } }
    public string ItemName { get { return baseItemData.itemName; } }
    public Sprite Icon { get { return baseItemData.icon; } }
    public ItemEffect[] itemEffects { get { return baseItemData.itemEffects; } }

    public ItemData GetData() => baseItemData;
}