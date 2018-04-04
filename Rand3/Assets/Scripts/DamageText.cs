using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    Text damageText;
	
	void Start ()
    {
        damageText = GetComponent<Text>();
	}
	
	
	void Update ()
    {
		
	}

    public void UpdateDamageText()
    {
        string text = string.Format("伤害：{0} ", PlayerPanel.Instance.UpdatePlayerPropertyAtk());
        damageText.text = text;
        Invoke("TextNull", 0.2f);
    }

    public void TextNull()
    {
        damageText.text = "";
    }

}
