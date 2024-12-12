using System;
using UnityEngine;

public class Fireplace : BaseInteraction, ISaveManager
{
    [SerializeField]
    private ItemData coockedMeat;

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();

        CookMeat();
    }

    public override void PlayerEnteredArea()
    {
        base.PlayerEnteredArea();

        SaveManager.instance.SaveGame();
    }

    private void CookMeat()
    {
        if (!UniqueInventory.instance.HasItem(ItemType.Meat)) return;

        UniqueInventory.instance.ReplaceFirstOccurrence(ItemType.Meat, Instantiate(coockedMeat));
    }

    public void LoadData(GameData data) 
    {
        if (data.fireplaceCheckpoint == null) return;
        PlayerManager.instance.player.transform.position = data.fireplaceCheckpoint.transform.position;
    }

    public void SaveData(ref GameData _data)
    {
        _data.fireplaceCheckpoint = this;
    }
}
