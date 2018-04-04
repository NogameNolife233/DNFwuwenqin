using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform pos;
    public Enemy enemy;
    float atkSpeed = 7f;
    public Vector3 dir;

    void Start ()
    {

    }
	
	
	void Update ()
    {
        //transform.Translate(-pos.right * Time.deltaTime * atkSpeed);
        transform.position += dir * Time.deltaTime * atkSpeed;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var player = other.gameObject.GetComponent<PlayerContral>();
            player.PlayerBeHit(enemy.EnemyDamage);
            Debug.Log(enemy.EnemyDamage);

        }
        if (other.gameObject.tag == "Coffin")
        {
            var coffin = other.gameObject.GetComponent<Coffin>();
            coffin.CoffinBeHit(enemy.EnemyDamage);

        }
    }
}
