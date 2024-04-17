using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;
    public PlayerStats playerStats;
    public Player player;
    public Wallet wallet;

    public List<PlayerAction> unlockedActions;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        wallet = GetComponent<Wallet>();
        wallet.UpdateBonesUI();
    }

    public void LoadData(GameData data)
    {
        Debug.LogWarning("Not Implemented Loader");
    }

    public void SaveData(ref GameData _data)
    {
        Debug.LogWarning("Not Implemented Saver");
    }

    public void ClearUnlockedActions() => unlockedActions.Clear();
}
