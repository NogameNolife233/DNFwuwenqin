using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeathUI : MonoBehaviour
{

    private Image heath;
    public PlayerContral player;

    void Start()
    {
        heath = GetComponent<Image>();
        
    }


    void Update ()
    {
        heath.fillAmount = player.GetPlayerHeathRate();
        //if (heath.fillAmount < 0.6f)
        //{
        //    heath.color = Color.yellow;
        //}
        //if (heath.fillAmount < 0.3f)
        //{
        //    heath.color = Color.red;
        //}
    }
}
