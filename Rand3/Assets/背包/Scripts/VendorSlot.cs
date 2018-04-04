using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VendorSlot : Slot
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && InventoryManger.Instance.isPickedItem == false)
        {
            if (transform.childCount > 0)
            {
                Item currentItem = transform.GetChild(0).GetComponent<ItemUI>().item;
                transform.parent.parent.SendMessage("BuyItem", currentItem);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Left && InventoryManger.Instance.isPickedItem == true)
        {
            transform.parent.parent.SendMessage("SellItem");
        }
    }


}
