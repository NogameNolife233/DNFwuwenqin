using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 物品
/// </summary>
public class ItemUI : MonoBehaviour
{
    public Item item { get; set; }
    public int Amount { get; set; }
    private Image itemImage;
    private Text amountText;

    void Awake()
    {
        itemImage = GetComponent<Image>();
        amountText = GetComponentInChildren<Text>();
    }

    //显示数据
    public void SetItem(Item item, int amount = 1)
    {
        this.item = item;
        this.Amount = amount;
        //更新UI
        itemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (item.Capacity > 1)
            amountText.text = Amount.ToString();
        else
            amountText.text = "";
    }
    //复制信息
    public void SetItemUI(ItemUI itemUI)
    {
        SetItem(itemUI.item, itemUI.Amount);
    }
    //更新文本
    public void AddAmount(int amount = 1)
    {
        this.Amount += amount;
        //更新UI
        if (item.Capacity > 1)
            amountText.text = Amount.ToString();
        else
            amountText.text = "";
    }

    public void ReduceAmount(int amount = 1)
    {
        this.Amount -= amount;
        //更新ui
        if(item.Capacity>1)
            amountText.text = Amount.ToString();
        else
            amountText.text = "";
    }
    //显示
    public void Show()
    {
        gameObject.SetActive(true);
    }
    //隐藏
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    //让ToolTip的位置跟随目标位置
    public void SetLocalPosition(Vector3 pos)
    {
        gameObject.transform.localPosition = pos; 
    }
    //当前物品 跟 另一个物品 交换显示
    public void Exchange(ItemUI itemUI)
    {
        Item itemTemp = itemUI.item;
        int amountTemp = itemUI.Amount;
        itemUI.SetItem(this.item, this.Amount);
        this.SetItem(itemTemp, amountTemp);
    }
}
