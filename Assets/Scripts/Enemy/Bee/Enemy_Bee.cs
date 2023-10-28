using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bee : EnemyGeneral
{
    public override void Move()//重写蜜蜂的移动效果
    {
        if (canMove == false) return;

        if ((check.isGround && speed < 0)||(check.isTouchTop&&speed>0)) speed = -speed;//接触到墙面或天花板时，移动方向取反
        rb.velocity = new Vector2(rb.velocity.x, speed * Time.deltaTime);//进行移动
    }

    public override void Hurt(Transform attacker)//蜜蜂的受伤效果
    {
        base.Hurt(attacker);
        canMove = false;//取消行动状态
        StartCoroutine(hurtMove(attacker));
    }
    IEnumerator hurtMove(Transform attacker)
    {
        rb.velocity = Vector2.zero;//无法移动
        yield return new WaitForSeconds(0.4f);//间隔0.4s后
        canMove = true;//恢复行动状态
    }
}
