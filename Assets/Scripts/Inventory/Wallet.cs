using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
{

    [SerializeField] private int currentBonesAmount;
    [SerializeField] private int maxBonesAmount;
    [SerializeField] private Image itemImage;
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

    public bool HasEnoughMoney(int amountNeeded) => currentBonesAmount >= amountNeeded;
}
