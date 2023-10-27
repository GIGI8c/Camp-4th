using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    void Awake() { instance = this; }
    public static UIManager Instance
    { get { return instance; } }

    public int coinCount;//当前金币的数量
    public Text coinCountShow;//金币数量的显示

    public float currentHealth;//当前血量
    public float maxHealth;//最大血量
    public Slider healthShow;//血量的显示

    private void Update()
    {
        coinCountShow.text = coinCount.ToString();//更新金币数量显示
        healthShow.value = currentHealth / maxHealth;//更新血量显示
    }
}
