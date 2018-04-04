using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFire : MonoBehaviour {

    //DamageText dt;
    void Start ()
    {
        //dt = transform.GetComponent<DamageText>();
	}
	
	
	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.EnemyBeHit(PlayerPanel.Instance.UpdatePlayerPropertyAtk());
            //if (dt == null)
            //    return;
            //dt.UpdateDamageText();
            Debug.Log(PlayerPanel.Instance.UpdatePlayerPropertyAtk());

        }
    }
}
