using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
    //所有敌人的基础属性
{
    [Header("======= 敌人组件 =======")]
    public PhysicCheck check;//物理检测组件
    protected Rigidbody2D rb;//敌人的刚体组件（只可被子类访问）
    protected Animator animator;//敌人的动画控制器组件（只可被子类访问）

    [Header("======= 敌人基础属性 =======")]
    [Tooltip("敌人的移动速度")]
    public float speed = 100;//敌人的移动速度，默认值为100
    [Tooltip("敌人的跳跃力度")]
    public float jumpForce = 16.5f;//角色的跳跃力度，默认为16.5
    [Tooltip("敌人的受伤被击退力度")]
    public float hurtBounceForce = 16.5f;//角色的受伤被击退力度，默认为16.5

    [Header("======= 敌人等待计时器 =======")]
    public bool canMove=true;//是否正在等待状态
    public float waitTime;//等待时间
    public float currentWaitTime;//当前等待时间

    private void Awake()
    {
        check = GetComponent<PhysicCheck>();//获取敌人身上的物理检测组件
        rb = GetComponent<Rigidbody2D>();//获取敌人身上的刚体组件
        animator = GetComponent<Animator>();//获取敌人身上的动画控制器组件
    }

    private void OnEnable()
    {
        canMove = true;//恢复行动状态
        gameObject.layer = 7;//恢复敌人图层
        animator.SetBool("dead", false);//在敌人的动画控制器中设置"dead"为false
    }

    private void Update()
    {
        Wait();//等待状态检测
    }

    private void FixedUpdate()
    {
        Move();//进行常规移动
    }

    public virtual void Move()//可被重写的移动方法
    {
        if (canMove==false) return;//非可移动状态时，不进行移动

        rb.velocity = new Vector2(speed * -transform.localScale.x * Time.deltaTime, rb.velocity.y);
        //敌人按speed速度朝当前朝向进行移动
    }

    public virtual void Wait()//可被重写的等待状态
    {
        if (currentWaitTime < waitTime)//未到达等待时间时
        {
            canMove = false;//取消行动状态
            rb.velocity = Vector2.zero;//角色停止移动
            currentWaitTime += Time.deltaTime;//进行敌人等待计时
        }
        else  //若已到达等待时间
        {
            currentWaitTime = 0; //则重置等待计时器
            canMove = true;//恢复行动状态
        }
    }

    public virtual void Hurt(Transform attacker) //可被重写的敌人的受击效果(传入攻击者)
    {
        animator.SetTrigger("hurt");//在敌人的动画控制器中触发"hurt"
    }

    public virtual void Dead()//可被重写的敌人的死亡效果
    {
        canMove = false;//取消行动状态
        gameObject.layer = 2;//改变敌人图层
        animator.SetBool("dead", true);//在敌人的动画控制器中设置"dead"为true
        rb.velocity = Vector2.zero;//敌人停止移动
    }

}
