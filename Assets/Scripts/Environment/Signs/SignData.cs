using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Sign Data", menuName = "Data/Sign")]
public class SignData : ScriptableObject
{
    public string SignName;
    [TextArea]
    public string SignText;

    public List<PlayerAction> actionsUnlocked;

    public void UnlockActions()
    {
        foreach(PlayerAction newUnlockedAction in actionsUnlocked)
        {
            TryUnlock(newUnlockedAction);
        }
    }

    private void TryUnlock(PlayerAction actionToUnlock)
    {
        PlayerManager pm = PlayerManager.instance;
        if(!pm.unlockedActions.Contains(actionToUnlock))
            pm.unlockedActions.Add(actionToUnlock);
    }
}