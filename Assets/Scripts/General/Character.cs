using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
    //所有角色（玩家和敌人）的基础属性
{
    [Header("所有角色的基本属性")]
    [Tooltip("最大生命值")]
    public float maxHealth;//最大生命值
    [Tooltip("当前生命值")]
    public float currentHealth;//当前生命值
    [Tooltip("当前是否处于受伤无敌状态")]
    public bool isInvincible=false;//是否处于受伤无敌状态,默认为否
    [Tooltip("受伤无敌时间")]
    public float invincibleTime=0.5f;//受伤无敌时间，默认为0.5秒
    float invincibleTimer;//受伤计时器

    [Header("受伤时触发的事件")]
    public UnityEvent<Transform> OnTakeDamage;//受伤时事件(需要传入攻击方的位置)
    [Header("死亡时触发的事件")]
    public UnityEvent OnDead;//死亡时事件

    private void Start()
    {
        currentHealth = maxHealth;//初始化当前生命值，使其等于最大生命值
    }


    public void TakeDamage(Attack attacker)//受到伤害时
    {
        if (isInvincible) return;//处于无敌状态时,不执行后续指令

        if(currentHealth>attacker.attackDamage)//若剩余血量大于攻击者的攻击力
        {
            currentHealth -= attacker.attackDamage;//扣除伤害对应的血量
            HurtInvincible();//进入受伤无敌状态
            OnTakeDamage?.Invoke(attacker.transform);
            //激活受伤事件（玩家的事件为：播放受伤动画、玩家被弹开效果），并告知受伤的一方攻击者的位置
        }
        else //否则
        {
            currentHealth = 0;//血量归0
            OnDead?.Invoke();
            //激活死亡事件（玩家的事件为：播放死亡动画）
        }
    }
    public void HurtInvincible()//受伤无敌
    {
        isInvincible = true;//设置为无敌状态
        
        invincibleTimer = invincibleTime;//开启受伤无敌倒计时
    }

    private void Update()
    {
        if(isInvincible)//处于无敌状态时
        {
            invincibleTimer -= Time.deltaTime;//进行倒计时
            if(invincibleTimer<=0)//倒计时结束后
            {
                isInvincible = false;//取消无敌状态
            }
        }
    }
    public void DestoryCharacter()//销毁角色方法
    {
        currentHealth = maxHealth;//恢复满血量
        gameObject.SetActive(false);
    }
}
