using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform player; // 玩家对象
    public float parallaxFactor = 0.5f; // 视差因子
    public float backgroundHeight = 20f; // 背景高度
    private float initialY; // 背景初始Y位置

    void Start()
    {
        // 记录背景初始的Y位置
        initialY = transform.position.y;
    }

    void Update()
    {
        // 根据玩家的Y位置移动背景
        float playerHeight = player.position.y;
        float newY = initialY + playerHeight * parallaxFactor;

        // 当背景的Y位置超过一定高度时，重新设置背景的位置
        if (newY - initialY >= backgroundHeight)
        {
            newY = initialY + (newY - initialY) % backgroundHeight;
        }

        // 更新背景的位置
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
