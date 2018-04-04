using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : Enemy
{
    public GameObject Bullet;
    public Transform pos;
    public float atkSpeed = 3f;
    float nextTime = 0;
    float cd = 1f;




    public override void Start()
    {
        EnemyMaxHeath = 80;
        EnemyDamage = 10;
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
            //else if (Vector3.Distance(player.transform.position, transform.position) < 30 && Vector3.Distance(player.transform.position, transform.position) > 10)
            //{
            //    anim.SetBool("IsRun", true);
            //    agent.destination = player.transform.position;

            //    //Debug.Log("11111");
            //}
            else if (Vector3.Distance(player.transform.position, transform.position) < 30)
            {
                agent.destination = transform.position;
                transform.LookAt(player.transform.position);
                anim.SetBool("IsRun", false);
                anim.Play("Attack");
                if (Time.time > nextTime)
                {
                    
                    GameObject go = Instantiate(Bullet, pos.position, Quaternion.identity);
                    go.transform.Rotate(pos.right, -90);
                    var bullet = go.GetComponent<Bullet>();
                    bullet.dir = pos.forward;
                    nextTime = Time.time + cd;
                    Destroy(go, 10f);
                }
                
                
            }
        }
    }
}
