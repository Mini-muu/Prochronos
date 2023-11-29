using UnityEngine;

public enum ItemType
{
    Bones,
    Consumable,
    Meat,
    Equipment
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite icon;

    [Range(0,100)]
    public float dropChance;

    public ItemEffect[] itemEffects;

    public void ExecuteItemEffects()
    {
        foreach (var item in itemEffects)
        {
            item.ExecuteEffect();
        }
    }
}
