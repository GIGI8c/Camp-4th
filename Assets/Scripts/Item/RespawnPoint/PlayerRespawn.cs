using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public GameObject player;
    private void Awake()
    {
        //player = GameObject.Find("Player");
    }
    [Header("======= 玩家当前的复活点 =======")]
    public Transform point;
}
