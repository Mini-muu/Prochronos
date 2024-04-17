using System;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] public int currentBonesAmount;
    [SerializeField] public int maxBonesAmount;
    [SerializeField] private TextMeshProUGUI bonesAmountText;

    public void IncreaseAmountBy(int amount)
    {
        currentBonesAmount += amount;
        if (currentBonesAmount >= maxBonesAmount)
        {
            currentBonesAmount = maxBonesAmount;
        }
        UpdateBonesUI();
    }

    public void DecreaseAmountBy(int amount)
    {
        currentBonesAmount -= amount;
        if (currentBonesAmount < 0)
        {
            currentBonesAmount = 0;
        }
        UpdateBonesUI();
    }

    public void UpdateBonesUI()
    {
        bonesAmountText.text = $"{currentBonesAmount}";
    }

}
