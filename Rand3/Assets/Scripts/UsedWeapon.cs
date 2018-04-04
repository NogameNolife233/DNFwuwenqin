using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedWeapon : MonoBehaviour
{

    //public DamageText dt;
    //public List<DamageText> dts;
	void Start ()
    {
		
	}
	

	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.EnemyBeHit(PlayerPanel.Instance.UpdatePlayerPropertyAtk());
            //if (dts == null)
            //    return;
            //foreach (var dt in dts)
            //{
            //    if (dt == null)
            //        return;
                
            //}
            Debug.Log(PlayerPanel.Instance.UpdatePlayerPropertyAtk());

        }
    }
}
