using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [Header("药水提供的治疗量")]
    public int recoverValue;
    private void OnTriggerEnter2D(Collider2D collision)//当玩家接触到治疗药水的碰撞体时
    {
        if (collision.GetComponent<Character>().currentHealth == collision.GetComponent<Character>().maxHealth) return;//若当前满血，则不进行使用

        if (collision.GetComponent<Character>().currentHealth + recoverValue > collision.GetComponent<Character>().maxHealth)//若治疗量溢出
            collision.GetComponent<Character>().currentHealth = collision.GetComponent<Character>().maxHealth;//则恢复至最大生命值
        else 
            collision.GetComponent<Character>().currentHealth += recoverValue;//否则，恢复等治疗量的生命值

        gameObject.SetActive(false);//将物体隐藏
    }
}
