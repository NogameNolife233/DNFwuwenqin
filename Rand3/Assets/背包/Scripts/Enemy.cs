using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float EnemyMaxHeath = 100;
    public int EnemyDamage = 10;
    public float EnemytempHeath;
    public GameObject BabyBox;
    public BoxCollider HandBox;
    public DamageText dt;
    public GameObject damageImage;
    internal Transform player;
    internal NavMeshAgent agent;
    internal Animator anim;

    internal bool isDead = false;
    
    public virtual void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        //HandBox = GetComponent<BoxCollider>();
        EnemytempHeath = EnemyMaxHeath;
        transform.Rotate(new Vector3(0, 90, 0));
        HandBox.enabled = false;
        damageImage.SetActive(false);
        
    }
	

	void Update ()
    {
        EnemyAI();
	}
    //怪物被打 玩家武器调用
    public void EnemyBeHit(int _damger)
    {
        EnemytempHeath -= _damger;
        damageImage.SetActive(true);
        agent.speed = 0.5f;
        if (EnemytempHeath < 0||EnemytempHeath==0)
        {
            isDead = true;
            EnemytempHeath = 0;
            EnemyDead();
            DeadDrop();
        }
        if (dt == null)
            return;
        dt.UpdateDamageText();
        Invoke("EnemySpeedSlow", 0.5f);
        Invoke("DamageTxOver", 0.2f);
        

    }
    //怪物回复速度减慢
    public void EnemySpeedSlow()
    {
        agent.speed = 2f;
    }
    //特效显示关闭
    public void DamageTxOver()
    {
        damageImage.SetActive(false);
    }

    //怪物死亡
    public void EnemyDead()
    {
        anim.Play("Death");
        Destroy(gameObject, 0.5f);
    }
    //血条比例
    public float GetHeathRate()
    {
        return EnemytempHeath / EnemyMaxHeath;
    }
    //怪物死亡掉落物品
    public void DeadDrop()
    {
        Instantiate(BabyBox, transform.position,Quaternion.identity);
    }
    //怪物攻击碰撞盒子关闭
    public void HandBoxFalse()
    {
        HandBox.enabled = false;
    }
    //怪物AI测试
    public virtual void EnemyAI()
    {
        if (isDead == false)
        {
            if (Vector3.Distance(player.transform.position, transform.position) > 10)
            {
                //Partrol();
                //anim.Play("Idle");
                //Debug.Log("11111");
            }
            else if (Vector3.Distance(player.transform.position, transform.position) < 10)
            {
                anim.SetBool("IsRun", true);
                agent.destination = player.transform.position;
                //Debug.Log("11111");
            }
        }
    }






    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        int id = Random.Range(1, 10);
    //        KnapsackPanel.Instance.StoreItem(id);
    //        //Destroy(gameObject);
    //    }
    //}
}
