
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item {

    public int HP { get; set; }
    public int MP { get; set; }

    public Consumable(int id, string name, ItemType type, ItemQuality quality, string des, int capacity, int buyPrice, int sellPrice, string sprite, int hp, int mp)
        : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprite)
    {
        this.HP = hp;
        this.MP = mp;
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();

        string newText = string.Format("{0}\n\n加血：{1}\n加蓝：{2}", text, HP, MP);

        return newText;
    }
}
