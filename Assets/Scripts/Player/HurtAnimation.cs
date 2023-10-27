using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAnimation : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerController>().canControl = true;
        //当动画播放完毕时，将PlayerController的canControl状态设置为ture
    }
}
