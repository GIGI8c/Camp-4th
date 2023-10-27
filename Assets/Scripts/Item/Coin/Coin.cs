using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)//当玩家接触到金币的碰撞体时
    {
        UIManager.Instance.coinCount++;//金币数量增加

        gameObject.SetActive(false);//将物体隐藏
    }
}
