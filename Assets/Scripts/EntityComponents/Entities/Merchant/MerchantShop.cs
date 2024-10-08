using System;
using System.Collections.Generic;
using UnityEngine;

public class MerchantShop : BaseInteraction
{
    [SerializeField] private UI_MerchantShop shopUI;

    private readonly List<GoodsData> sellingGoods = new();

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();

        ToggleMerchantShopUI();
    }

    private void ToggleMerchantShopUI() => shopUI.Toggle();

    public void BuySingleGoods(GoodsData goods)
    {
        if (!sellingGoods.Contains(goods)) return;

        if (!goods.IsAmountAvailable(1)) return;

        Wallet playerWallet = PlayerManager.instance.wallet;
        if (!playerWallet.HasEnoughMoney(goods.unitPrice)) return;

        playerWallet.DecreaseAmountBy(goods.unitPrice);

        GiveItem(goods);
        goods.DecreaseQuantity();

        shopUI.UpdateUI();
    }

    public void BuyMultipleGoods(GoodsData goods, int amount)
    {
        if (!goods.IsAmountAvailable(amount)) return;

        for (int i = 0; i < amount; i++)
            BuySingleGoods(goods);
    }

    public void SellSingleGoods(GoodsData goods)
    {
        //Player Inventory check
        throw new NotImplementedException();
    }

    public void SellMultipleGoods()
    {
        //Player Inventory check
        throw new NotImplementedException();
    }

    private void GiveItem(GoodsData goods)
    {
        //Player Inventory check
        throw new NotImplementedException();
    }

}
