using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
    //角色的基础控制（移动、跳跃、攻击）
{
    [Header("======= 角色组件 =======")]
    public Player_Input input;//按键输入表组件
    public PhysicCheck check;//物理检测组件
    Rigidbody2D rb;//角色的刚体组件
    Animator animator;//角色的动画控制器组件

    [Header("======= 角色变量 =======")]
    [Tooltip("角色是否可被控制")]
    public bool canControl = true;//角色是否可被控制（如：受伤时无法控制）
    [Tooltip("角色的移动速度")]
    public float speed = 300;//角色的移动速度，默认值为300
    [Tooltip("角色的跳跃力度")]
    public float jumpForce = 16.5f;//角色的跳跃力度，默认为16.5
    [Tooltip("角色的受伤被击退力度")]
    public float hurtBounceForce = 16.5f;//角色的受伤被击退力度，默认为16.5

    private void Awake()
    {
        input = GetComponent<Player_Input>();//获取角色身上的按键输入表组件
        check = GetComponent<PhysicCheck>();//获取角色身上的物理检测组件
        rb = GetComponent<Rigidbody2D>();//获取角色身上的刚体组件
        animator = GetComponent<Animator>();//获取角色身上的动画控制器组件
    }

    int moveForward = -1;//移动朝向
    private void Update()
    {
        UIManager.Instance.currentHealth = GetComponent<Character>().currentHealth;
        UIManager.Instance.maxHealth = GetComponent<Character>().maxHealth;
        //同步当前血量到UI显示

        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        //将动画控制器中的"velocityX"变量设置为角色刚体的横向速度的绝对值（朝左移动时速度为负数）
        animator.SetFloat("velocityY", rb.velocity.y);
        //将动画控制器中的"velocityY"变量设置为角色刚体的纵向速度
        animator.SetBool("isGround", check.isGround);
        //将动画控制器中的"isGround"变量设置为物理检测中的isGround状态

        if (Input.GetKey(input.keyLeft)) moveForward = -1;
        //按键输入表中的"左移动键"被按下时，设置移动朝向为-1
        else if (Input.GetKey(input.keyRight)) moveForward = 1;
        //按键输入表中的"右移动键"被按下时，设置移动朝向为1
        else moveForward = 0;//否则，移动朝向归零

        if (Input.GetKeyDown(input.keyJump) && check.isGround) Jump();//按下跳跃键，且位于地面时,进行跳跃

        if (Input.GetKeyDown(input.keyAttack)) PlayerAttack();//按下攻击键时，进行攻击
    }

    private void FixedUpdate()
    {
        if (canControl == false) return;//无法被控制时，不执行后续代码
        Move();
    }

    #region 角色的移动方法
    public void Move()
    {
        switch(moveForward)
        {
            case -1://移动朝向为-1时
                transform.localScale = new Vector3(-1, 1, 1);//角色朝左进行翻转
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
                //角色向左按speed大小的速度进行移动
                break;

            case 1://移动朝向为1时
                transform.localScale = new Vector3(1, 1, 1);//角色朝右进行翻转
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
                //角色向右按speed大小的速度进行移动
                break;

            case 0://移动朝向为0时
                rb.velocity = new Vector2(0, rb.velocity.y);
                break;
        }
    }
    #endregion

    #region 角色的跳跃方法
    public void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        //对角色刚体的上方向提供一个jumpForce大小的力，提供方式为瞬时
    }
    #endregion

    #region 角色受伤相关
    public void PlayerHurtAnimation()//角色受伤动画
    {
        animator.SetTrigger("hurt");//在玩家动画控制器中触发"hurt"
    }
    public void PlayerHurtEffect(Transform attacker)//角色受伤效果
    {
        canControl = false;
        rb.velocity = Vector2.zero;//使角色停止移动
        rb.AddForce(new Vector2((transform.position.x - attacker.position.x), 0).normalized * hurtBounceForce, ForceMode2D.Impulse);
        //根据角色与攻击者的水平距离方向，提供一个hurtBounceForce大小的力，提供方式为瞬时
    }
    #endregion

    #region 角色死亡相关
    public void PlayerDeadAnimation()//角色死亡动画
    {
        //if(animator.GetBool("isDead")==false) animator.SetBool("isDead", true);//在玩家动画控制器中将"isDead"设置为true
        animator.SetTrigger("isDead");
    }

    public void PlayerDeadEffect()
    {
        canControl = false;//角色无法被继续控制
        rb.velocity = Vector2.zero;//使角色停止移动
    }

    public void PlayerRespawn()
    {
        GetComponent<Character>().currentHealth = GetComponent<Character>().maxHealth;//将玩家回满血量
        transform.position = GameObject.Find("ReSpawnPoints").GetComponent<PlayerRespawn>().point.position;//重置玩家出生点
        animator.ResetTrigger("isDead");//清除死亡信号

        for (int i=0;i<GameObject.Find("RefreshItem").transform.childCount;i++)//遍历“RefreshItem”物体的所有子物体
            GameObject.Find("RefreshItem").transform.GetChild(i).gameObject.SetActive(true);//将其全部激活
        for (int i = 0; i < GameObject.Find("RefreshEnemy").transform.childCount; i++)//遍历“RefreshEnemy”物体的所有子物体
            GameObject.Find("RefreshEnemy").transform.GetChild(i).gameObject.SetActive(true);//将其全部激活
        
        canControl = true;//角色恢复控制
    }
    #endregion

    #region 角色攻击相关
    public void PlayerAttack()
    {
        animator.SetTrigger("clickAttack");//在玩家动画控制器中触发"clickAttack"
        rb.velocity = new Vector2(0,rb.velocity.y);//使角色水平方向停止移动
        canControl = false;
    }
    #endregion

}
