using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
//所有攻击的基础属性
{
    [Header("攻击属性")]
    [Tooltip("攻击力")]
    public int attackDamage;//攻击力
    [Tooltip("攻击范围")]
    public float attackRange;//攻击范围
    [Tooltip("攻击频率")]
    public float attackRate;//攻击频率

    private void OnTriggerStay2D(Collider2D collision)//当有物体进入该角色身体碰撞范围内时
    {
        collision.GetComponent<Character>()?.TakeDamage(this);
        //在该物体身上的character组件内调用(无该组件则不调用)TakeDamage方法，并告知受伤的一方攻击者是谁
    }
}
