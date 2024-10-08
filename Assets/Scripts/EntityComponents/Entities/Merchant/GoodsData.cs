using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item Data", menuName = "Data/Shop Item")]
public class GoodsData : ItemData
{
    public int availableQuantity;
    public int unitPrice;
    public bool sellable;

    public void DecreaseQuantity() => --availableQuantity;
    public void IncreaseQuantity() => ++availableQuantity;
    public bool IsUnlimited() => availableQuantity < 0;
    public bool IsAmountAvailable(int amountNeeded) => IsUnlimited() || availableQuantity > amountNeeded;

}

