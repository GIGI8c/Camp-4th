using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicCheck : MonoBehaviour
{
    [Header("======= 物理检测参数 =======")]
    [Tooltip("物理检测半径")]
    public float checkRaduis=0.2f;//检测范围，默认为0.2
    [Tooltip("地面图层")]
    public LayerMask groundLayer;//地面图层
    [Tooltip("墙面图层")]
    public LayerMask wallLayer;//地面图层

    [Header("======= 角色当前状态 =======")]
    [Tooltip("角色默认朝向，-1为左，1为右")]
    public int baseDirection = 1;//角色默认朝向
    [Tooltip("角色当前是否处于地面")]
    public bool isGround;//角色是否处于地面
    [Tooltip("角色当前是否处接触到天花板")]
    public bool isTouchTop;//角色是否接触到天花板
    [Tooltip("角色当前是否接触到墙面")]
    public bool isTouchWall;//角色是否接触到墙面

    private void Update()
    {
        CheckGround();//持续进行地面检测
        CheckWall();//持续进行墙面检测
        CheckTop();
    }

    public void CheckGround()//检测是否位于地面
    {
        isGround = Physics2D.OverlapCircle(transform.position, checkRaduis, groundLayer);
        //设置isGround状态为：以当前角色位置，在checkRaduis半径范围内，检测是否有groundLayer图层
    }

    public void CheckTop()//检测是否接触到天花板
    {
        isTouchTop = Physics2D.OverlapCircle(new Vector2(transform.position.x,(transform.position.y + GetComponent<CapsuleCollider2D>().bounds.size.y)), checkRaduis, groundLayer);
        //设置isTouchTop状态为：以当前角色位置+碰撞体高度，在checkRaduis半径范围内，检测是否有groundLayer图层
    }

    public void CheckWall()//检测是否接触到墙面
    {
        float offset = GetComponent<CapsuleCollider2D>().bounds.size.x;
        bool touchLeftWall = Physics2D.OverlapCircle( //左侧墙面检测
            new Vector2
            (
                transform.position.x - GetComponent<CapsuleCollider2D>().bounds.size.x / 2, //以角色x轴位置+碰撞体宽度一半为起始点x轴
                transform.position.y + GetComponent<CapsuleCollider2D>().bounds.size.y / 2),//以角色y轴位置+碰撞体高度一半为起始点y轴
                checkRaduis, //进行以checkRaduis为半径
                wallLayer//wallpaper为目标检测图层的碰撞检测
            );
        bool touchRightWall = Physics2D.OverlapCircle( //右侧墙面检测
            new Vector2
            (
                transform.position.x + GetComponent<CapsuleCollider2D>().bounds.size.x / 2, //以角色x轴位置+碰撞体宽度一半为起始点x轴
                transform.position.y + GetComponent<CapsuleCollider2D>().bounds.size.y / 2),//以角色y轴位置+碰撞体高度一半为起始点y轴
                checkRaduis, //进行以checkRaduis为半径
                wallLayer//wallpaper为目标检测图层的碰撞检测
            );
        isTouchWall = (touchLeftWall&&baseDirection*transform.localScale.x<0) || (touchRightWall&& baseDirection * transform.localScale.x>0);
        //左侧或右侧接触到墙壁时，且朝向和接触方向相同时,设置isTouchWall为真
    }

    private void OnDrawGizmosSelected()//在编辑窗口绘制出碰撞检测区域
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x - GetComponent<CapsuleCollider2D>().bounds.size.x / 2,
                transform.position.y + GetComponent<CapsuleCollider2D>().bounds.size.y / 2), checkRaduis);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + GetComponent<CapsuleCollider2D>().bounds.size.x / 2,
                transform.position.y + GetComponent<CapsuleCollider2D>().bounds.size.y / 2), checkRaduis);
        //左右墙壁的检测区域显示

        Gizmos.DrawWireSphere(transform.position, checkRaduis);
        //地面的检测区域显示

        Gizmos.DrawWireSphere(new Vector2(transform.position.x, (transform.position.y + GetComponent<CapsuleCollider2D>().bounds.size.y)), checkRaduis);
        //天花板的检测区域显示
    }
}
