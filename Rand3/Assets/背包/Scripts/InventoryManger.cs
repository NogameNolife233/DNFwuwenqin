using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


/// <summary>
/// 所有物品的管理器
/// </summary>
public class InventoryManger : MonoBehaviour
{
    private static InventoryManger _instance;
    private List<Item> itemList;
    private ToolTip toolTip;
    private Canvas canvas;
    private ItemUI pickedItem;//鼠标选中的物体

    public bool isPickedItem = false;


    public ItemUI PickedItem
    {
        get
        {
            return pickedItem;
        }
    }
    //private JsonData itemJson;

    private void Awake()
    {
        ParseItemJson();
    }


    void Start()
    {
        //itemJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/item.json"));
        
        toolTip = GameObject.Find("ToolTip").GetComponent<ToolTip>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        pickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();
        pickedItem.Hide();//让pickeditem 默认不显示

    }

    void Update()
    {

        if (isPickedItem == true)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out pos);
            pickedItem.SetLocalPosition(pos);

        }
        //控制提示面板的跟随 箱子面板 角色面板 背包面板
        if (isPickedItem == false)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            toolTip.SetLocalPostion(position);
            
            //面板跟随 
            //if (Input.GetMouseButton(0))
            //{
            //    Vector2 pos2;
            //    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out pos2);
            //    ChestPanel.Instance.SetLocalPostion(pos2);
            //    //Vector2 pos3;
            //    //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out pos3);
            //    //PlayerPanel.Instance.SetLocalPostion(pos3);
            //}
          
        }
        //丢东西的处理
        if (isPickedItem == true && Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1) == false)
        {
            isPickedItem = false;
            PickedItem.Hide();
        }


    }
    //单例模式
    public static InventoryManger Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("InventoryManger").GetComponent<InventoryManger>();
            }
            return _instance;
        }
        set { }
    }

    //解析Json文件
    void ParseItemJson()
    {
        itemList = new List<Item>();
        //文本为在Unity里面是 TextAsset类型
        TextAsset itemText = Resources.Load<TextAsset>("Items");
        string itemsJson = itemText.text;//物品信息的Json格式
        JSONObject j = new JSONObject(itemsJson);
        foreach (JSONObject temp in j.list)
        {
            string typeStr = temp["type"].str;
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), typeStr);

            //下面的事解析这个对象里面的公有属性
            int id = (int)(temp["id"].n);
            string name = temp["name"].str;
            Item.ItemQuality quality = (Item.ItemQuality)System.Enum.Parse(typeof(Item.ItemQuality), temp["quality"].str);
            string description = temp["description"].str;
            int capacity = (int)(temp["capacity"].n);
            int buyPrice = (int)(temp["buyPrice"].n);
            int sellPrice = (int)(temp["sellPrice"].n);
            string sprite = temp["sprite"].str;

            Item item = null;
            switch (type)
            {
                case Item.ItemType.Consumable:
                    int hp = (int)(temp["hp"].n);
                    int mp = (int)(temp["mp"].n);
                    item = new Consumable(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, hp, mp);
                    break;
                case Item.ItemType.Equipment:
                    //
                    int strength = (int)temp["strength"].n;
                    int intellect = (int)temp["intellect"].n;
                    int agility = (int)temp["agility"].n;
                    int stamina = (int)temp["stamina"].n;
                    Equipmen.EquipmentType equipType = (Equipmen.EquipmentType)System.Enum.Parse(typeof(Equipmen.EquipmentType), temp["equipType"].str);
                    item = new Equipmen(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, strength, intellect, agility, stamina, equipType);
                    break;
                case Item.ItemType.Weapon:
                    //
                    int damage = (int)temp["damage"].n;
                    Weapon.WeaponType wpType = (Weapon.WeaponType)System.Enum.Parse(typeof(Weapon.WeaponType), temp["weaponType"].str);
                    item = new Weapon(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, damage, wpType);
                    break;
                case Item.ItemType.Material:
                    //
                    item = new Materials(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite);
                    break;
            }
            itemList.Add(item);
            Debug.Log(item);
        }
    }

    public Item GetItemById(int id)
    {
        foreach (var item in itemList)
        {
            if (id == item.ID)
            {
                return item;
            }
        }
        return null;
    }

    public void ShowToolTip(string content)
    {
        if (isPickedItem == true)
            return;
        toolTip.Show(content);
    }

    public void HideToolTip()
    {
        toolTip.Hide();
    }
    //把当前的物品槽的信息 复制给pickitem
    public void PickupItem(ItemUI itemUI)
    {
        PickedItem.SetItemUI(itemUI);
        isPickedItem = true;
        PickedItem.Show();
        toolTip.Hide();
    }
    //移除物品
    public void RemoveItem()
    {

            isPickedItem = false;
            PickedItem.Hide();

    }
    //移动面板

}
