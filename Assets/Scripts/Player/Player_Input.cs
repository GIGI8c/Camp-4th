using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Player_Input : MonoBehaviour
{
    //键位表和读取信号
    [Header("======= 键位设置 =======")]//键位设置
    [Tooltip("移动-左")]
    public string keyLeft = "a";//左转
    [Tooltip("移动-右")]
    public string keyRight = "d";//右转
    [Tooltip("跳跃")]
    public string keyJump = "space";//翻滚/跳跃
    [Tooltip("攻击")]
    public string keyAttack = "mouse 0";//攻击
    [Tooltip("防御")]
    public string keyDefense2 = "mouse 1";//防御

}