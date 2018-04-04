using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquimentSlot : Slot
{
    public Equipmen.EquipmentType equipType;
    public Weapon.WeaponType weaponType;
    //public PlayerContral player;


    private void Start()
    {
        //player = GameObject.Find("Player").GetComponent<PlayerContral>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        bool isUpdateProperty = false;
        //手上有东西
        if (InventoryManger.Instance.isPickedItem == true)
        {
            ItemUI pickedItem = InventoryManger.Instance.PickedItem;
            //放入的装备槽有装备的情况
            if (transform.childCount > 0)//物品槽里是否有物品
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();//当前装备槽里的物品
                if (pickedItem.item is Equipmen)//判断类型
                {
                    if (((Equipmen)pickedItem.item).EquipType == this.equipType)//手上装备适合放在装备槽里
                    {
                        //Item item = currentItemUI.item;
                        //int amount = currentItemUI.Amount;
                        //currentItemUI.SetItem(pickedItem.item, pickedItem.Amount);
                        //InventoryManger.Instance.PickedItem.SetItem(item, amount);
                        InventoryManger.Instance.PickedItem.Exchange(currentItemUI);
                        isUpdateProperty = true;
                        //player.PlayerMaxHp = PlayerPanel.Instance.UpdatePlayerPropertyMaxHp();




                    }
                }
                else if (pickedItem.item is Weapon)
                {
                    if (((Weapon)pickedItem.item).WpType == this.weaponType)//手上装备适合放在装备槽里
                    {
                        InventoryManger.Instance.PickedItem.Exchange(currentItemUI);
                        isUpdateProperty = true;
                    }
                }
            }
            else
            {
                if (pickedItem.item is Equipmen)
                {
                    if (((Equipmen)pickedItem.item).EquipType == this.equipType)//手上装备适合放在装备槽里
                    {
                        this.StoreItem(InventoryManger.Instance.PickedItem.item);
                        InventoryManger.Instance.RemoveItem();
                        isUpdateProperty = true;
                        //player.PlayerMaxHp = PlayerPanel.Instance.UpdatePlayerPropertyMaxHp();



                    }
                }
                else if (pickedItem.item is Weapon)
                {
                    if (((Weapon)pickedItem.item).WpType == this.weaponType)//手上装备适合放在装备槽里
                    {
                        this.StoreItem(InventoryManger.Instance.PickedItem.item);
                        InventoryManger.Instance.RemoveItem();
                        isUpdateProperty = true;
                    }
                }
            }

            
        }
        //手上没东西
        else
        {
            if (transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();//当前装备槽里的物品
                InventoryManger.Instance.PickupItem(currentItemUI);
                DestroyImmediate(currentItemUI.gameObject);
                isUpdateProperty = true;
                //player.PlayerMaxHp = PlayerPanel.Instance.UpdatePlayerPropertyMaxHp();
                


            }
        }
        if (isUpdateProperty == true)
        {
            transform.parent.SendMessage("UpdatePropertyText");
        }

    }

}
