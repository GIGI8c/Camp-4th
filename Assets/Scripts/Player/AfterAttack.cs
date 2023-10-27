using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterAttack : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //结束攻击动画时
        animator.ResetTrigger("clickAttack");//清除攻击信号
        animator.GetComponent<PlayerController>().canControl = true;//将PlayerController的canControl状态设置为ture
    }
}
