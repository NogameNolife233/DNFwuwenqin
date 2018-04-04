using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathUI : MonoBehaviour
{
    private Image heath;
    public Enemy eyv;

	void Start ()
    {
        heath = GetComponent<Image>();
        //eyv = GetComponent<Enemy>();
	}
	

	void Update ()
    {
        heath.fillAmount = eyv.GetHeathRate();
        if (heath.fillAmount < 0.6f)
        {
            heath.color = Color.yellow;
        }
        if (heath.fillAmount < 0.3f)
        {
            heath.color = Color.red;
        }
        //if (heath.fillAmount == 0 || heath.fillAmount < 0)
        //{
        //    //eyv.EnemyDead();
        //    //var scrpts =  this.eyv.GetComponent<EnemyAl>();
        //    //if (scrpts.selfmonsterType == EnemyAl.monseterType.Dog)
        //    //{
        //    //    // dog.isDeadD = true;
        //    //}

        //    //EnemyAl.isDead = true;
        //    //FighterScript.isDeadF = true;
        //    //DogEnemyValue.isDeadD = true;
        //    //MutantScript.isDeadM = true;
        //}
        //Debug.Log(heath.fillAmount);
	}
}
