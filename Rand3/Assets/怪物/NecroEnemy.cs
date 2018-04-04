using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroEnemy : Enemy
{
    public GameObject smallEnemy;
    public Transform pos;
    float nextTime = 0;
    float cd = 1f;


    public override void Start()
    {
        EnemyMaxHeath = 50;
        EnemyDamage = 50;
        base.Start();
    }


    public override void EnemyAI()
    {
        if (isDead == false)
        {
            if (Vector3.Distance(player.transform.position, transform.position) > 30)
            {
                //Partrol();
                //anim.Play("Idle");
                //Debug.Log("11111");
            }
            else if (Vector3.Distance(player.transform.position, transform.position) < 20 && Vector3.Distance(player.transform.position, transform.position) > 8)
            {
                anim.SetBool("IsRun", true);
                agent.destination = player.transform.position;

                //Debug.Log("11111");
            }
            else if (Vector3.Distance(player.transform.position, transform.position) < 8)
            {
                agent.destination = transform.position;
                transform.LookAt(player.transform.position);
                anim.SetBool("IsRun", false);
                anim.Play("Attack");
                if (Time.time > nextTime)
                {
                    if (player.GetComponent<PlayerContral>().isDead == true)//如果玩家死亡 不再生成 小怪物
                    {
                        return;
                    }
                    GameObject go = Instantiate(smallEnemy, pos.position, Quaternion.identity);
                    nextTime = Time.time + cd;
                    Destroy(go, 4f);
                }


            }
        }
    }
}
