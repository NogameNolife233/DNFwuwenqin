using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 物品槽
/// </summary>
public class Slot : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    public GameObject itemPrefab;
    //把item放在自身下面，如果有item了，amount++，没有就生成
    public void StoreItem(Item item)
    {
        if (transform.childCount == 0)
        {
            var go = Instantiate(itemPrefab, transform);
            go.GetComponent<ItemUI>().SetItem(item);

        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount();
        }
    }
    //找到当前物品槽存储的类型
    public Item.ItemType GetItemType()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().item.Type;
    }
    //找到当前物品槽存储的ID
    public int GetItemId()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().item.ID;
    }
    //判断当前的物品槽是否满了
    public bool IsFilled()
    {
        ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
        return itemUI.Amount >= itemUI.item.Capacity;//当前数量大于等于容量
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            string toolTipText = transform.GetChild(0).GetComponent<ItemUI>().item.GetToolTipText();
            InventoryManger.Instance.ShowToolTip(toolTipText);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManger.Instance.HideToolTip();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
            if (InventoryManger.Instance.isPickedItem == false)//当前鼠标没有任何物品，也没选中任何物品
            {
                InventoryManger.Instance.PickupItem(currentItem);//把当前的物品槽的信息 复制给pickitem
                Destroy(currentItem.gameObject);
            }
            else
            {
                if (currentItem.item.ID != InventoryManger.Instance.PickedItem.item.ID)
                {
                    //借鉴的代码
                    Item item = currentItem.item;
                    int amount = currentItem.Amount;
                    currentItem.SetItem(InventoryManger.Instance.PickedItem.item, InventoryManger.Instance.PickedItem.Amount);
                    InventoryManger.Instance.PickedItem.SetItem(item, amount);
                    ////BUG
                    //StoreItem(currentItem.item);
                    //InventoryManger.Instance.RemoveItem();
                    //InventoryManger.Instance.PickupItem(currentItem);
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            if (InventoryManger.Instance.isPickedItem == true)
            {
                
                for (int i = 0; i < InventoryManger.Instance.PickedItem.Amount; i++)
                {
                    StoreItem(InventoryManger.Instance.PickedItem.item);
                }

                InventoryManger.Instance.RemoveItem();


            }
            else
            {
                return;
            }
        }
    }
}
