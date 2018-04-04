using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestPanel : Inventory
{

    //单例模式
    private static ChestPanel _instance;
    public static ChestPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("ChestPanel").GetComponent<ChestPanel>();
            }
            return _instance;
        }
    }


}
