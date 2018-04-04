using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Atk,
    Skill,
    Guard,
    Roll,
    Outbrust,
    Sidestep
}

public class PlayerContral : MonoBehaviour
{

    Animator anim;
    public List<Enemy> enemys;
    public List<AboutVFXGhostTrail> canyings;
    CapsuleCollider playerBox;
    public ShakeCamera shakeCamera;
    public BoxCollider weaponBox;
    public GameObject skillEfft;
    public GameObject outBrust;
    public GameObject skillImage;
    public GameObject setImage;
    public GameObject bigSkill;
    public PlayerState Nowstate = PlayerState.Idle;
    //人物属性
    public int PlayerMaxHp = 100;
    public int PlayerTempHp;
    public float MoveSpeed = 3f;
    public int basicStrength = 10;
    public int basicIntellect = 10;
    public int basicAgility = 10;
    public int basicStamina = 10;
    public int basicDamage = 10;

    public Transform From;
    public Transform To;



    private int coinAmount = 100;
    private Text coinText;
    private bool isOutBrust;
    public bool isDead = false;
    private Tweener PlayerIconTween;

    float nexttime = 0f;
    float cd = 1f;



    void Awake()
    {
        //weaponBox = GameObject.Find("LongSword").gameObject.GetComponent<BoxCollider>();
    }

    void Start ()
    {
        isOutBrust = false;
        skillImage.SetActive(false);
        PlayerTempHp = PlayerMaxHp;
        //enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        playerBox = GetComponent<CapsuleCollider>();
        //人物金币
        coinText = GameObject.Find("Coin").GetComponentInChildren<Text>();
        coinText.text = coinAmount.ToString();
        
        //Debug.Log(test.name);

        Debug.Log(weaponBox+":");
        weaponBox.enabled = false;
        transform.Rotate(new Vector3(0, -90, 0));
        //setImage.gameObject.SetActive(true);
        setImage.transform.localPosition = From.localPosition;
        PlayerIconTween = setImage.transform.DOLocalMoveX(To.localPosition.x,1f).OnComplete(()=> {
            setImage.transform.localPosition = From.localPosition;
           setImage.gameObject.SetActive(false);
            //setImage.GetComponent<Image>().CrossFadeAlpha(1, 0, false);
        }).OnStart(()=> {
            
        });
        
        PlayerIconTween.SetAutoKill(false);
       
        PlayerIconTween.Pause();
    }
	
	
	void Update ()
    {
        if (isDead!=true)
        {
            UpdateInput();
            PlayAnim();
        }

        //PlayerMaxHp = PlayerPanel.Instance.UpdatePlayerPropertyMaxHp();
        //Debug.Log(PlayerMaxHp);
    }
    //玩家输入 角色状态控制器 武器碰撞盒子的开关
    void UpdateInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        var dirMove = new Vector3(-h, 0, -v);

        if (Input.GetKeyDown(KeyCode.U))
        {
            isOutBrust = true;
            outBrust.SetActive(true);
            foreach (var canying in canyings)
            {
                canying.openGhostTrail = true;
            }
            Invoke("OutBrustOver", 5f);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isOutBrust == true)
            {
                anim.SetBool("skillcombo", true);
                gameObject.transform.Translate(new Vector3(0, 0, 1));
            }
            else
            {
                PlayerAtk();
            }
                // weaponBox.enabled = true;//武器碰撞盒子开
                //Nowstate = PlayerState.Atk;//切换状态
                //人物连击

            //Invoke("WeaponBoxFalse", 0.5f);//关闭碰撞盒子
            //Invoke("isChangeIdle", 0.4f);//切换状态
            //weaponBox.enabled = false;
        }
        //else if (Input.GetKeyDown(KeyCode.J) && Nowstate == PlayerState.Run)
        //{
        //    weaponBox.enabled = true;
        //    Nowstate = PlayerState.Atk;
        //    anim.Play("Atk1");
        //    Invoke("WeaponBoxFalse", 0.5f);
        //    Invoke("isChangeIdle", 0.3f);
        //}
        else if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("IsRoll");
            
            if (isOutBrust == true)
            {
                skillImage.SetActive(true);
                Time.timeScale = 0.6f;
                MoveSpeed = 8;
                foreach (var enemy in enemys)
                {
                    if (enemy == null)
                        continue;
                    enemy.agent.speed = 0;
                }
                
                Invoke("EnemySpeedSlow", 3f);
                
                //enemy.EnemySpeedSlow();
            }
        }
        else if (Input.GetKeyDown(KeyCode.L) && Nowstate != PlayerState.Atk)
        {
            if (isOutBrust == false)
            {
                weaponBox.enabled = true;
                anim.Play("BigAtk");
                GameObject go = Instantiate(skillEfft, transform.position, Quaternion.identity);
                shakeCamera.ShakeFor(0.3f, 0.1f);
                go.transform.position += transform.forward * 8.0f;
                Destroy(go, 1f);
                Invoke("WeaponBoxFalse", 0.8f);
            }
            else
            {
                //setImage.SetActive(true);
                //PlayerIconTween.Restart();
                //PlayerIconTween.Play();
                anim.Play("BigSkill");
                shakeCamera.ShakeFor(0.2f, 0.1f);
                //setImage.GetComponent<Image>().CrossFadeAlpha(0.5f, 1, false);
                //Invoke("SetImageOver", 1.5f);
            }

        }
        else if (Input.GetKeyDown(KeyCode.I) && Nowstate != PlayerState.Atk&& Nowstate != PlayerState.Roll)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            Nowstate = PlayerState.Guard;

            PlayerGuard();
            playerBox.enabled = false;
            Invoke("PlayerBoxTrue", 0.4f);
            Invoke("isChangeIdle", 0.3f);

        }
        else if (Nowstate != PlayerState.Guard && Nowstate != PlayerState.Atk&& Nowstate != PlayerState.Roll)
        {

            //Nowstate = PlayerState.Run;
            //Debug.Log(Nowstate);
            PlayerMove(dirMove, h);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ReplyHp();
        }

        
    }
    //玩家移动
    void PlayerMove(Vector3 dirMove, float h)
    {

        transform.position += dirMove * Time.deltaTime * MoveSpeed;
        transform.LookAt(transform.position + new Vector3(-h, 0, 0));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //transform.position += new Vector3(0, 2, 0);
            anim.Play("Jump");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            KnapsackPanel.Instance.Hide();
            PlayerPanel.Instance.Hide();
            ChestPanel.Instance.Hide();
            VendorPanel.Instance.Hide();
            //KnapsackPanel.Instance.StoreItem()
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            int id = Random.Range(1, 15);
            KnapsackPanel.Instance.StoreItem(id);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            
            KnapsackPanel.Instance.DisplaySwitch();
            PlayerPanel.Instance.DisplaySwitch();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Chest" && Input.GetKeyDown(KeyCode.J))
        {
            ChestPanel.Instance.DisplaySwitch();
            KnapsackPanel.Instance.DisplaySwitch();
            PlayerPanel.Instance.DisplaySwitch();
        }
        if (other.gameObject.tag == "Vendor" && Input.GetKeyDown(KeyCode.J))
        {
            //VendorPanel.Instance.InitShop();
            VendorPanel.Instance.DisplaySwitch();
            KnapsackPanel.Instance.DisplaySwitch();
            PlayerPanel.Instance.DisplaySwitch();
        }
    }

    void PlayerAtk()
    {
        //anim.Play("Atk1");
        anim.SetTrigger("combo");
    }

    void PlayerSkill()
    {
        anim.Play("");
    }

    void PlayerGuard()
    {
        anim.Play("Guard");
    }
    //被打的伤害方法
    public void PlayerBeHit(int _damger)
    {
        PlayerTempHp -= _damger;
        if (PlayerTempHp <= 0)
        {
            PlayerTempHp = 0;
            PlayerDead();
        }
    }
    //死亡
    public void PlayerDead()
    {
        isDead = true;
        if (PlayerTempHp <= 0)
        {
            anim.Play("DieA");
            Invoke("Disappear", 2.5f);
            //Destroy(gameObject,3f);
            
        }
    }
    //UI血条更新
    public float GetPlayerHeathRate()
    {
        return (float)PlayerTempHp / PlayerMaxHp;
    }

    void PlayAnim()
    {
        anim.SetFloat("H", Input.GetAxis("Horizontal"));
        anim.SetFloat("V", Input.GetAxis("Vertical"));
    }

    //状态切换
    void isChangeIdle()
    {
        Nowstate = PlayerState.Idle;
    }

    void isChangeAtk()
    {
        Nowstate = PlayerState.Atk;
    }

    void OutBrustOver()
    {
        outBrust.SetActive(false);
        foreach (var canying in canyings)
        {
            canying.openGhostTrail = false;
        }
        isOutBrust = false;
    }

    void isChangeGuard()
    {
        Nowstate = PlayerState.Guard;
    }

    //武器碰撞盒子关闭
    void WeaponBoxFalse()
    {
        weaponBox.enabled = false;
    }

    //人物碰撞盒子开启
    void PlayerBoxTrue()
    {
        playerBox.enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
    //人物死亡消失
    void Disappear()
    {
        gameObject.SetActive(false);
    }
    //武器碰撞盒子开启
    void OpenSwordBox()
    {
        weaponBox.enabled = true;
    }
    //武器碰撞盒子关闭
    void CloseSwordBox()
    {
        weaponBox.enabled = false;
    }
    //攻击状态判定
    public void AttackStart()
    {
        Nowstate = PlayerState.Atk;
    }
    public void AttackOver()
    {
        Nowstate = PlayerState.Idle;
    }
    //翻滚碰撞
    public void RollBoxStart()
    {
        Nowstate = PlayerState.Roll;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        playerBox.enabled = false;
    }
    public void RollBoxOver()
    {
        Nowstate = PlayerState.Idle;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        playerBox.enabled = true;
    }

    //花费金币
    public bool ConsumeCoin(int amount)
    {
        if (coinAmount >= amount)
        {
            coinAmount -= amount;
            coinText.text = coinAmount.ToString();
            return true;
        }
        return false;
    }

    //赚金币
    public void EarnCoin(int amount)
    {
        coinAmount += amount;
        coinText.text = coinAmount.ToString();
    }

    public void EnemySpeedSlow()
    {
        foreach (var enemy in enemys)
        {
            if (enemy == null)
                continue;
            enemy.EnemySpeedSlow();
        }
        skillImage.SetActive(false);
        MoveSpeed = 3;
        Time.timeScale = 1;
    }

    //人物回血
    public void ReplyHp()
    {
        PlayerTempHp += 50;
    }
    //技能特效
    public void BigSkill()
    {
        bigSkill.SetActive(true);
        //Time.timeScale = 1f;
    }
    public void SetImage()
    {
        setImage.SetActive(true);
        PlayerIconTween.Restart();
        PlayerIconTween.Play();
        
    }

    public void BigSkillOver()
    {
        bigSkill.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ComboSkillover()
    {
        anim.SetBool("skillcombo", false);
    }
    public void TimeScalesamll()
    {
        Time.timeScale = 0.4f;
    }
    public void TimeScalebig()
    {
        Time.timeScale = 1f;
    }

}
