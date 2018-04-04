using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemy : Enemy
{


    public override void Start()
    {
        EnemyMaxHeath = 500;
        EnemyDamage = 20;
        base.Start();
    }


    public override void EnemyAI()
    {
        if (isDead == false)
        {
            if (Vector3.Distance(player.transform.position, transform.position) > 10)
            {
                //Partrol();
                //anim.Play("Idle");
                //Debug.Log("11111");
            }
            else if (Vector3.Distance(player.transform.position, transform.position) < 10 && Vector3.Distance(player.transform.position, transform.position) > 1.5f)
            {
                anim.SetBool("IsRun", true);
                agent.destination = player.transform.position;

                //Debug.Log("11111");
            }
            else if (Vector3.Distance(player.transform.position, transform.position) < 1.5f)
            {
                agent.destination = transform.position;
                transform.LookAt(player.transform.position);
                anim.SetBool("IsRun", false);
                anim.Play("Attack");
                HandBox.enabled = true;
                Invoke("HandBoxFalse", 1.5f);
            }
        }
    }
}
