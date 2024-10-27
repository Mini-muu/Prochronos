using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;
    public PlayerStats playerStats;
    public Player player;
    //TODO - Check if used
    [SerializeField] private TextMeshProUGUI bonesAmountText;

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
        wallet.UpdateBonesUI();
    }

    public void LoadData(GameData _data)
    {
        ClearUnlockedActions();

        foreach(var action in _data.unlockedActions)
            unlockedActions.Add(action);
    }

    public void SaveData(ref GameData _data)
    {
        _data.unlockedActions.Clear();

        foreach (var action in unlockedActions) 
            _data.unlockedActions.Add(action);
    }

    public void ClearUnlockedActions() => unlockedActions.Clear();
}
