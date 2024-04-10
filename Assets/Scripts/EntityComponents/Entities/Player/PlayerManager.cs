using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;
    public PlayerStats playerStats;
    public Player player;
    private int playerBones;
    [SerializeField] private TextMeshProUGUI bonesAmountText;

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
        UpdateBonesUI();
    }

    public void IncreaseAmountBy(int _amount)
    {
        playerBones += _amount;
        UpdateBonesUI();
    }

    public void DecreaseAmountBy(int _amount)
    {
        playerBones -= _amount;
        UpdateBonesUI();
    }

    private void UpdateBonesUI()
    {
        bonesAmountText.text = $"{playerBones}";
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
