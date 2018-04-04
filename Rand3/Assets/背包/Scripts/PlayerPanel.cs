using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : Inventory
{
    //单例模式
    private static PlayerPanel _instance;
    public static PlayerPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("PlayerPanel").GetComponent<PlayerPanel>();
            }
            return _instance;
        }
    }

    private Text PropertyText;
    private PlayerContral player;


    public override void Start()
    {
        base.Start();
        PropertyText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContral>();
        UpdatePropertyText();
    }
    //更新角色属性面板显示
    public void UpdatePropertyText()
    {
        int Strength = 0;
        int Intellect = 0;
        int Agility = 0;
        int Stamina = 0;
        int Damage = 0;
        int playermaxhp = 0;


        foreach (EquimentSlot slot in slotlist)
        {
            if (slot.transform.childCount > 0)
            {
                Item item = slot.transform.GetChild(0).GetComponent<ItemUI>().item;
                if (item is Equipmen)
                {
                    Equipmen e = (Equipmen)item;
                    Strength += e.Strength;
                    Intellect += e.Intellect;
                    Agility += e.Agility;
                    Stamina += e.Stamina;
                }
                else if (item is Weapon)
                {
                    Damage += ((Weapon)item).Damage;
                }
            }
        }
        Strength += player.basicStrength;
        Intellect += player.basicStrength;
        Agility += player.basicAgility;
        Stamina += player.basicStamina;
        Damage = (int)(player.basicDamage + Damage + Strength * 0.3f);
        playermaxhp = player.PlayerMaxHp + (int)(Stamina * 0.3f);
        string text = string.Format("力量：{0}\n智力：{1}\n敏捷：{2}\n体力：{3}\n攻击力：{4}\n人物血量：{5} ", Strength, Intellect, Agility, Stamina, Damage,playermaxhp);
        PropertyText.text = text;
        
    }

    //更新角色属性的平A的伤害
    public int UpdatePlayerPropertyAtk()
    {
        int Strength = 0;
        int Intellect = 0;
        int Agility = 0;
        int Stamina = 0;
        int Damage = 0;

        foreach (EquimentSlot slot in slotlist)
        {
            if (slot.transform.childCount > 0)
            {
                Item item = slot.transform.GetChild(0).GetComponent<ItemUI>().item;
                if (item is Equipmen)
                {
                    Equipmen e = (Equipmen)item;
                    Strength += e.Strength;
                    Intellect += e.Intellect;
                    Agility += e.Agility;
                    Stamina += e.Stamina;
                }
                else if (item is Weapon)
                {
                    Damage += ((Weapon)item).Damage;
                }
            }
        }
        Strength += player.basicStrength;
        Intellect += player.basicStrength;
        Agility += player.basicAgility;
        Stamina += player.basicStamina;
        Damage = (int)(player.basicDamage + Damage + Strength * 0.3f);
        return Damage;
    }

    ////更新角色装备加的血量 BUG
    public int UpdatePlayerPropertyMaxHp()
    {

        int Strength = 0;
        int Intellect = 0;
        int Agility = 0;
        int Stamina = 0;
        int Damage = 0;
        int playermaxhp = 0;
        foreach (EquimentSlot slot in slotlist)
        {
            if (slot.transform.childCount > 0)
            {
                Item item = slot.transform.GetChild(0).GetComponent<ItemUI>().item;
                if (item is Equipmen)
                {
                    Equipmen e = (Equipmen)item;
                    Strength += e.Strength;
                    Intellect += e.Intellect;
                    Agility += e.Agility;
                    Stamina += e.Stamina;
                }
                else if (item is Weapon)
                {
                    Damage += ((Weapon)item).Damage;
                }
            }
        }
        Strength += player.basicStrength;
        Intellect += player.basicStrength;
        Agility += player.basicAgility;
        Stamina += player.basicStamina;
        playermaxhp = player.PlayerMaxHp + (int)(Stamina * 0.3f);


        return playermaxhp;
    }

}
