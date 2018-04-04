using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorPanel : Inventory
{

    //单例模式
    private static VendorPanel _instance;
    public static VendorPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("VendorPanel").GetComponent<VendorPanel>();
            }
            return _instance;
        }
    }
    public int[] itemIdArray;

    PlayerContral player;

    public override void Start()
    {
        base.Start();
        InitShop();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContral>();
    }

    public void InitShop()
    {
        foreach (int itemId in itemIdArray)
        {
            //Random.Range(0, itemId);
            StoreItem(Random.Range(0, itemId));
        }
    }
    //主角购买
    public void BuyItem(Item item)
    {
        bool isSuccess = player.ConsumeCoin(item.BuyPrice);
        if (isSuccess)
        {
            KnapsackPanel.Instance.StoreItem(item);
        }
    }
    //主角出售
    public void SellItem()
    {
        int coinAmount = InventoryManger.Instance.PickedItem.item.SellPrice * InventoryManger.Instance.PickedItem.Amount;
        player.EarnCoin(coinAmount);
        InventoryManger.Instance.RemoveItem();
    }


}
