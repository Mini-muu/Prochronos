using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int currentBonesAmount;

    public SerializableDictionary<string, int> inventory;

    public List<PlayerAction> unlockedActions;

    public GameData()
    {
        currentBonesAmount = 0;

        inventory = new SerializableDictionary<string, int>();

        unlockedActions = new();
    }
}