using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储物品
/// </summary>
public class Inventory : MonoBehaviour
{

    public Slot[] slotlist;
    private CanvasGroup canvasGroup;
    public Canvas canvas;

    public virtual void Start ()
    {
        slotlist = GetComponentsInChildren<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public bool StoreItem(int id)
    {
        Item item = InventoryManger.Instance.GetItemById(id);
        return StoreItem(item);
    }

    public bool StoreItem(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("要存储的物品id不存在");
            return false;
        }
        if (item.Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.LogWarning("没有空的物品槽");
                return false;
            }
            else
            {
                slot.StoreItem(item);//把物品存储到这个空的物品槽内
            }
        }
        else
        {
            Slot slot = FindSameIdSlot(item);
            if (slot != null)
            {
                slot.StoreItem(item);
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    emptySlot.StoreItem(item);
                }
                else
                {
                    Debug.LogWarning("没有空的物品槽");
                    return false;
                }
            }
        }
        return true;
    }
    //找到一个空的物品槽
    private Slot FindEmptySlot()
    {
        foreach (var slot in slotlist)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }

    private Slot FindSameIdSlot(Item item)
    {
        foreach (var slot in slotlist)
        {
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.ID&&slot.IsFilled()==false)
            {
                return slot;
            }
        }
        return null;
    }

    //显示面板
    public void Show()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
    //隐藏面板
    public void Hide()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
    //选择显示还是显示
    public void DisplaySwitch()
    {
        if (canvasGroup.alpha == 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

}
