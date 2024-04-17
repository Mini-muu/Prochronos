using UnityEditor;
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
    public string itemId;

    [Range(0,100)]
    public float dropChance;

    public ItemEffect[] itemEffects;

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public void ExecuteItemEffects()
    {
        foreach (var item in itemEffects)
        {
            item.ExecuteEffect();
        }
    }
}
