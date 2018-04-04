using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkBox : MonoBehaviour
{
    public Enemy enemy;
	
	void Start ()
    {
		
	}
	
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var player = other.gameObject.GetComponent<PlayerContral>();
            player.PlayerBeHit(enemy.EnemyDamage);
            Debug.Log(enemy.EnemyDamage);

        }
    }
}
