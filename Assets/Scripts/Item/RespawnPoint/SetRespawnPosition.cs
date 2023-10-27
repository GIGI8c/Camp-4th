using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRespawnPosition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)//当玩家接触到该复活点的碰撞体时
    {
        GameObject.Find("ReSpawnPoints").GetComponent<PlayerRespawn>().point = transform;//将当前复活点设置为该复活点
    }

    private void Update()
    {
        //持续更新激活点的颜色状态
        if (GameObject.Find("ReSpawnPoints").GetComponent<PlayerRespawn>().point == transform)//当该复活点为当前复活点时
            GetComponent<SpriteRenderer>().color = Color.green;//将其设置为绿色（表示已激活)
        else GetComponent<SpriteRenderer>().color = Color.white;//否则将其设置为蓝色（表示未激活）
    }

}
