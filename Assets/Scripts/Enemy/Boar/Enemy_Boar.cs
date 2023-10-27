using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boar : EnemyGeneral
{
    public override void Move()//野猪的移动效果
    {
        base.Move();//先执行敌人基础类中的方法
        animator.SetBool("chasing", false);
    }

    private void Update()
    {
        if (check.isTouchWall)//接触到墙壁时
            Wait();
    }
    public override void Wait()//野猪的等待效果
    {
        base.Wait();
        animator.SetBool("waiting", !canMove);//设置等待动画
        if (canMove==true)//可以行动时
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);//角色进行翻转
        }
    }

    public override void Hurt(Transform attacker)//野猪的受伤效果
    {
        base.Hurt(attacker);
        canMove = false;//取消行动状态
        StartCoroutine(hurtMove(attacker));
    }
    IEnumerator hurtMove(Transform attacker)
    {
        rb.AddForce(new Vector2((transform.position.x - attacker.position.x), 0).normalized * hurtBounceForce, ForceMode2D.Impulse);
        //根据角色与攻击者的水平距离方向，提供一个hurtBounceForce大小的力，提供方式为瞬时
        yield return new WaitForSeconds(0.3f);//间隔0.3s后
        canMove = true;//恢复行动状态
    }
}
