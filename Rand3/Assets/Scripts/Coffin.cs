using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffin : MonoBehaviour
{
    public int CoffinMaxHp = 50;
    public int CoffinTempHp;

    void Start ()
    {
        CoffinTempHp = CoffinMaxHp;
	}
	
	
	void Update ()
    {
		
	}

    public void CoffinBeHit(int _damger)
    {
        CoffinTempHp -= _damger;
        if (CoffinTempHp <= 0)
        {
            CoffinTempHp = 0;
            Destroy(gameObject, 1f);
            //CoffinDead();
        }
    }
}
