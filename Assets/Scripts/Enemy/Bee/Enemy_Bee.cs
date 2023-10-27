using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bee : EnemyGeneral
{
    public override void Move()//蜜蜂的移动效果
    {
        if(canMove)
        rb.velocity = new Vector2(rb.velocity.x, speed * Time.deltaTime);
    }

    private void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, speed * Time.deltaTime);
        if (check.isTouchTop || check.isGround)//接触到天花板或地板时
            speed=-speed;//进行方向取反
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
